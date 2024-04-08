//-------------------------------------------------------------------------------------------------
// @file	UI_Manager.cs
//
// @brief	UI를 위한 매니저
//
// @date	2024-03-14
//
// Copyright 2024 Team One-eyed Games. All Rights Reserved.
//-------------------------------------------------------------------------------------------------


using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class UI_Manager : ManagerSingle<UI_Manager>, IClearable // UI를 관리하는 Manager이다
{
    public static Vector2 ScreenSize = new Vector2(1920, 1080);
    public static Vector3 DownUIPos  = new Vector3(0, -740, 0);
    public static Vector3 UpUIPos    = new Vector3(0, -370, 0);

    private Stack<UI_Base> _popupStack = new(); // 팝업같은 경우 Stack으로 관리해준다

    private UI_Base       _sceneUI;
    private UI_BackGround _backGroundUI;
    
    private int _order = 70;

    public enum UITypes
    {
        None,
        Default,
        BackGround,
    }

    /// <summary>
    /// < Type > UI를 반환한다.
    /// </summary>
    /// <typeparam name="UIType"> 해당 타입을 반환함 </typeparam>
    /// <returns></returns>
    public static UIType GetUI<UIType>() where UIType : UI_Base
    {
        if (Instance._sceneUI is UIType) // SceneUI 인지 검사
            return Instance._sceneUI as UIType;

        foreach(UI_Base popup in Instance._popupStack) // PopupUI 인지 검사
        {
            if (popup is UIType)
                return popup as UIType;
        }

        Debug.LogWarning($"error_UI_Manager : {typeof(UIType).ToString()} UI는 존재하지 않습니다.");
        return null;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static UI_BackGround GetBackGroundUI()
    {
        if(Instance._backGroundUI == null)
        {
            Instance.ShowSceneUI("BackGround", UITypes.BackGround);
        }
        
        return Instance._backGroundUI;
    }

    /// <summary> UI Root </summary>
    public GameObject Root
    {
        get
        {
            GameObject root = GameObject.Find("@UI_Root"); // UI같은 경우 @UI_Root에 자식으로 들어간다
            if (root == null)
            {
                root = new GameObject { name = "@UI_Root" };
            }
            return root;
        }
    }

    public void SetCanavas(GameObject go, bool sort = true ,int DirectSortOrder = -1) // 기본적인 Setting + 기존의 UI들과 충돌이 일어나지 않도록 Order를 관리해준다
    {
        Canvas canvas       = Util.GetOrAddComponent<Canvas>(go);
        CanvasScaler scaler = Util.GetOrAddComponent<CanvasScaler>(go);
        
        canvas.overrideSorting     = true;
        canvas.renderMode          = RenderMode.ScreenSpaceCamera;
        canvas.worldCamera         = Camera.main;
        scaler.uiScaleMode         = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = ScreenSize; // 플레이어 지정 해상도

        if(sort)
            canvas.sortingOrder = ++_order;
        else
            canvas.sortingOrder = 0;

        if(DirectSortOrder != -1 )
            canvas.sortingOrder = DirectSortOrder;

    }

    /// <summary>
    /// Scene 에 UI를 세팅한다. 
    /// </summary>
    /// <param name="name"> Scene UI를 세팅한다. </param>
    /// <param name="isDefault"> Default UI로 사용한다. </param>
    public void ShowSceneUI(string name = null, UITypes Type = UITypes.Default) // Scene에서 가장 기본이 되는 SceneUI를 세팅한다
    {   
        GameObject go = ResourceManager.Instance.Instantiate($"Prefabs/UI/Scene/UI_{name}"); // SceneUI같은 경우 Resources/UI/Scene에 저장한다
        if (go == null)
        {
            Debug.LogWarning($"error_UI_Manager : {name} Scene의 Scene UI 생성을 실패했습니다.");
            return;
        }
        go.transform.SetParent(Root.transform);
        
        switch (Type)
        {
            case UITypes.Default:
                SetCanavas(go);
                _sceneUI = go.GetComponent<UI_Base>();

                break;

            case UITypes.BackGround:
                SetCanavas(go, true, 0);
                _backGroundUI = go.GetComponent<UI_BackGround>();
                
                break;

            default:
                
                break;

        }
    }

    public void ShowPopupUI(string name = null) // PopupUI를 세팅한다
    {
        GameObject go = ResourceManager.Instance.Instantiate($"Prefabs/UI/Popup/UI_{name}"); // SceneUI같은 경우 Resources/UI/Popup에 저장한다
        if (go == null)
        {
            Debug.LogWarning($"error_UI_Manager : UI_{name} Popup 생성을 실패했습니다.");
            return;
        }
        SetCanavas(go);
        go.transform.SetParent(Root.transform);
        
        _popupStack.Push(go.GetComponent<UI_Base>());
    }


    public void ClosePopupUI(UI_Base popup) // 팝업을 닫는다
    {
        if(_popupStack.Count == 0) // 스택에 팝업이 없을 경우 리턴한다
            return;

        if(_popupStack.Peek() != popup) // 가장 위에 있는 팝업이 닫을려는 팝업이 아닌 경우 리턴한다
        {
            Debug.LogWarning($"error_UI_Manager : {popup.name} Popup은 최상단에 위치하지 않습니다.");
            return;
        }

        ClosePopupUI();
    }

    public void ClosePopupUI() // 가장 위에 있는 팝업을 닫는다
    {
        if(_popupStack.Count == 0)
            return;

        UI_Base popup = _popupStack.Pop();
        Destroy(popup.gameObject);
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
