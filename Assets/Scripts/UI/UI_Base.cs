using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class UI_Base : MonoBehaviour // UI�� ���� �⺻�� �Ǵ� calss�̴�
{
    protected Dictionary<Type, UnityEngine.Object[]> _objects = new Dictionary<Type, UnityEngine.Object[]>(); // �ʿ��� ������Ʈ�� ��ųʸ��� �����Ѵ�
    static GameObject ClickTarget; // Ŭ�� ����Ʈ�� ���õ� ����

    public abstract void Init();
    

    protected void Bind<T>(Type type) where T : UnityEngine.Object // Enum���� ���ǵ� Object�� ã�Ƽ� _objects�ȿ� �ִ´�
    {
        string[] names = Enum.GetNames(type);

        UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];
        _objects.Add(typeof(T), objects);

        for(int i = 0; i < names.Length; i++)
        {
            if(typeof(T)==typeof(GameObject))
                objects[i] = Util.FindChild(gameObject, names[i], true);
            else
                objects[i] = Util.FindChild<T>(gameObject, names[i], true);
        }
    }
    protected void HardBind<T>(Type type) where T : UnityEngine.Object // Bind���� �߰��� ��Ȱ��ȭ �� Object���� ã���ش�
    {
        string[] names = Enum.GetNames(type);

        UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];
        _objects.Add(typeof(T), objects);

        for (int i = 0; i < names.Length; i++)
        {
            if (typeof(T) == typeof(GameObject))
            {
                foreach (Transform target in GetComponentsInChildren<Transform>(true))
                {
                    if(target.name == names[i])
                        objects[i] = target.gameObject;
                }
            }
            else
            {
                foreach (T target in GetComponentsInChildren<T>(true))
                {
                    if (target.name == names[i])
                        objects[i] = target;
                }
            }
        }
    }
    protected T Get<T>(int idx) where T : UnityEngine.Object // ���������� �ڵ�󿡼� �츮�� ���ϴ� Object�� ã���� �� �Լ��� ���� _objects�� �����ؼ� ã�´�
    {
        UnityEngine.Object[] objects = null;
        if(_objects.TryGetValue(typeof(T), out objects) == false)
            return null;

        return objects[idx] as T;
    }

    public static void BindEvent(GameObject go, Action<UnityEngine.EventSystems.PointerEventData> action, Define.UIEvent type = Define.UIEvent.Click) // Ŭ���� ���õ� �̺�Ʈ�� Bind ���ش�
    {
        UI_EventHandler evt = Util.GetOrAddComponent<UI_EventHandler>(go);

        switch(type)
        {
            case Define.UIEvent.Click:
                evt.OnClickHandler -= action;
                evt.OnClickHandler += action;
                break;

            case Define.UIEvent.Drag:
                evt.OnDragHandler -= action;
                evt.OnDragHandler += action;
                break;
            case Define.UIEvent.Down:
                evt.OnDownHandler -= action;
                evt.OnDownHandler += action;
                break;
            case Define.UIEvent.Up:
                evt.OnUpHandler -= action;
                evt.OnUpHandler += action;
                break;
        }
    }

    public static void Down(PointerEventData data) // Ŭ�� ����Ʈ�� ���õ� �Լ�
    {
        ClickTarget = data.pointerCurrentRaycast.gameObject;
        if(ClickTarget.transform.childCount == 0)
            Debug.LogWarning($"error_UI_Base : {ClickTarget.name}�� Ŭ�� ����Ʈ�� �����ϴ�.");
        else
            ClickTarget.transform.GetChild(0).gameObject.SetActive(true);
    }

    public static void Up(PointerEventData data) // Ŭ�� ����Ʈ�� ���õ� �Լ�
    {
        if (ClickTarget.transform.childCount == 0)
            Debug.LogWarning($"error_UI_Base : {ClickTarget.name}�� Ŭ�� ����Ʈ�� �����ϴ�.");
        else
            ClickTarget.transform.GetChild(0).gameObject.SetActive(false);
    }
}
