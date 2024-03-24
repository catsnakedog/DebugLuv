//-------------------------------------------------------------------------------------------------
// @file	Managers.cs
//
// @brief	메니저 ???
//
// @date	2024-03-14
//
// Copyright 2024 Team One-eyed Games. All Rights Reserved.
//-------------------------------------------------------------------------------------------------


using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary> 메니저 컴포넌트 </summary>
public class Managers : MonoBehaviour // 따로 부착할 필요는 없음
{
    /// <summary> Singleton instance </summary>
    private static Managers _instance;
    /// <summary> Get Singleton instance </summary>
    public static Managers Instance
    {
        get { Init(); return _instance; }
    }

    /// <summary>
    /// Awake
    /// </summary>
    private void Awake()
    {
        Init();
    }

    /// <summary>
    /// Initialize
    /// </summary>
    static void Init()
    {
        if (_instance != null)
            return; // 유일성 보장

        GameObject managers = GameObject.Find("@Managers");
        if (managers == null)
        {
            GameObject root = GameObject.Find("@Root");
            if (root == null)
            {
                root = new GameObject("@Root");
                DontDestroyOnLoad(root);
            }

            managers = new GameObject("@Managers");
            managers.transform.SetParent(root.transform);
            managers.AddComponent<Managers>();
        }

        _instance = managers.GetComponent<Managers>();
    }

    /// <summary>
    /// 전체 Clear
    /// </summary>
    public static void ClearAll()
    {
        ManagerBase[] managers = _instance.transform.GetComponents<ManagerBase>();

        foreach (ManagerBase manager in managers)
        {
            if(manager is IClearable)
            {
                (manager as IClearable).Clear();
            }
        }
    }

    /// <summary>
    /// 단일 Clear 
    /// </summary>
    /// <typeparam name="ManagerType"></typeparam>
    public static void Clear<ManagerType>() where ManagerType : ManagerBase
    {
        ManagerType manager = _instance.transform.GetComponent<ManagerType>();

        if (manager is IClearable)
            (manager as IClearable).Clear();
    }

    /// <summary>
    /// Managers Initialize
    /// </summary>
    /// <typeparam name="ManagerType"></typeparam>
    public static void Init<ManagerType>() where ManagerType : ManagerBase
    {
        ManagerType manager = _instance.transform.GetComponent<ManagerType>();

        if (manager is IInit)
            (manager as IInit).Init();
    }
}
