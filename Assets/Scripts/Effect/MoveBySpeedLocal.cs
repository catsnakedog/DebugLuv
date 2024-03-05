using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBySpeedLocal : EffectBase
{
    private Coroutine effectC;

    public override void DoEffect()
    {
        effectC = StartCoroutine(MoveBySpeedEffect());
    }

    private IEnumerator MoveBySpeedEffect()
    {
        while(Vector2.Distance(transform.localPosition, Util.StringToVector2(Value.Value2)) > 0.1f)
        {
            transform.localPosition = Vector2.MoveTowards(transform.localPosition, Util.StringToVector2(Value.Value2), Time.deltaTime * Util.StringToFloat(Value.Value1));
            yield return null;
        }
        EndEffect();
    }

    public override void ShowResult()
    {
        transform.localPosition = Util.StringToVector2(Value.Value2);
    }

    public override void StopAll()
    {
        if (effectC != null)
            StopCoroutine(effectC);
    }
}
