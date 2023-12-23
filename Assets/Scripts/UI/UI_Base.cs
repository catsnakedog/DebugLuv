using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class UI_Base : MonoBehaviour // UI의 가장 기본이 되는 calss이다
{
    protected Dictionary<Type, UnityEngine.Object[]> _objects = new Dictionary<Type, UnityEngine.Object[]>(); // 필요한 오브젝트를 딕셔너리로 저장한다
    static GameObject ClickTarget; // 클릭 이펙트와 관련된 변수

    public abstract void Init();
    

    protected void Bind<T>(Type type) where T : UnityEngine.Object // Enum으로 정의된 Object를 찾아서 _objects안에 넣는다
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
    protected void HardBind<T>(Type type) where T : UnityEngine.Object // Bind에서 추가로 비활성화 된 Object까지 찾아준다
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
    protected T Get<T>(int idx) where T : UnityEngine.Object // 실질적으로 코드상에서 우리가 원하는 Object를 찾을때 이 함수를 통해 _objects에 접근해서 찾는다
    {
        UnityEngine.Object[] objects = null;
        if(_objects.TryGetValue(typeof(T), out objects) == false)
            return null;

        return objects[idx] as T;
    }

    public static void BindEvent(GameObject go, Action<UnityEngine.EventSystems.PointerEventData> action, Define.UIEvent type = Define.UIEvent.Click) // 클릭과 관련된 이벤트를 Bind 해준다
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

    public static void Down(PointerEventData data) // 클릭 이펙트와 관련된 함수
    {
        ClickTarget = data.pointerCurrentRaycast.gameObject;
        if(ClickTarget.transform.childCount == 0)
            Debug.LogWarning($"error_UI_Base : {ClickTarget.name}의 클릭 이펙트가 없습니다.");
        else
            ClickTarget.transform.GetChild(0).gameObject.SetActive(true);
    }

    public static void Up(PointerEventData data) // 클릭 이펙트와 관련된 함수
    {
        if (ClickTarget.transform.childCount == 0)
            Debug.LogWarning($"error_UI_Base : {ClickTarget.name}의 클릭 이펙트가 없습니다.");
        else
            ClickTarget.transform.GetChild(0).gameObject.SetActive(false);
    }
}
