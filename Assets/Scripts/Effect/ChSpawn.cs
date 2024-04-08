using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChSpawn : EffectBase
{
    private Coroutine effectC = null;

    public override void DoEffect()
    {
        effectC = StartCoroutine(ChSpawnEffect());
    }

    private IEnumerator ChSpawnEffect()
    {
        transform.localPosition = Util.StringToVector3(Value.Value2);
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

