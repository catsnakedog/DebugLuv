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
    } // GameObject의 클릭 이벤트를 Bind 해준다
    public static void AddUIEffectEvent(this GameObject go, Action<UnityEngine.EventSystems.PointerEventData> action, Define.UIEvent type = Define.UIEvent.Click)
    {
        UI_Base.BindEvent(go, UI_Base.Down, Define.UIEvent.Down); // Down Up를 정적으로 선언한 이유는 정적인 AddUIEffectEvent에서 접근하기 위해서다
        UI_Base.BindEvent(go, UI_Base.Up, Define.UIEvent.Up);
        UI_Base.BindEvent(go, action, type);
    } // 버튼 클릭시 해당 GameObject의 첫 자식을 활성화 비활성화 시킴으로써 이펙트를 추가한다.
}