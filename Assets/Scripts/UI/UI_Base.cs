//-------------------------------------------------------------------------------------------------
// @file	UI_Base.cs
//
// @brief	개별 UI를 위한 UI base class
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
    /// <summary> UI에서 사용할 객체(Button Text 등)를 담는 자료구조</summary>
    private Dictionary<string, Dictionary<Type, UnityEngine.Object>> _objects;

    /// <summary>
    /// 해당 UI의 GameObject Type을 Get
    /// </summary>
    /// <param name="name"> 객체 이름 Enum Type </param>
    /// <returns> 해당 객체 </returns>
    public GameObject Get(Enum name) // GameObject 반환
    {
        return Get(name.ToString());
    }

    /// <summary>
    /// String을 이용한 Get
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public GameObject Get(String name) // GameObject 반환
    {
        if (_objects == null)
            _objects = new();
        if (!_objects.ContainsKey(name)) // 한번도 호출이 안된 경우 _objects에 등록한다
            _objects[name] = new();
        if (!_objects[name].ContainsKey(typeof(GameObject))) // GameObject가 아직 바인딩이 안된 경우 바인딩 해준다
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
                Debug.LogWarning($"error_UI : {name}라는 UI 오브젝트가 없습니다.");
                return null;
            }
            else
                _objects[name][typeof(GameObject)] = target;
        }
        return (GameObject)_objects[name][typeof(GameObject)];
    }

    /// <summary>
    /// TargetType 객체 반환 
    /// </summary>
    /// <typeparam name="TargetType"></typeparam>
    /// <param name="name"></param>
    /// <returns></returns>
    public TargetType Get<TargetType>(Enum name) where TargetType : UnityEngine.Object // GameObject 반환
    {
        return Get<TargetType>(name.ToString());
    }

    /// <summary>
    /// TargetType 객체 반환 
    /// </summary>
    /// <typeparam name="TargetType"></typeparam>
    /// <param name="name"></param>
    /// <returns></returns>
    public TargetType Get<TargetType>(string name) where TargetType : UnityEngine.Object // GameObject 반환
    {
        if (_objects == null)
            _objects = new();
        if (!_objects.ContainsKey(name)) // 한번도 호출이 안된 경우 _objects에 등록
            _objects[name] = new();
        if (!_objects[name].ContainsKey(typeof(TargetType))) // TargetType이 아직 바인딩이 안된 경우 바인딩 해준다
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
                Debug.LogWarning($"error_UI : {name}라는 UI 오브젝트에 {typeof(TargetType).ToString()} 컴포넌트가 없습니다.");
                return null;
            }
            else
                _objects[name][typeof(TargetType)] = target;
        }

        return (TargetType)_objects[name][typeof(TargetType)];
    }

    /// <summary>
    /// 이벤트 바인딩 (현재 Click, Drag, Mouse Down, Mouse Up 지원 필요시 추가 바람)
    /// </summary>
    /// <param name="go"></param>
    /// <param name="action"></param>
    /// <param name="type"></param>
    public void BindEvent(GameObject go, Action<UnityEngine.EventSystems.PointerEventData> action, Define.UIEvent type = Define.UIEvent.Click) // 클릭과 관련된 이벤트를 Bind 해준다
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
