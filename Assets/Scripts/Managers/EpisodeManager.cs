using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EpisodeManager : ManagerSingle<EpisodeManager>
{
    public class InGameObjPack
    {
        public GameObject Etc;
        public GameObject BG;
        public GameObject Ch1;
        public GameObject Ch2;
        public GameObject Ch3;
        public GameObject Ch4;
        public List<GameObject> Storage;

        public InGameObjPack(GameObject etc, GameObject bG, GameObject ch1, GameObject ch2, GameObject ch3, GameObject ch4, List<GameObject> storage)
        {
            Etc = etc;
            BG = bG;
            Ch1 = ch1;
            Ch2 = ch2;
            Ch3 = ch3;
            Ch4 = ch4;
            Storage = storage;
        }
    }

    private InGameObjPack _inGameObjPack;

    public bool IsSetenceDone;
    public bool IsNext;

    public void StartEpisode(EpisodeData data)
    {
        SetInGameObj();

        StartCoroutine(Episode(data));
    }

    public void SetInGameObj()
    {
        _inGameObjPack = new InGameObjPack(GameObject.Find("Etc") ,GameObject.Find("BG"), GameObject.Find("Ch1"), GameObject.Find("Ch2"), GameObject.Find("Ch3"), GameObject.Find("Ch4"), new());
    }

    private IEnumerator Episode(EpisodeData data)
    {
        foreach (List<LineData> setence in data.Setence)
        {
            IsNext = false;
            IsSetenceDone = false;
            TaskManager.Instance.StartSetenceTasks(setence);
            yield return new WaitUntil(CheckSetenceDone);
            yield return new WaitUntil(() => IsNext);
        }
    }

    private bool CheckSetenceDone()
    {
        return IsSetenceDone;
    }

    public void DoneSetenece()
    {
        IsSetenceDone = true;
    }

    public InGameObjPack GetInGameObjPack()
    {
        return _inGameObjPack;
    }
}