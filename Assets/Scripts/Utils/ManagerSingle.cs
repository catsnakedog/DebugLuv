using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerSingle<ManagerType> : ManagerBase where ManagerType : ManagerBase
{
    private static ManagerType _instance;

    public static ManagerType Instance
    {
        get
        {
            if (_instance == null) // 자동화 - Manager중 아무나 호출해도 전부 세팅이 되도록 자동화 해둔 것이다
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
            }

            Managers.Init<ManagerType>();
            return _instance;
        }
    }
}