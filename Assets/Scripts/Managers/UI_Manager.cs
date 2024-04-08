//-------------------------------------------------------------------------------------------------
// @file	UI_Manager.cs
//
// @brief	UI�� ���� �Ŵ���
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


public class UI_Manager : ManagerSingle<UI_Manager>, IClearable // UI�� �����ϴ� Manager�̴�
{
    public static Vector2 ScreenSize = new Vector2(1920, 1080);
    public static Vector3 DownUIPos  = new Vector3(0, -740, 0);
    public static Vector3 UpUIPos    = new Vector3(0, -370, 0);

    private Stack<UI_Base> _popupStack = new(); // �˾����� ��� Stack���� �������ش�

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
    /// < Type > UI�� ��ȯ�Ѵ�.
    /// </summary>
    /// <typeparam name="UIType"> �ش� Ÿ���� ��ȯ�� </typeparam>
    /// <returns></returns>
    public static UIType GetUI<UIType>() where UIType : UI_Base
    {
        if (Instance._sceneUI is UIType) // SceneUI ���� �˻�
            return Instance._sceneUI as UIType;

        foreach(UI_Base popup in Instance._popupStack) // PopupUI ���� �˻�
        {
            if (popup is UIType)
                return popup as UIType;
        }

        Debug.LogWarning($"error_UI_Manager : {typeof(UIType).ToString()} UI�� �������� �ʽ��ϴ�.");
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
            GameObject root = GameObject.Find("@UI_Root"); // UI���� ��� @UI_Root�� �ڽ����� ����
            if (root == null)
            {
                root = new GameObject { name = "@UI_Root" };
            }
            return root;
        }
    }

    public void SetCanavas(GameObject go, bool sort = true ,int DirectSortOrder = -1) // �⺻���� Setting + ������ UI��� �浹�� �Ͼ�� �ʵ��� Order�� �������ش�
    {
        Canvas canvas       = Util.GetOrAddComponent<Canvas>(go);
        CanvasScaler scaler = Util.GetOrAddComponent<CanvasScaler>(go);
        
        canvas.overrideSorting     = true;
        canvas.renderMode          = RenderMode.ScreenSpaceCamera;
        canvas.worldCamera         = Camera.main;
        scaler.uiScaleMode         = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = ScreenSize; // �÷��̾� ���� �ػ�

        if(sort)
            canvas.sortingOrder = ++_order;
        else
            canvas.sortingOrder = 0;

        if(DirectSortOrder != -1 )
            canvas.sortingOrder = DirectSortOrder;

    }

    /// <summary>
    /// Scene �� UI�� �����Ѵ�. 
    /// </summary>
    /// <param name="name"> Scene UI�� �����Ѵ�. </param>
    /// <param name="isDefault"> Default UI�� ����Ѵ�. </param>
    public void ShowSceneUI(string name = null, UITypes Type = UITypes.Default) // Scene���� ���� �⺻�� �Ǵ� SceneUI�� �����Ѵ�
    {   
        GameObject go = ResourceManager.Instance.Instantiate($"Prefabs/UI/Scene/UI_{name}"); // SceneUI���� ��� Resources/UI/Scene�� �����Ѵ�
        if (go == null)
        {
            Debug.LogWarning($"error_UI_Manager : {name} Scene�� Scene UI ������ �����߽��ϴ�.");
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

    public void ShowPopupUI(string name = null) // PopupUI�� �����Ѵ�
    {
        GameObject go = ResourceManager.Instance.Instantiate($"Prefabs/UI/Popup/UI_{name}"); // SceneUI���� ��� Resources/UI/Popup�� �����Ѵ�
        if (go == null)
        {
            Debug.LogWarning($"error_UI_Manager : UI_{name} Popup ������ �����߽��ϴ�.");
            return;
        }
        SetCanavas(go);
        go.transform.SetParent(Root.transform);
        
        _popupStack.Push(go.GetComponent<UI_Base>());
    }


    public void ClosePopupUI(UI_Base popup) // �˾��� �ݴ´�
    {
        if(_popupStack.Count == 0) // ���ÿ� �˾��� ���� ��� �����Ѵ�
            return;

        if(_popupStack.Peek() != popup) // ���� ���� �ִ� �˾��� �������� �˾��� �ƴ� ��� �����Ѵ�
        {
            Debug.LogWarning($"error_UI_Manager : {popup.name} Popup�� �ֻ�ܿ� ��ġ���� �ʽ��ϴ�.");
            return;
        }

        ClosePopupUI();
    }

    public void ClosePopupUI() // ���� ���� �ִ� �˾��� �ݴ´�
    {
        if(_popupStack.Count == 0)
            return;

        UI_Base popup = _popupStack.Pop();
        Destroy(popup.gameObject);
        popup = null;

        _order--;
    }

    public void CloseALLPopupUI() // ��� �˾��� �ݴ´�
    {
        while(_popupStack.Count>0)
            ClosePopupUI();
    }

    public void Clear()
    {
        CloseALLPopupUI();
    }
}
