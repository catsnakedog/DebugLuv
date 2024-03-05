using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EpisodeManager
{
    public void Test(EpisodeData data)
    {
        foreach(var a in data.Setence)
        {
            TaskManager.Instance.StartSetenceTasks(a);
        }
    }
}
