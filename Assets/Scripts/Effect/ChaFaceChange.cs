using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaFaceChange : EffectBase
{
    private Coroutine effectC;

    public override void DoEffect()
    {
        effectC = StartCoroutine(ChaFaceChangeEffect());
    }

    private IEnumerator ChaFaceChangeEffect()
    {
        Transform chTransform = transform.GetChild(0);
        SpriteRenderer chSpriteRenderer= null;

        if (chTransform != null)
        {
            chSpriteRenderer = chTransform.GetComponent<SpriteRenderer>();
        }

        if (chSpriteRenderer != null)
        {
            ChInfo.Opacity = transform.GetChild(0).GetComponent<SpriteRenderer>().color.a;
        }
        
        ChMakingManager.ChMaking(ChInfo, gameObject);

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
