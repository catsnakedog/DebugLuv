using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wait : EffectBase
{
    private Coroutine effectC;

    public override void DoEffect()
    {
        effectC = StartCoroutine(WaitSeconds());
    }

    private IEnumerator WaitSeconds()
    {
        yield return new WaitForSeconds(Util.StringToFloat(Value.Value1));
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
