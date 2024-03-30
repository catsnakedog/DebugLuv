using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjLock : EffectBase
{
    private Coroutine effectC;

    public override void DoEffect()
    {
        effectC = StartCoroutine(ObjLockEffect());
    }

    private IEnumerator ObjLockEffect()
    {
        int idx = Util.GetStorageIdx(transform.name);
        if(idx != -1)
        {
            EpisodeManager.ObjLockStorageIdx(idx);
        }
        else
        {
            Util.DebugLogWarning("ObjLock ½ÇÆÐ");
        }

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
