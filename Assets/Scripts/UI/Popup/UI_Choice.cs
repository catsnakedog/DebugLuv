//-------------------------------------------------------------------------------------------------
// @file	UI_Choice.cs
//
// @brief	을 위한 매니저
//
// @date	2024-03-14
//
// Copyright 2024 Team One-eyed Games. All Rights Reserved.
//-------------------------------------------------------------------------------------------------



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Choice : UI_Base
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
