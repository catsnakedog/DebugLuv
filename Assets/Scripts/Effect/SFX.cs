using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : EffectBase
{
    private Coroutine effectC;

    public override void DoEffect()
    {
        effectC = StartCoroutine(PlaySFX());
    }

    private IEnumerator PlaySFX()
    {
        SoundManager.Instance.Play(Value.Value1);
        yield return null;
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
