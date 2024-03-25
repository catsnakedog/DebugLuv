//-------------------------------------------------------------------------------------------------
// @file	EpisodeManager.cs
//
// @brief	���Ǽҵ带 ���� �Ŵ���
//
// @date	2024-03-14
//
// Copyright 2024 Team One-eyed Games. All Rights Reserved.
//-------------------------------------------------------------------------------------------------


using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


/// <summary> ���Ǽҵ带 ���� �Ŵ��� </summary>
public class EpisodeManager : ManagerSingle<EpisodeManager>
{
    /// <summary> �ϳ��� ������ ���� ������ </summary>
    public class InGameObjPack
    {
        /// <summary> �߰� �۾� </summary>
        public GameObject Etc;
        /// <summary> ��׶��� </summary>
        public GameObject BG;
        /// <summary> ĳ���� 1�� </summary>
        public GameObject Ch1;
        /// <summary> ĳ���� 2�� </summary>
        public GameObject Ch2;
        /// <summary> ĳ���� 3�� </summary>
        public GameObject Ch3;
        /// <summary> ĳ���� 4�� </summary>
        public GameObject Ch4;
        /// <summary> Storage ��ü ����� </summary>
        public List<GameObject> Storage;

        /// <summary>
        /// InGameObjPack ������
        /// </summary>
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

    /// <summary> InGameObjPack </summary>
    InGameObjPack _inGameObjPack;
    /// <summary> EpisodeData </summary>
    EpisodeData _episodeData;

    /// <summary> ���� ���� ���� Flag </summary>
    bool isSentenceDone;
    /// <summary> ���� ���� ���� Flag </summary>
    public static bool IsSentenceDone { get { return Instance.isSentenceDone; } set { Instance.isSentenceDone = value; } }

    /// <summary> ?? </summary>
    bool isNext;
    /// <summary> ?? </summary>
    public static bool IsNext { get { return Instance.isNext;  } set { Instance.isNext = value; } }

    /// <summary> �б�? </summary>
    int branch;
    /// <summary> �б�? </summary>
    public static int Branch { get { return Instance.branch; } set { Instance.branch = value;  } }

    /// <summary> ���� ����?? </summary>
    int sentence;
    /// <summary> ���� ����?? </summary>
    public static int Sentence { get { return Instance.sentence; } set { Instance.sentence = value; } }


    /// <summary>
    /// ���Ǽҵ� ����
    /// </summary>
    /// <param name="data"></param>
    /// <param name="branch"></param>
    /// <param name="setence"></param>
    public static void StartEpisode(EpisodeData data, int branch, int setence)
    {
        SetInGameObj();
        Branch = branch;
        Sentence = setence;
        Instance._episodeData = data;
        Instance.StartCoroutine(Instance.Episode(data));
    }

    /// <summary>
    /// ���Ǽҵ� �ʱ�ȭ
    /// </summary>
    public static void SetInGameObj()
    {
        Instance._inGameObjPack = new InGameObjPack(GameObject.Find("Etc") ,GameObject.Find("BG"), GameObject.Find("Ch1"), GameObject.Find("Ch2"), GameObject.Find("Ch3"), GameObject.Find("Ch4"), new());
    }

    /// <summary>
    /// ���Ǽҵ� ���� Enumerator 
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    private IEnumerator Episode(EpisodeData data)
    {
        for(int i = Sentence; i < data.Setence[Branch].Count; i++)
        {
            Sentence = i;
            IsNext = false;
            IsSentenceDone = false;
            TaskManager.Instance.StartSetenceTasks(data.Setence[Branch][i]);
            yield return new WaitUntil(CheckSentenceDone);
            yield return new WaitUntil(() => IsNext);
        }
        if (data.Setence[Branch][^1][^1].Choice != 0)
            UI_Manager.GetUI<UI_InGame>().ShowChoice(data.Setence[Branch][^1][^1].Choice);
        else
            EpisodeEnd();
    }

    /// <summary>
    ///  ���� �������� Ȯ��
    /// </summary>
    /// <returns></returns>
    private static bool CheckSentenceDone()
    {
        return IsSentenceDone;
    }

    /// <summary>
    /// ���� ���Ḧ ����
    /// </summary>
    public static void DoneSentenece()
    {
        IsSentenceDone = true;
    }

    /// <summary>
    /// GetInGameObjPack
    /// </summary>
    /// <returns>InGameObjPack</returns>
    public static InGameObjPack GetInGameObjPack()
    {
        return Instance._inGameObjPack;
    }


    /// <summary>
    /// ???
    /// </summary>
    /// <returns>InGameObjPack</returns>
    public static void EpisodeEnd()
    {

    }

    /// <summary>
    /// �б� ��ȯ
    /// </summary>
    /// <param name="num"></param>
    public static void ChangeBranch(int num)
    {
        Branch = num;
        Sentence = 0;
        Instance.StartCoroutine(Instance.Episode(Instance._episodeData));
    }

    /// <summary>
    /// Storage �ε��� ��ȿ�� �˻�
    /// </summary>
    /// <param name="idx"> �ε��� </param>
    /// <returns></returns>
    public static bool IsStorageAvailable(int idx)
    {
        if (GetInGameObjPack().Storage.Count > idx && idx >= 0) 
        {
            return true;
        }
        else
        {
            Util.DebugLogWarning($" Storage NOT Available {idx}, Index Must Under {GetInGameObjPack().Storage.Count}");
            return false;
        }
    }
}