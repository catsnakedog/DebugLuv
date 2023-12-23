using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager // UI를 관리하는 Manager이다
{
    int _order = 0;

    Stack<UI_Popup> _popupStack = new Stack<UI_Popup>(); // 팝업같은 경우 Stack으로 관리해준다

    public GameObject Root{
        get{
            GameObject root = GameObject.Find("@UI_Root"); // UI같은 경우 @UI_Root에 자식으로 들어간다
        if(root == null)
            root = new GameObject{name = "@UI_Root"};
        return root;
        }
    }

    public void SetCanavas(GameObject go, bool sort = true) // 기본적인 Setting + 기존의 UI들과 충돌이 일어나지 않도록 Order를 관리해준다
    {
        Canvas canvas = Util.GetOrAddComponent<Canvas>(go);
        canvas.renderMode= RenderMode.ScreenSpaceOverlay;
        canvas.overrideSorting = true;
        CanvasScaler scaler = Util.GetOrAddComponent<CanvasScaler>(go);
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(2400, 1080); // 플레이어 지정 해상도
        Util.GetOrAddComponent<CanvasSizeFix>(go); // UI 해상도 지정

        if(sort)
            canvas.sortingOrder = ++_order;
        else
            canvas.sortingOrder = 0;
    }

    public T ShowSceneUI<T>(string name = null) where T : UI_Scene // Scene에서 가장 기본이 되는 SceneUI를 세팅한다
    {   
        if(string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"UI/Scene/{name}"); // SceneUI같은 경우 Resources/UI/Scene에 저장한다
        T sceneUI = Util.GetOrAddComponent<T>(go);

        go.transform.SetParent(Root.transform);

        Managers.Scene.CurrentScene.SceneUI = sceneUI;
        return sceneUI;
    }

    public T ShowPopupUI<T>(string name = null) where T : UI_Popup // PopupUI를 세팅한다
    {   
        if(string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"UI/Popup/{name}"); // PopupUI같은 경우 Resources/UI/Popup에 저장한다
        T popup = Util.GetOrAddComponent<T>(go);
        _popupStack.Push(popup);

        go.transform.SetParent(Root.transform);


        return popup;
    }


    public void ClosePopupUI(UI_Popup popup) // 팝업을 닫는다
    {
        if(_popupStack.Count == 0) // 스택에 팝업이 없을 경우 리턴한다
            return;

        if(_popupStack.Peek() != popup) // 가장 위에 있는 팝업이 닫을려는 팝업이 아닌 경우 리턴한다
        {
            Debug.LogWarning("Close Popup Failed");
            return;
        }

        ClosePopupUI();
    }

    public void ClosePopupUI() // 가장 위에 있는 팝업을 닫는다
    {
        if(_popupStack.Count==0)
            return;

        UI_Popup popup = _popupStack.Pop();
        Managers.Resource.Destroy(popup.gameObject);
        popup = null;

        _order--;
    }

    public void CloseALLPopupUI() // 모든 팝업을 닫는다
    {
        while(_popupStack.Count>0)
            ClosePopupUI();
    }

    public void Clear()
    {
        CloseALLPopupUI();
    }
}
