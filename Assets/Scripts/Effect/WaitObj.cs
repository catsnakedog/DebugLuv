using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitObj : EffectBase
{
    private Coroutine effectC = null;

    public override void DoEffect()
    {
        effectC = StartCoroutine(A_Effect());
    }

    private IEnumerator A_Effect()
    {
        float time = Util.StringToFloat(Value.Value1);
        yield return new WaitForSeconds(time);
        EndEffect();
    }

    public override void ShowResult()
    {

    }

    public override void StopAll()
    {
        if (effectC != null)
            StopCoroutine(effectC);
    }
}
