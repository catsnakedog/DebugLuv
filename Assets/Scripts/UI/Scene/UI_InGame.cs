using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_InGame : UI_Base
{
    enum UI
    {
        Name,
        Text,
        NextParagraph
    }

    private void Start()
    {
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
            yield return new WaitForSeconds(0.05f);
        }

        work?.Invoke();
    }

    public void SetName(string text)
    {
        Get<TMP_Text>(UI.Name).text = text;
    }

    public void NextSetence(PointerEventData data)
    {
        if (EpisodeManager.Instance.IsSetenceDone)
        {
            EpisodeManager.Instance.IsNext = true;
        }
        else
        {
            //Skip
        }
    }
}
