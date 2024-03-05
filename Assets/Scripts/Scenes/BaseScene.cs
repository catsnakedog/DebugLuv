using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public abstract class BaseScene : MonoBehaviour // Scene�� ���� �⺻�� �Ǵ� class�̴�
{
    private void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {
        Object obj =  GameObject.FindObjectOfType(typeof(EventSystem));
        if(obj == null)
            ResourceManager.Instance.Instantiate("UI/EventSystem").name = "@EventSystem"; // EventSystem�� �߰��Ѵ�

        UI_Manager.Instance.ShowSceneUI(SceneManager.GetActiveScene().name);
    }
}