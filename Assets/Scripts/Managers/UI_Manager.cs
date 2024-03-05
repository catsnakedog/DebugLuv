using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : ManagerSingle<UI_Manager>, IClearable // UI�� �����ϴ� Manager�̴�
{
    private int _order = 0;
    private Vector2 _screenSize = new Vector2(1920, 1080);
    private Stack<UI_Base> _popupStack = new(); // �˾����� ��� Stack���� �������ش�

    private UI_Base _sceneUI;
    
    public UIType GetUI<UIType>() where UIType : UI_Base
    {
        if (_sceneUI is UIType) // SceneUI ���� �˻�
            return _sceneUI as UIType;

        foreach(UI_Base popup in _popupStack) // PopupUI ���� �˻�
        {
            if (popup is UIType)
                return popup as UIType;
        }

        Debug.LogWarning($"error_UI_Manager : {typeof(UIType).ToString()} UI�� �������� �ʽ��ϴ�.");
        return null;
    }

    public GameObject Root{
        get
        {
        GameObject root = GameObject.Find("@UI_Root"); // UI���� ��� @UI_Root�� �ڽ����� ����
        if(root == null)
            {
                root = new GameObject { name = "@UI_Root" };
            }
        return root;
        }
    }

    public void SetCanavas(GameObject go, bool sort = true) // �⺻���� Setting + ������ UI��� �浹�� �Ͼ�� �ʵ��� Order�� �������ش�
    {
        Canvas canvas = Util.GetOrAddComponent<Canvas>(go);
        canvas.renderMode= RenderMode.ScreenSpaceCamera;
        canvas.worldCamera = Camera.main;
        canvas.overrideSorting = true;
        CanvasScaler scaler = Util.GetOrAddComponent<CanvasScaler>(go);
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = _screenSize; // �÷��̾� ���� �ػ�

        if(sort)
            canvas.sortingOrder = ++_order;
        else
            canvas.sortingOrder = 0;
    }

    public void ShowSceneUI(string name = null) // Scene���� ���� �⺻�� �Ǵ� SceneUI�� �����Ѵ�
    {   
        GameObject go = ResourceManager.Instance.Instantiate($"Prefabs/UI/Scene/UI_{name}"); // SceneUI���� ��� Resources/UI/Scene�� �����Ѵ�
        if (go == null)
        {
            Debug.LogWarning($"error_UI_Manager : {name} Scene�� Scene UI ������ �����߽��ϴ�.");
            return;
        }
        SetCanavas(go, false);
        go.transform.SetParent(Root.transform);

        _sceneUI = go.GetComponent<UI_Base>();
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
        if(_popupStack.Count==0)
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
