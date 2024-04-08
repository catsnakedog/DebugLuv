using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class UI_Main : UI_Base
{
    enum UI
    {
        NewBtn,
        LoadBtn,
        CGBtn,
        OptionBtn,
        QuitBtn,
        DataLoadBtn,
        Debug,
    }
    private void Start()
    {
        BindEvent(Get(UI.QuitBtn),     ClickQuitBtn,     Define.UIEvent.Click);
        BindEvent(Get(UI.OptionBtn),   ClickOptionBtn,   Define.UIEvent.Click);
        BindEvent(Get(UI.NewBtn),      ClickNewBtn,      Define.UIEvent.Click);
        BindEvent(Get(UI.DataLoadBtn), ClickDataLoadBtn, Define.UIEvent.Click);

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

    private void ClickDataLoadBtn(PointerEventData data)
    {
        StartCoroutine(NetworkManager.GoogleSheetsDataParsing());
        string Path = (NetworkManager.Instance.folderPath == "" || NetworkManager.Instance.folderPath == null) ? "경로가 없음" : NetworkManager.Instance.folderPath;
        string LindData = (NetworkManager.Instance.LineData == "" || NetworkManager.Instance.LineData == null) ? "경로가 없음" : NetworkManager.Instance.folderPath;

        Get<TMP_Text>(UI.Debug).text = $"{NetworkManager.Instance.folderPath} \n {NetworkManager.Instance.LineData}";

    }

}
