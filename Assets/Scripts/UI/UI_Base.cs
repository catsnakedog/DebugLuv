//-------------------------------------------------------------------------------------------------
// @file	UI_Base.cs
//
// @brief	���� UI�� ���� UI base class
//
// @date	2024-03-14
//
// Copyright 2024 Team One-eyed Games. All Rights Reserved.
//-------------------------------------------------------------------------------------------------


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UI_Base : MonoBehaviour
{
    /// <summary> UI���� ����� ��ü(Button Text ��)�� ��� �ڷᱸ��</summary>
    private Dictionary<string, Dictionary<Type, UnityEngine.Object>> _objects;

    /// <summary>
    /// �ش� UI�� GameObject Type�� Get
    /// </summary>
    /// <param name="name"> ��ü �̸� Enum Type </param>
    /// <returns> �ش� ��ü </returns>
    public GameObject Get(Enum name) // GameObject ��ȯ
    {
        return Get(name.ToString());
    }

    /// <summary>
    /// String�� �̿��� Get
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public GameObject Get(String name) // GameObject ��ȯ
    {
        if (_objects == null)
            _objects = new();
        if (!_objects.ContainsKey(name)) // �ѹ��� ȣ���� �ȵ� ��� _objects�� ����Ѵ�
            _objects[name] = new();
        if (!_objects[name].ContainsKey(typeof(GameObject))) // GameObject�� ���� ���ε��� �ȵ� ��� ���ε� ���ش�
        {
            GameObject target = null;
            foreach (Transform child in transform.GetComponentsInChildren<Transform>(true))
                if (child.gameObject.name == name)
                {
                    target = child.gameObject;
                    break;
                }
            if (target == null)
            {
                Debug.LogWarning($"error_UI : {name}��� UI ������Ʈ�� �����ϴ�.");
                return null;
            }
            else
                _objects[name][typeof(GameObject)] = target;
        }
        return (GameObject)_objects[name][typeof(GameObject)];
    }

    /// <summary>
    /// TargetType ��ü ��ȯ 
    /// </summary>
    /// <typeparam name="TargetType"></typeparam>
    /// <param name="name"></param>
    /// <returns></returns>
    public TargetType Get<TargetType>(Enum name) where TargetType : UnityEngine.Object // GameObject ��ȯ
    {
        return Get<TargetType>(name.ToString());
    }

    /// <summary>
    /// TargetType ��ü ��ȯ 
    /// </summary>
    /// <typeparam name="TargetType"></typeparam>
    /// <param name="name"></param>
    /// <returns></returns>
    public TargetType Get<TargetType>(string name) where TargetType : UnityEngine.Object // GameObject ��ȯ
    {
        if (_objects == null)
            _objects = new();
        if (!_objects.ContainsKey(name)) // �ѹ��� ȣ���� �ȵ� ��� _objects�� ���
            _objects[name] = new();
        if (!_objects[name].ContainsKey(typeof(TargetType))) // TargetType�� ���� ���ε��� �ȵ� ��� ���ε� ���ش�
        {
            TargetType target = null;
            foreach (TargetType child in transform.GetComponentsInChildren<TargetType>(true))
                if (child.name == name)
                {
                    target = child;
                    break;
                }
            if (target == null)
            {
                Debug.LogWarning($"error_UI : {name}��� UI ������Ʈ�� {typeof(TargetType).ToString()} ������Ʈ�� �����ϴ�.");
                return null;
            }
            else
                _objects[name][typeof(TargetType)] = target;
        }

        return (TargetType)_objects[name][typeof(TargetType)];
    }

    /// <summary>
    /// �̺�Ʈ ���ε� (���� Click, Drag, Mouse Down, Mouse Up ���� �ʿ�� �߰� �ٶ�)
    /// </summary>
    /// <param name="go"></param>
    /// <param name="action"></param>
    /// <param name="type"></param>
    public void BindEvent(GameObject go, Action<UnityEngine.EventSystems.PointerEventData> action, Define.UIEvent type = Define.UIEvent.Click) // Ŭ���� ���õ� �̺�Ʈ�� Bind ���ش�
    {
        UI_EventHandler evt = Util.GetOrAddComponent<UI_EventHandler>(go);

        switch (type)
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
}
