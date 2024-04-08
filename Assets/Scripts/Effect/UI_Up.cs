using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Up : EffectBase 
{
    private Coroutine effectC       = null;
    private GameObject TextBox      = null;

    public override void DoEffect()
    {
        effectC = StartCoroutine(ShowUIEffect());
    }

    private IEnumerator ShowUIEffect()
    {
        if (TextBox == null) TextBox = UI_Manager.GetUI<UI_InGame>().Get("TextBox");
        

        Vector2 startPosition = TextBox.transform.localPosition;
        float time = 0f;

        while (time < Util.StringToFloat(Value.Value1))
        {
            TextBox.transform.localPosition = Vector3.Lerp(startPosition, UI_Manager.UpUIPos, time / Util.StringToFloat(Value.Value1));
            time += Time.deltaTime;
            yield return null;
        }

        Debug.Log("이동 완료");
        EndEffect();
    }

    public override void ShowResult()
    {
        transform.localPosition = UI_Manager.UpUIPos;
    }

    public override void StopAll()
    {
        if (effectC != null)
            StopCoroutine(effectC);
    }
}
