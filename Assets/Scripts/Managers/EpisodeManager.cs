using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    private EpisodeData _episodeData;

    public bool IsSetenceDone;
    public bool IsNext;

    public int Branch;
    public int Setence;

    public void StartEpisode(EpisodeData data, int branch, int setence)
    {
        SetInGameObj();
        Branch = branch;
        Setence = setence;
        _episodeData = data;
        StartCoroutine(Episode(data));
    }

    public void SetInGameObj()
    {
        _inGameObjPack = new InGameObjPack(GameObject.Find("Etc") ,GameObject.Find("BG"), GameObject.Find("Ch1"), GameObject.Find("Ch2"), GameObject.Find("Ch3"), GameObject.Find("Ch4"), new());
    }

    private IEnumerator Episode(EpisodeData data)
    {
        for(int i = Setence; i < data.Setence[Branch].Count; i++)
        {
            Setence = i;
            IsNext = false;
            IsSetenceDone = false;
            TaskManager.Instance.StartSetenceTasks(data.Setence[Branch][i]);
            yield return new WaitUntil(CheckSetenceDone);
            yield return new WaitUntil(() => IsNext);
        }
        if (data.Setence[Branch][^1][^1].Choice != 0)
            UI_Manager.Instance.GetUI<UI_InGame>().ShowChoice(data.Setence[Branch][^1][^1].Choice);
        else
            EpisodeEnd();
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

    public void EpisodeEnd()
    {

    }

    public void ChangeBranch(int num)
    {
        Branch = num;
        Setence = 0;
        StartCoroutine(Episode(_episodeData));
    }
}