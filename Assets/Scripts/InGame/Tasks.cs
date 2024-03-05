using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TaskManager;

public class Tasks
{
    private TaskLogic _taskLogic;
    private SetenceData _setenceData;
    private int _completeCnt;

    public Tasks(TaskLogic taskLogic, SetenceData setenceData)
    {
        _taskLogic = taskLogic;
        _setenceData = setenceData;

        RunTasks();
    }

    private void RunTasks()
    {
        RunTask();
        RunAdd();
    }

    private void RunAdd()
    {
        foreach (int addNum in _taskLogic.AddLogic)
            TaskManager.Instance.RunTasks(addNum);
    }

    private void RunExtra()
    {

    }

    private void RunTask()
    {
        string[] lines = _setenceData.TasksOrder.Split("\n");
        foreach(string line in lines)
        {
            string[] tasks = line.Trim().Split("/");
            foreach(string task in tasks)
            {
                Type classType;

                switch(task)
                {
                    case "Etc":
                        classType = Type.GetType(_setenceData.EtcTask.Name);
                        EffectManager.Instance.PlayEffect(classType, TaskManager.Instance.Storage[_setenceData.Storage], _setenceData.EtcTask.Value);
                        break;
                    case "BG":
                        classType = Type.GetType(_setenceData.BgTask.Name);
                        EffectManager.Instance.PlayEffect(classType, GameObject.Find("BG"), _setenceData.BgTask.Value);
                        break;
                    case "Text":
                        // Text 재생으로 연결
                        break;
                    case "Ch1":
                        classType = Type.GetType(_setenceData.Ch1Task.Name);
                        EffectManager.Instance.PlayEffect(classType, GameObject.Find("Ch1"), _setenceData.Ch1Task.Value);
                        break;
                    case "Ch2":
                        classType = Type.GetType(_setenceData.Ch2Task.Name);
                        EffectManager.Instance.PlayEffect(classType, GameObject.Find("Ch2"), _setenceData.Ch2Task.Value);
                        break;
                    case "Ch3":
                        classType = Type.GetType(_setenceData.Ch3Task.Name);
                        EffectManager.Instance.PlayEffect(classType, GameObject.Find("Ch3"), _setenceData.Ch3Task.Value);
                        break;
                    case "Ch4":
                        classType = Type.GetType(_setenceData.Ch4Task.Name);
                        EffectManager.Instance.PlayEffect(classType, GameObject.Find("Ch4"), _setenceData.Ch4Task.Value);
                        break;
                }
            }
        }
    }

    private void Complete()
    {
        _completeCnt++;
    }
}
