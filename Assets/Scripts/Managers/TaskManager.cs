using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : ManagerSingle<TaskManager>
{
    public class TaskLogic
    {
        public List<int> AddLogic;
        public List<int> ExtraLogic;

        public TaskLogic()
        {
            AddLogic = new();
            ExtraLogic = new();
        }
    }

    private TaskLogic[] _taskLogic;
    private List<Tasks> _tasks;
    private List<SetenceData> _data;

    public List<GameObject> Storage = new();

    public void StartSetenceTasks(List<SetenceData> data)
    {
        _taskLogic = new TaskLogic[data.Count];
        _tasks = new();
        _data = data;

        for(int i = 0; i < _taskLogic.Length; i++)
        {
            _taskLogic[i] = new TaskLogic();
            _taskLogic[i].AddLogic = new();
            _taskLogic[i].ExtraLogic = new();
        }

        for(int i = data.Count - 1; i >= 0; i--)
        {
            if (data[i].Plus == -1)
                break;
            for (int j = data.Count - 1; j >= 0; j--)
            {
                if (data[j].Plus == -1)
                {
                    if (data[i].Type == "Add")
                        _taskLogic[j].AddLogic.Add(i);
                    else
                        _taskLogic[j].ExtraLogic.Add(i);

                    break;
                }
                if (data[j].Plus < data[i].Plus)
                {
                    if (data[i].Type == "Add")
                        _taskLogic[j].AddLogic.Add(i);
                    else
                        _taskLogic[j].ExtraLogic.Add(i);

                    break;
                }
            }
        }

        RunTasks(0);
    }

    public void RunTasks(int num)
    {
        _tasks.Add(new Tasks(_taskLogic[num], _data[num]));
    }
}
