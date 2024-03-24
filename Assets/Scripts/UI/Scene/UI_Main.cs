using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Main : UI_Base
{
    enum UI
    {
        NewBtn,
        LoadBtn,
        CGBtn,
        OptionBtn,
        QuitBtn
    }
    private void Start()
    {
        BindEvent(Get(UI.QuitBtn), ClickQuitBtn, Define.UIEvent.Click);
        BindEvent(Get(UI.OptionBtn), ClickOptionBtn, Define.UIEvent.Click);
        BindEvent(Get(UI.NewBtn), ClickNewBtn, Define.UIEvent.Click);
    }

    private void ClickNewBtn(PointerEventData data)
    {
        SceneManagerEX.Instance.LoadScene("InGame");
    }

    private void ClickLoadBtn(PointerEventData data)
    {

    }

    private void ClickCGBtn(PointerEventData data)
    {

    }

    private void ClickOptionBtn(PointerEventData data)
    {
        UI_Manager.Instance.ShowPopupUI("Option");
    }

    private void ClickQuitBtn(PointerEventData data)
    {
        UI_Manager.Instance.ShowPopupUI("Quit");
    }
}
