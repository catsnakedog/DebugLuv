//-------------------------------------------------------------------------------------------------
// @file	Managers.cs
//
// @brief	�޴��� ???
//
// @date	2024-03-14
//
// Copyright 2024 Team One-eyed Games. All Rights Reserved.
//-------------------------------------------------------------------------------------------------


using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary> �޴��� ������Ʈ </summary>
public class Managers : MonoBehaviour // ���� ������ �ʿ�� ����
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
            return; // ���ϼ� ����

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
    /// ��ü Clear
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
    /// ���� Clear 
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
