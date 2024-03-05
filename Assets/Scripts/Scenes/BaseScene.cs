using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public abstract class BaseScene : MonoBehaviour // Scene의 가장 기본이 되는 class이다
{
    private void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {
        Object obj =  GameObject.FindObjectOfType(typeof(EventSystem));
        if(obj == null)
            ResourceManager.Instance.Instantiate("UI/EventSystem").name = "@EventSystem"; // EventSystem를 추가한다

        UI_Manager.Instance.ShowSceneUI(SceneManager.GetActiveScene().name);
    }
}