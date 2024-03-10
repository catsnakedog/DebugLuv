using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : ManagerSingle<TaskManager>
{
    public class TaskConnect
    {
        public List<int> ParallelConnect;
        public List<int> SequentialConnect;

        public TaskConnect()
        {
            ParallelConnect = new();
            SequentialConnect = new();
        }
    }

    private TaskConnect[] _taskConnect;
    private List<Tasks> _tasks;
    private List<LineData> _data;
    private GameObject _tasksRoot;

    public int _taskDoneCount;
    public int _taskCount;

    public void StartSetenceTasks(List<LineData> data)
    {
        _taskConnect = new TaskConnect[data.Count];
        _data = data;
        _taskDoneCount = 0;
        _taskCount = data.Count;

        for (int i = 0; i < _taskConnect.Length; i++)
        {
            _taskConnect[i] = new TaskConnect();
            _taskConnect[i].ParallelConnect = new();
            _taskConnect[i].SequentialConnect = new();

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
                        _taskConnect[i].SequentialConnect.Add(num);
                        break;
                    case "P":
                        _taskConnect[i].ParallelConnect.Add(num);
                        break;
                    default:
                        Debug.LogWarning($"error_TaskManager : Connect Type이 이상합니다.");
                        break;
                }
            }
        }

        _tasksRoot = new GameObject("@Tasks");

        if(_tasks == null)
        {
            _tasks = new();
            for (int i = 0; i < 5; i++)
            {
                Tasks tasks = _tasksRoot.AddComponent<Tasks>();
                _tasks.Add(tasks);
            }
        }

        RunTasks(0);

        StartCoroutine(WaitTaskAllDone());
    }

    public void RunTasks(int num)
    {
        while(_tasks.Count <= num)
        {
            Tasks tasks = _tasksRoot.AddComponent<Tasks>();
            _tasks.Add(tasks);
        }

        _tasks[num].Set(_taskConnect[num], _data[num]);
    }

    public void DoneTask()
    {
        _taskDoneCount++;
    }

    private IEnumerator WaitTaskAllDone()
    {
        yield return new WaitUntil(() => _taskCount == _taskDoneCount);

        EpisodeManager.Instance.DoneSetenece();
    }
}
