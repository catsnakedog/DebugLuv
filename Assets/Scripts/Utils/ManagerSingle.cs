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
            if (_instance == null)
            {
                _instance = FindObjectOfType<ManagerType>();
                if (_instance == null)
                {
                    GameObject managers = GameObject.Find("@Managers");
                    if (managers == null)
                    {
                        GameObject root = GameObject.Find("@Root");
                        if (root == null)
                            root = new GameObject("@Root");

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