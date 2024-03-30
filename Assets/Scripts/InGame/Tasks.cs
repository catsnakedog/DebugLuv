using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using static TaskManager;


public class Tasks : MonoBehaviour
{
    [SerializeField]
    private TaskConnect _taskConnect;
    [SerializeField]
    private LineData _setenceData;
    [SerializeField]
    private int _LinecompleteCnt;
    [SerializeField]
    private List<List<string>> _taskOrder;
    [SerializeField]
    private int[] _isDoneLineTask;

    /// <summary>
    /// 단일 Task Data Set
    /// </summary>
    /// <param name="taskConnect"> TaskConnect </param>
    /// <param name="setenceData"> LineData </param>
    public void Set(TaskConnect taskConnect, LineData setenceData)
    {
        _taskConnect = taskConnect;
        _setenceData = setenceData;

        StartCoroutine(RunTasks());
    }

    private IEnumerator RunTasks()
    {
        _taskOrder = SortTaskOrder();
        _LinecompleteCnt = 0;
        for(int i = 0; i < _taskOrder.Count; i++)
        {
            StartCoroutine(RunLineTask(_taskOrder[i], i));
        }
        RunParallel();

        yield return new WaitUntil(() => _LinecompleteCnt == _taskOrder.Count);

        TaskManager.Instance.DoneTask();
        RunSequential();
    }

    private void RunParallel()
    {
        foreach (int num in _taskConnect.ParallelConnect)
            TaskManager.Instance.RunTasks(num);
    }

    private void RunSequential()
    {
        foreach (int num in _taskConnect.SequentialConnect)
            TaskManager.Instance.RunTasks(num);
    }

    private List<List<string>> SortTaskOrder()
    {
        List<List<string>> taskOrder = new();

        string[] lines = _setenceData.TasksOrder.Split("\n");
        _LinecompleteCnt = lines.Length;
        _isDoneLineTask = new int[lines.Length];
        for (int i = 0; i < lines.Length; i++)
            _isDoneLineTask[i] = 0;

        foreach (string line in lines)
        {
            taskOrder.Add(new());
            string[] tasks = line.Trim().Split("-");
            taskOrder[^1] = new List<string>(tasks);
        }

        return taskOrder;
    }

    private IEnumerator RunLineTask(List<string> line, int num)
    {
        foreach(string tasks in line)
        {
            _isDoneLineTask[num] = 0;

            int targetComplete = tasks.Split("/").Length;

            tasks.Trim();
            foreach(string task in tasks.Split("/"))
                RunTask(task, () => { DoneLineTask(num, targetComplete); });

            yield return new WaitUntil(() => TaskEnd(num, targetComplete));
        }

        LineComplete();
    }

    private void RunTask(string task, Action done)
    {
        switch (task)
        {
            case "Etc":
                EtcTask(task, done);
                break;
            case "BG":
                BgTask(task, done);
                break;
            case "Text":
                TextTask(task, done);
                break;
            case "Ch1":
                Ch1Task(task, done);
                break;
            case "Ch2":
                Ch2Task(task, done);
                break;
            case "Ch3":
                Ch3Task(task, done);
                break;
            case "Ch4":
                Ch4Task(task, done);
                break;
            default:
                Debug.LogWarning($"error_Tasks : {task}라는 Task는 없습니다.");
                break;
        }
    }

    #region Tasks
    private void EtcTask(string task, Action done)
    {
        Type classType = Type.GetType(_setenceData.EtcTask.Name);
        if (_setenceData.Storage != -1 && EpisodeManager.IsStorageAvailable(_setenceData.Storage))
        {
            EpisodeManager.ScheduleDropStorageIdx(_setenceData.Storage);
            EffectManager.Instance.PlayEffect(classType, EpisodeManager.GetInGameObjPack().Storage[_setenceData.Storage], _setenceData.EtcTask.Value, done);
        }
        else
            EffectManager.Instance.PlayEffect(classType, EpisodeManager.GetInGameObjPack().Etc, _setenceData.EtcTask.Value, done);
    }

    private void BgTask(string task, Action done)
    {
        Type classType = Type.GetType(_setenceData.BgTask.Name);
        EffectManager.Instance.PlayEffect(classType, EpisodeManager.GetInGameObjPack().BG, _setenceData.BgTask.Value, done);
    }

    private void Ch1Task(string task, Action done)
    {
        Type classType = Type.GetType(_setenceData.Ch1Task.Name);
        _setenceData.Ch1Info.ChIndex = (int)ChMakingManager.ChIndex.Ch1;
        EffectManager.Instance.PlayEffect(classType, EpisodeManager.GetInGameObjPack().Ch1, _setenceData.Ch1Task.Value, done, _setenceData.Ch1Info);
    }

    private void Ch2Task(string task, Action done)
    {
        Type classType = Type.GetType(_setenceData.Ch2Task.Name);
        _setenceData.Ch2Info.ChIndex = (int)ChMakingManager.ChIndex.Ch2;
        EffectManager.Instance.PlayEffect(classType, EpisodeManager.GetInGameObjPack().Ch2, _setenceData.Ch2Task.Value, done, _setenceData.Ch2Info);
    }

    private void Ch3Task(string task, Action done)
    {
        Type classType = Type.GetType(_setenceData.Ch3Task.Name);
        _setenceData.Ch3Info.ChIndex = (int)ChMakingManager.ChIndex.Ch3;
        EffectManager.Instance.PlayEffect(classType, EpisodeManager.GetInGameObjPack().Ch3, _setenceData.Ch3Task.Value, done, _setenceData.Ch3Info);
    }

    private void Ch4Task(string task, Action done)
    {
        Type classType = Type.GetType(_setenceData.Ch4Task.Name);
        _setenceData.Ch4Info.ChIndex = (int)ChMakingManager.ChIndex.Ch4;
        EffectManager.Instance.PlayEffect(classType, EpisodeManager.GetInGameObjPack().Ch4, _setenceData.Ch4Task.Value, done, _setenceData.Ch4Info);
    }

    private void TextTask(string task, Action done)
    {
        UI_Manager.GetUI<UI_InGame>().SetName(_setenceData.Name);
        UI_Manager.GetUI<UI_InGame>().ShowTextEffect(_setenceData.Text, done);
    }

    private void DoneLineTask(int num, int targetComplete)
    {
        _isDoneLineTask[num]++;
    }

    private bool TaskEnd(int num, int targetComplete)
    {
        return _isDoneLineTask[num] == targetComplete;
    }

    private void LineComplete()
    {
        _LinecompleteCnt++;
    }
    #endregion
}