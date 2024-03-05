using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : ManagerSingle<EffectManager>
{
    public void PlayEffect(Type type, GameObject target,Value value, Action work = null)
    {
        target.GetOrAddComponent<Effect>().PlayEffect(type, EndEffect, value, work);
    }

    public void SkipEffect(Type type, GameObject target)
    {
        target.GetOrAddComponent<Effect>().SkipEffect(type);
    }

    public void EndEffect(Action work = null)
    {
        work?.Invoke();
    }
}
