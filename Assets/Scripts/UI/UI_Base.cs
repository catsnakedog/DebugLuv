using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Base : MonoBehaviour
{
    private Dictionary<string, Dictionary<Type, UnityEngine.Object>> _objects;

    public GameObject Get(Enum name) // GameObject 반환
    {
        if (_objects == null)
            _objects = new();
        if(!_objects.ContainsKey(name.ToString())) // 한번도 호출이 안된 경우 _objects에 등록한다
            _objects[name.ToString()] = new();
        if (!_objects[name.ToString()].ContainsKey(typeof(GameObject))) // GameObject가 아직 바인딩이 안된 경우 바인딩 해준다
        {
            GameObject target = null;
            foreach (Transform child in transform.GetComponentsInChildren<Transform>(true))
                if (child.gameObject.name == name.ToString())
                {
                    target = child.gameObject;
                    break;
                }
            if (target == null)
            {
                Debug.LogWarning($"error_UI : {name.ToString()}라는 UI 오브젝트가 없습니다.");
                return null;
            }
            else
                _objects[name.ToString()][typeof(GameObject)] = target;
        }
        return (GameObject)_objects[name.ToString()][typeof(GameObject)];
    }

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

    public TargetType Get<TargetType>(Enum name) where TargetType : UnityEngine.Object // GameObject 반환
    {
        if (_objects == null)
            _objects = new();
        if (!_objects.ContainsKey(name.ToString())) // 한번도 호출이 안된 경우 _objects에 등록
            _objects[name.ToString()] = new();
        if (!_objects[name.ToString()].ContainsKey(typeof(TargetType))) // TargetType이 아직 바인딩이 안된 경우 바인딩 해준다
        {
            TargetType target = null;
            foreach (TargetType child in transform.GetComponentsInChildren<TargetType>(true))
                if (child.name == name.ToString())
                {
                    target = child;
                    break;
                }
            if (target == null)
            {
                Debug.LogWarning($"error_UI : {name.ToString()}라는 UI 오브젝트에 {typeof(TargetType).ToString()} 컴포넌트가 없습니다.");
                return null;
            }
            else
                _objects[name.ToString()][typeof(TargetType)] = target;
        }

        return (TargetType)_objects[name.ToString()][typeof(TargetType)];
    }

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
