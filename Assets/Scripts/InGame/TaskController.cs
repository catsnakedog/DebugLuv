using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskController : MonoBehaviour
{
    List<TextData> _data;
    string[] _chTask;

    public IEnumerator TaskSetting(List<TextData> textData)
    {
        _data = textData;
        
        foreach(TextData data in _data)
        {
            _chTask = new string[4];
            if (!string.IsNullOrEmpty(data.Ch1Task))
                _chTask[0] = data.Ch1Task;
            if (!string.IsNullOrEmpty(data.Ch2Task))
                _chTask[0] = data.Ch2Task;
            if (!string.IsNullOrEmpty(data.Ch3Task))
                _chTask[0] = data.Ch3Task;
            if (!string.IsNullOrEmpty(data.Ch4Task))
                _chTask[0] = data.Ch4Task;

            List<List<List<string>>> lineList = new List<List<List<string>>>();

            int i = 0;
            foreach(string orderList in data.TaskOrder.Split("\n"))
            {
                lineList.Add(new List<List<string>>());
                int j = 0;
                foreach (string order in orderList.Split("-"))
                {
                    lineList[i].Add(new List<string>());
                    foreach(string taskName in order.Split("/"))
                    {
                        if(taskName == "BG")
                        {
                            if (!string.IsNullOrEmpty(data.BGTask))
                                lineList[i][j].Add(taskName);
                        }
                        else if(taskName == "Ch")
                            {
                                if (!string.IsNullOrEmpty(_chTask[0]) || !string.IsNullOrEmpty(_chTask[1]) || !string.IsNullOrEmpty(_chTask[2]) || !string.IsNullOrEmpty(_chTask[3]))
                                lineList[i][j].Add(taskName);
                        }
                        else
                        {
                            if(!string.IsNullOrEmpty(data.Text))
                                lineList[i][j].Add(taskName);
                        }
                    }
                    j++;
                }
                i++;
            }
            StartCoroutine(StartLine(lineList));
        }
        yield return new WaitForSeconds(0);
    }

    IEnumerator StartLine(List<List<List<string>>> lineList)
    {
        Coroutine[] tasks = new Coroutine[lineList.Count];
        int i = 0;
        foreach (List<List<string>> orderList in lineList)
        {
            tasks[i] = StartCoroutine(StartOrder(orderList));
            i++;
        }

        foreach(Coroutine task in tasks)
        {
            yield return task;
        }

        TaskEnd();
    }

    IEnumerator StartOrder(List<List<string>> orderList)
    {
        foreach(List<string> taskList in orderList)
        {
            yield return StartTask(taskList);
        }
    }

    IEnumerator StartTask(List<string> taskList)
    {
        Coroutine[] tasks = new Coroutine[taskList.Count];
        int i = 0;
        foreach(string task in taskList)
        {
            if (task == "BG")
            {
                tasks[i] = StartCoroutine(BGTask());
            }
            if (task == "Ch")
            {
                tasks[i] = StartCoroutine(ChTask(_chTask));
            }
            else
            {
                tasks[i] = StartCoroutine(TextTask(_data[0]));
            }
            i++;
        }

        foreach(Coroutine task in tasks)
        {
            yield return task;
        }
    }

    IEnumerator TextTask(TextData data)
    {
        yield return StartCoroutine((Managers.Scene.CurrentScene.SceneUI as UI_InGame).TextShow(data));
    }

    IEnumerator BGTask()
    {
        yield break;
    }

    IEnumerator ChTask(string[] task)
    {
        yield break;
    }


    void TaskEnd()
    {
        if (_data[0].ChoiceNumber == 0)
        {
            (Managers.Scene.CurrentScene as InGame).ParagraphEnd();
        }
        else
        {
            Managers.UI_Manager.ShowPopupUI<UI_Choice>();
        }
    }
}