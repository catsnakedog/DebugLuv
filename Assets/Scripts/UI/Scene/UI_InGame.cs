using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_InGame : UI_Base
{
    /// <summary> 글자와 글자 사이 딜래이 </summary>
    WaitForSeconds _textDelay = null; 
    enum UI
    {
        Name,
        Text,
        NextParagraph,
        Select,
        SelectBox1,
        SelectBox2,
        SelectBox3,
        SelectBox4,
        SelectBoxText1,
        SelectBoxText2,
        SelectBoxText3,
        SelectBoxText4
    }

    private ChoiceData _choiceData;

    private void Start()
    {

#if UNITY_EDITOR
        _textDelay = new WaitForSeconds(0.01f);
#else
        //_textDelay = new WaitForSeconds(0.05f);
        _textDelay = new WaitForSeconds(0.01f);

#endif

        BindEvent(Get(UI.NextParagraph), NextSetence, Define.UIEvent.Click);
    }

    public void ShowTextEffect(string text, Action work)
    {
        StartCoroutine(ShowText(text, work));
    }

    public IEnumerator ShowText(string text, Action work)
    {
        StringBuilder sb = new(Get<TMP_Text>(UI.Text).text);

        foreach(char letter in text)
        {
            sb.Append(letter);
            Get<TMP_Text>(UI.Text).text = sb.ToString();

            yield return _textDelay;
        }

        work?.Invoke();
    }

    public void SetName(string text)
    {
        Get<TMP_Text>(UI.Name).text = text;
    }

    public void NextSetence(PointerEventData data)
    {
        if (EpisodeManager.IsSentenceDone)
        {
            EpisodeManager.DropStorageAll();
            EpisodeManager.IsNext = true;
        }
        else
        {
            //Skip

            //SkipEffect()
        }
    }

    public void ShowChoice(int num)
    {
        Get(UI.Select).SetActive(true);

        List<float> selectBoxPos = new();
        _choiceData = DataManager.Instance.DebugLuvData.Choice[num];

        switch (_choiceData.Select.Count)
        {
            case 1:
                selectBoxPos.Add(129);
                break;
            case 2:
                selectBoxPos.Add(210.5f);
                selectBoxPos.Add(47.5f);
                break;
            case 3:
                selectBoxPos.Add(292);
                selectBoxPos.Add(129);
                selectBoxPos.Add(-34);
                break;
            case 4:
                selectBoxPos.Add(373.5f);
                selectBoxPos.Add(210.5f);
                selectBoxPos.Add(47.5f);
                selectBoxPos.Add(-116.5f);
                break;
        }

        for (int i = 0; i < _choiceData.Select.Count; i++)
        {
            Get($"SelectBoxP{i + 1}").transform.localPosition = new Vector2(13.4f, selectBoxPos[i]);
            Get<TMP_Text>($"SelectBoxP{i + 1}").text = _choiceData.Select[i].Text;
            BindEvent(Get($"SelectBoxP{i + 1}"), SelectChoice, Define.UIEvent.Click);
            Get($"SelectBoxP{i + 1}").SetActive(true);
        }
    }

    private void SelectChoice(PointerEventData data)
    {
        EpisodeManager.ChangeBranch(_choiceData.Select[Util.StringToInt(data.pointerCurrentRaycast.gameObject.name[^1].ToString())].Branch);
        Get(UI.Select).SetActive(false);
    }
}
