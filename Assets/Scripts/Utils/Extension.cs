using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extension
{
    public static T GetOrAddComponent<T>(this GameObject go) where T : UnityEngine.Component
    {
        return Util.GetOrAddComponent<T>(go);
    }
    public static void AddUIEvent(this GameObject go, Action<UnityEngine.EventSystems.PointerEventData> action, Define.UIEvent type = Define.UIEvent.Click)
    {
        UI_Base.BindEvent(go, action, type);
    } // GameObject�� Ŭ�� �̺�Ʈ�� Bind ���ش�
    public static void AddUIEffectEvent(this GameObject go, Action<UnityEngine.EventSystems.PointerEventData> action, Define.UIEvent type = Define.UIEvent.Click)
    {
        UI_Base.BindEvent(go, UI_Base.Down, Define.UIEvent.Down); // Down Up�� �������� ������ ������ ������ AddUIEffectEvent���� �����ϱ� ���ؼ���
        UI_Base.BindEvent(go, UI_Base.Up, Define.UIEvent.Up);
        UI_Base.BindEvent(go, action, type);
    } // ��ư Ŭ���� �ش� GameObject�� ù �ڽ��� Ȱ��ȭ ��Ȱ��ȭ ��Ŵ���ν� ����Ʈ�� �߰��Ѵ�.
}