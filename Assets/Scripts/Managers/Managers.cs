using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour // ���� ������ �ʿ�� ����
{
    private static Managers _instance;
    public static Managers Instance
    {
        get { Init(); return _instance; }
    }

    private void Awake()
    {
        Init();
    }

    static void Init()
    {
        if (_instance != null)
            return; // ���ϼ� ����

        GameObject managers = GameObject.Find("@Managers");
        if (managers == null)
        {
            GameObject root = GameObject.Find("@Root");
            if (root == null)
                root = new GameObject("@Root");

            DontDestroyOnLoad(root);

            managers = new GameObject("@Managers");
            managers.transform.SetParent(root.transform);
            managers.AddComponent<Managers>();
        }

        _instance = managers.GetComponent<Managers>();
    }

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

    public static void Clear<ManagerType>() where ManagerType : ManagerBase
    {
        ManagerType manager = _instance.transform.GetComponent<ManagerType>();

        if (manager is IClearable)
            (manager as IClearable).Clear();
    }

    public static void Init<ManagerType>() where ManagerType : ManagerBase
    {
        ManagerType manager = _instance.transform.GetComponent<ManagerType>();

        if (manager is IInit)
            (manager as IInit).Init();
    }
}
