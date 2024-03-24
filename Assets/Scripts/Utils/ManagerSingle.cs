//-------------------------------------------------------------------------------------------------
// @file	ManagerSingle.cs
//
// @brief	Singleton �� ����ϴ� Manager 
//
// @date	2024-03-14
//
// Copyright 2024 Team One-eyed Games. All Rights Reserved.
//-------------------------------------------------------------------------------------------------


using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ManagerSingle<ManagerType> : ManagerBase where ManagerType : ManagerBase
{
    /// <summary> </summary>
    private static ManagerType _instance;


    public static ManagerType Instance
    {
        get
        {
            if (_instance == null) // �ڵ�ȭ - Manager�� �ƹ��� ȣ���ص� ���� ������ �ǵ��� �ڵ�ȭ �ص� ���̴�
            {
                _instance = FindObjectOfType<ManagerType>();
                if (_instance == null)
                {
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
                    _instance = managers.AddComponent<ManagerType>();
                }

                Managers.Init<ManagerType>();
            }

            return _instance;
        }
    }
}