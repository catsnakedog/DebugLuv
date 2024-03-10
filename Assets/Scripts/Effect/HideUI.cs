using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideUI : EffectBase
{
    private Coroutine effectC;
    private Vector2 _targetPosition = new Vector2(0, -740);

    public override void DoEffect()
    {
        effectC = StartCoroutine(ShowUIEffect());
    }

    private IEnumerator ShowUIEffect()
    {
        GameObject target = UI_Manager.Instance.GetUI<UI_InGame>().Get("TextBox");

        Vector2 startPosition = target.transform.localPosition;
        float time = 0f;

        while (time < Util.StringToFloat(Value.Value1))
        {
            target.transform.localPosition = Vector3.Lerp(startPosition, _targetPosition, time / Util.StringToFloat(Value.Value1));

            time += Time.deltaTime;
            yield return null;
        }

        EndEffect();
    }

    public override void ShowResult()
    {
        transform.localPosition = _targetPosition;
    }

    public override void StopAll()
    {
        if (effectC != null)
            StopCoroutine(effectC);
    }
}
