//-------------------------------------------------------------------------------------------------
// @file	.cs
//
// @brief	을 위한 매니저
//
// @date	2024-03-14
//
// Copyright 2024 Team One-eyed Games. All Rights Reserved.
//-------------------------------------------------------------------------------------------------


using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class TaskManager : ManagerSingle<TaskManager>
{
    public class TaskConnect
    {
        public List<int> ParallelConnect;
        public List<int> SequentialConnect;

        public TaskConnect()
        {
            ParallelConnect   = new();
            SequentialConnect = new();
        }
    }
    
    private TaskConnect[] _taskConnect = null;
    private List<Tasks> _tasks         = null;
    private List<LineData> _data       = null;
    private GameObject _tasksRoot      = null;

    private int _taskDoneCount = 0;
    private int _taskCount     = 0;

    public static void StartSetenceTasks(List<LineData> data)
    {
        Instance._taskConnect   = new TaskConnect[data.Count];
        Instance._data          = data;
        Instance._taskDoneCount = 0;
        Instance._taskCount     = data.Count;

        ClearText();

        for (int i = 0; i < Instance._taskConnect.Length; i++)
        {
            Instance._taskConnect[i]                   = new TaskConnect();
            Instance._taskConnect[i].ParallelConnect   = new();
            Instance._taskConnect[i].SequentialConnect = new();

            string[] connects = data[i].Connect.Split("/");

            if (connects[0].Trim() == "")
                continue;

            foreach (string connect in connects)
            {
                string[] info = connect.Split("_");
                string type = info[0];
                int num = Util.StringToInt(info[1]);

                switch (type)
                {
                    case "C":
                        Instance._taskConnect[i].SequentialConnect.Add(num);
                        break;
                    case "P":
                        Instance._taskConnect[i].ParallelConnect.Add(num);
                        break;
                    default:
                        Debug.LogWarning($"error_TaskManager : Connect Type이 이상합니다.");
                        break;
                }
            }
        }

        if(Instance._tasksRoot == null)
        {
            Instance._tasksRoot = new GameObject("@Tasks");
        }
            
        if(Instance._tasks == null)
        {
            Instance._tasks = new();
            for (int i = 0; i < 6; i++)
            {
                GameObject _task = new GameObject($"Task_{ i.ToString() }");
                _task.transform.parent =  Instance._tasksRoot.transform;
                Tasks tasks = _task.AddComponent<Tasks>();
                Instance._tasks.Add(tasks);
            }
        }

        RunTasks(0);

        Instance.StartCoroutine(Instance.WaitTaskAllDone());
    }

    /// <summary>
    /// Task를 run
    /// </summary>
    /// <param name="num"></param>
    public static void RunTasks(int num)
    {
        while(Instance._tasks.Count <= num)
        {
            GameObject _task = new GameObject($"Task_{Instance._tasks.Count.ToString()}");
            _task.transform.parent =  Instance._tasksRoot.transform;
            Tasks tasks = _task.AddComponent<Tasks>();
            Instance._tasks.Add(tasks);
        }

        Instance._tasks[num].Set(Instance._taskConnect[num], Instance._data[num]);
    }

    /// <summary>
    /// Task를 Skip
    /// </summary>
    /// <param name="num"></param>
    public static void SkipTasks()
    {
        int num = 0;
        
        while (num++ < Instance._tasks.Count)
        {
            Instance._tasks[num].Set(Instance._taskConnect[num], Instance._data[num]);
        
        }

    }



    public void DoneTask()
    {
        _taskDoneCount++;
    }

    private IEnumerator WaitTaskAllDone()
    {
        yield return new WaitUntil(() => _taskCount == _taskDoneCount);

        EpisodeManager.DoneSentenece();
    }

    public static void SetFirst(List<LineData> data = null)
    {
        Instance._taskConnect = new TaskConnect[data.Count];
        Instance._data = data;
        Instance._taskDoneCount = 0;
        Instance._taskCount = data.Count;

        ChMakingManager.Instance.SetDefault(0, Instance._data[0].Ch1Info);
        ChMakingManager.Instance.SetDefault(1, Instance._data[0].Ch2Info);
        ChMakingManager.Instance.SetDefault(2, Instance._data[0].Ch3Info);
        ChMakingManager.Instance.SetDefault(3, Instance._data[0].Ch4Info);

        Image BG  = EpisodeManager.GetInGameObjPack().BG.GetComponent<Image>();
        BG.sprite = ResourceManager.GetSprite(Instance._data[0].BgInfo.BgImage);
        BG.color  = new Color(1f, 1f, 1f, Instance._data[0].BgInfo.Opacity);

        UI_Manager.GetUI<UI_InGame>().Get<TMP_Text>("Text").text = "";
        UI_Manager.GetUI<UI_InGame>().Get("TextBox").transform.localPosition = UI_Manager.DownUIPos;
    }

    static void ClearText()
    {
        UI_Manager.GetUI<UI_InGame>().Get<TMP_Text>("Text").text = "";
    }
}
