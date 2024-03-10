using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class UI_InGame : UI_Base
{
    enum UI
    {
        Name,
        Text,
        NextParagraph
    }

    public void ShowTextEffect(string text, Action work)
    {
        StartCoroutine(ShowText(text, work));
    }

    public IEnumerator ShowText(string text, Action work)
    {
        Get<TMP_Text>(UI.Text).text = "";
        StringBuilder sb = new();

        foreach(char letter in text)
        {
            sb.Append(letter);
            Get<TMP_Text>(UI.Text).text = sb.ToString();
            yield return new WaitForSeconds(0.1f);
        }

        work?.Invoke();
    }

    public void SetName(string text)
    {
        Get<TMP_Text>(UI.Name).text = text;
    }
}
