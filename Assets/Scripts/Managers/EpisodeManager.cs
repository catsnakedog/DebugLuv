//-------------------------------------------------------------------------------------------------
// @file	EpisodeManager.cs
//
// @brief	에피소드를 위한 매니저
//
// @date	2024-03-14
//
// Copyright 2024 Team One-eyed Games. All Rights Reserved.
//-------------------------------------------------------------------------------------------------


using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


/// <summary> 에피소드를 위한 매니저 </summary>
public class EpisodeManager : ManagerSingle<EpisodeManager>
{
    /// <summary> 하나의 문장을 위한 데이터 </summary>
    public class InGameObjPack
    {
        /// <summary> 추가 작업 </summary>
        public GameObject Etc;
        /// <summary> 백그라운드 </summary>
        public GameObject BG;
        /// <summary> 캐릭터 1번 </summary>
        public GameObject Ch1;
        /// <summary> 캐릭터 2번 </summary>
        public GameObject Ch2;
        /// <summary> 캐릭터 3번 </summary>
        public GameObject Ch3;
        /// <summary> 캐릭터 4번 </summary>
        public GameObject Ch4;
        /// <summary> Storage 객체 저장용 </summary>
        public Dictionary<int,GameObject> Storage;

        /// <summary>
        /// InGameObjPack 생성자
        /// </summary>
        public InGameObjPack(GameObject etc, GameObject bG, GameObject ch1, GameObject ch2, GameObject ch3, GameObject ch4, Dictionary<int, GameObject> storage)
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

    /// <summary> 현재 문장 종료 Flag </summary>
    bool isSentenceDone;
    /// <summary> 현재 문장 종료 Flag </summary>
    public static bool IsSentenceDone { get { return Instance.isSentenceDone; } set { Instance.isSentenceDone = value; } }

    /// <summary> ?? </summary>
    bool isNext;
    /// <summary> ?? </summary>
    public static bool IsNext { get { return Instance.isNext;  } set { Instance.isNext = value; } }

    /// <summary> 분기? </summary>
    int branch;
    /// <summary> 분기? </summary>
    public static int Branch { get { return Instance.branch; } set { Instance.branch = value;  } }

    /// <summary> 문장 갯수?? </summary>
    int sentence;
    /// <summary> 문장 갯수?? </summary>
    public static int Sentence { get { return Instance.sentence; } set { Instance.sentence = value; } }

    /// <summary> 삭제하려는 스토리지 인덱스 보관 </summary>
    List<int> dropStorageIndex = new();

    /// <summary>
    /// 에피소드 시작 
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
    /// 에피소드 초기화
    /// </summary>
    public static void SetInGameObj()
    {
        Instance._inGameObjPack = new InGameObjPack(GameObject.Find("Etc") ,GameObject.Find("BG"), GameObject.Find("Ch1"), GameObject.Find("Ch2"), GameObject.Find("Ch3"), GameObject.Find("Ch4"), new());
    }

    /// <summary>
    /// 에피소드 진행 Enumerator 
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    private IEnumerator Episode(EpisodeData data)
    {
        WaitUntil waitUntilCheckSentenceDone = new WaitUntil(CheckSentenceDone);
        WaitUntil waitUntilIsNext            = new WaitUntil(() => IsNext);

        TaskManager.SetFirst(data.Setence[Branch][Sentence]);

        for (int i = Sentence; i < data.Setence[Branch].Count; i++)
        {
            Sentence = i;
            IsNext = false;
            IsSentenceDone = false;
            TaskManager.StartSetenceTasks(data.Setence[Branch][i]);
            yield return waitUntilCheckSentenceDone;
            yield return waitUntilIsNext;
        }
        if (data.Setence[Branch][^1][^1].Choice != 0)
            UI_Manager.GetUI<UI_InGame>().ShowChoice(data.Setence[Branch][^1][^1].Choice);
        else
            EpisodeEnd();
    }

    /// <summary>
    /// 문장이 종료인지 확인한다.
    /// </summary>
    /// <returns></returns>
    private static bool CheckSentenceDone()
    {
        return IsSentenceDone;
    }

    /// <summary>
    /// 문장 종료를 선언
    /// </summary>
    public static void DoneSentenece()
    {
        //EpisodeManager.DropStorageAll();
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
    /// 분기 변환
    /// </summary>
    /// <param name="num"></param>
    public static void ChangeBranch(int num)
    {
        Branch = num;
        Sentence = 0;
        Instance.StartCoroutine(Instance.Episode(Instance._episodeData));
    }

    /// <summary>
    /// Storage 인덱스 유효성 검사
    /// </summary>
    /// <param name="idx"> 인덱스 </param>
    /// <returns></returns>
    public static bool IsStorageAvailable(int idx)
    {
        if (GetInGameObjPack().Storage != null && GetInGameObjPack().Storage.ContainsKey(idx)) 
        {
            return true;
        }
        else
        {
            Util.DebugLogWarning($" Storage NOT Available {idx}");
            return false;
        }
    }

    /// <summary>
    /// 삭제하려는 Index를 등록
    /// </summary>
    /// <param name="idx"></param>
    public static void ScheduleDropStorageIdx(int idx)
    {
        if (Instance.dropStorageIndex != null && !Instance.dropStorageIndex.Contains(idx))
        {
            Instance.dropStorageIndex.Add(idx);
        }
    }

    public static bool ObjLockStorageIdx(int idx)
    {
        if (Instance.dropStorageIndex != null && Instance.dropStorageIndex.Contains(idx))
        {
            Instance.dropStorageIndex.Remove(idx);
            return true;
        }
        else 
        {
            Util.DebugLogError($"보관하려는 {idx} 에 문제가 있음");
            return false;
        }
    }

    /// <summary>
    /// 스토리지에서 예약한 Storage 전체 삭제
    /// </summary>
    public static void DropStorageAll()
    {
        if (Instance.dropStorageIndex == null) return;

        foreach(int dropIdx in Instance.dropStorageIndex )
        {
            if (!GetInGameObjPack().Storage.ContainsKey(dropIdx)) continue;
            
            GameObject dropObj = GetInGameObjPack().Storage[dropIdx];
            GetInGameObjPack().Storage.Remove(dropIdx);
            Destroy(dropObj);
        }
    }

    /// <summary>
    /// 스토리지에서 예약한 Storage 전체 Lock
    /// </summary>
    public static void ObjLockStorageAll()
    {
        if (Instance.dropStorageIndex == null) return;

        Instance.dropStorageIndex.Clear();
    }

    /// <summary>
    /// 스토리지에서 예약한 Storage 전체 삭제
    /// </summary>
    public static void DropStorageIdxDirect(int dropIdx)
    {
        if (!GetInGameObjPack().Storage.ContainsKey(dropIdx))
        {
            Util.DebugLogWarning($"삭제할 Index: {dropIdx} 가 없음");
            return;
        }

        GameObject dropObj = GetInGameObjPack().Storage[dropIdx];
        GetInGameObjPack().Storage.Remove(dropIdx);
        Destroy(dropObj);
    }

}