using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Quit : UI_Base
{
    enum UI
    {
        YesBtn,
        NoBtn,
        BgPanel
    }

    private void Start()
    {
        BindEvent(Get(UI.YesBtn), Yes, Define.UIEvent.Click);
        BindEvent(Get(UI.NoBtn), No, Define.UIEvent.Click);
        BindEvent(Get(UI.BgPanel), No, Define.UIEvent.Click);
    }

    private void Yes(PointerEventData data)
    {
        Application.Quit();
    }

    private void No(PointerEventData data)
    {
        UI_Manager.Instance.ClosePopupUI(this);
    }
}
