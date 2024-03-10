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

        SetFirst();

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

    private void SetFirst()
    {
        ChMakingManager.Instance.SetDefault(0, _data[0].Ch1Info);
        ChMakingManager.Instance.SetDefault(1, _data[0].Ch2Info);
        ChMakingManager.Instance.SetDefault(2, _data[0].Ch3Info);
        ChMakingManager.Instance.SetDefault(3, _data[0].Ch4Info);

        UI_Manager.Instance.GetUI<UI_InGame>().Get<Image>("BG").sprite = ResourceManager.Instance.LoadSprite(_data[0].BgInfo.BgImage);
        UI_Manager.Instance.GetUI<UI_InGame>().Get<Image>("BG").color = new Color(1f, 1f, 1f, _data[0].BgInfo.Opacity);
        UI_Manager.Instance.GetUI<UI_InGame>().Get<TMP_Text>("Text").text = "";
        if (_data[0].StateUI == "Hide")
            UI_Manager.Instance.GetUI<UI_InGame>().Get("TextBox").transform.localPosition = new Vector3(0, -740, 0);
        else
            UI_Manager.Instance.GetUI<UI_InGame>().Get("TextBox").transform.localPosition = new Vector3(0, -370, 0);
    }
}
