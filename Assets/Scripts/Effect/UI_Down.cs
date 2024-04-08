using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Down : EffectBase
{
    private Coroutine  effectC = null;
    private GameObject TextBox = null;

    public override void DoEffect()
    {
        effectC = StartCoroutine(ShowUIEffect());
    }

    private IEnumerator ShowUIEffect()
    {
        if (TextBox == null) TextBox = UI_Manager.GetUI<UI_InGame>().Get("TextBox");

        GameObject target = UI_Manager.GetUI<UI_InGame>().Get("TextBox");

        Vector2 startPosition = target.transform.localPosition;
        float time = 0f;

        while (time < Util.StringToFloat(Value.Value1))
        {
            target.transform.localPosition = Vector3.Lerp(startPosition, UI_Manager.DownUIPos, time / Util.StringToFloat(Value.Value1));

            time += Time.deltaTime;
            yield return null;
        }

        EndEffect();
    }

    public override void ShowResult()
    {
        transform.localPosition = UI_Manager.DownUIPos;
    }

    public override void StopAll()
    {
        if (effectC != null)
            StopCoroutine(effectC);
    }
}
