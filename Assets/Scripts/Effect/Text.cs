using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class Text : EffectBase
{
    private Coroutine      effectC   = null;
    private UI_InGame      inGame    = null;
    private WaitForSeconds textDelay = new WaitForSeconds(0.01f);
    public override void DoEffect()
    {
        effectC = StartCoroutine(TextEffect());
    }
    private IEnumerator TextEffect()
    {
        yield return null;
        EndEffect();
    }
    public IEnumerator ShowText()
    {
        if(inGame == null)
        {
            inGame = UI_Manager.GetUI<UI_InGame>();
        }

        inGame.SetName(Value.Value1);

        StringBuilder sb = new(inGame.UIText.text);

        foreach (char letter in Value.Value2)
        {
            sb.Append(letter);
            inGame.UIText.text = sb.ToString();

            yield return textDelay;
        }

    }

    public override void ShowResult()
    {
        inGame.UIText.text = Value.Value2;
    }

    public override void StopAll()
    {
        if (effectC != null)
            StopCoroutine(effectC);
    }
}
