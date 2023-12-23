using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager // UI�� �����ϴ� Manager�̴�
{
    int _order = 0;

    Stack<UI_Popup> _popupStack = new Stack<UI_Popup>(); // �˾����� ��� Stack���� �������ش�

    public GameObject Root{
        get{
            GameObject root = GameObject.Find("@UI_Root"); // UI���� ��� @UI_Root�� �ڽ����� ����
        if(root == null)
            root = new GameObject{name = "@UI_Root"};
        return root;
        }
    }

    public void SetCanavas(GameObject go, bool sort = true) // �⺻���� Setting + ������ UI��� �浹�� �Ͼ�� �ʵ��� Order�� �������ش�
    {
        Canvas canvas = Util.GetOrAddComponent<Canvas>(go);
        canvas.renderMode= RenderMode.ScreenSpaceOverlay;
        canvas.overrideSorting = true;
        CanvasScaler scaler = Util.GetOrAddComponent<CanvasScaler>(go);
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(2400, 1080); // �÷��̾� ���� �ػ�
        Util.GetOrAddComponent<CanvasSizeFix>(go); // UI �ػ� ����

        if(sort)
            canvas.sortingOrder = ++_order;
        else
            canvas.sortingOrder = 0;
    }

    public T ShowSceneUI<T>(string name = null) where T : UI_Scene // Scene���� ���� �⺻�� �Ǵ� SceneUI�� �����Ѵ�
    {   
        if(string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"UI/Scene/{name}"); // SceneUI���� ��� Resources/UI/Scene�� �����Ѵ�
        T sceneUI = Util.GetOrAddComponent<T>(go);

        go.transform.SetParent(Root.transform);

        Managers.Scene.CurrentScene.SceneUI = sceneUI;
        return sceneUI;
    }

    public T ShowPopupUI<T>(string name = null) where T : UI_Popup // PopupUI�� �����Ѵ�
    {   
        if(string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"UI/Popup/{name}"); // PopupUI���� ��� Resources/UI/Popup�� �����Ѵ�
        T popup = Util.GetOrAddComponent<T>(go);
        _popupStack.Push(popup);

        go.transform.SetParent(Root.transform);


        return popup;
    }


    public void ClosePopupUI(UI_Popup popup) // �˾��� �ݴ´�
    {
        if(_popupStack.Count == 0) // ���ÿ� �˾��� ���� ��� �����Ѵ�
            return;

        if(_popupStack.Peek() != popup) // ���� ���� �ִ� �˾��� �������� �˾��� �ƴ� ��� �����Ѵ�
        {
            Debug.LogWarning("Close Popup Failed");
            return;
        }

        ClosePopupUI();
    }

    public void ClosePopupUI() // ���� ���� �ִ� �˾��� �ݴ´�
    {
        if(_popupStack.Count==0)
            return;

        UI_Popup popup = _popupStack.Pop();
        Managers.Resource.Destroy(popup.gameObject);
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
