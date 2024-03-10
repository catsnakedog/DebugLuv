using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    private Dictionary<Type, EffectBase> _effect;
    
    public void PlayEffect(Type type, Action<Action> endAction, Value value, Action work)
    {
        if (type == null)
        {
            Debug.LogWarning($"error_Effect : Effect Type이 이상합니다.");
            work?.Invoke();
            return;
        }
        if (_effect == null)
            _effect = new();
        if (!_effect.ContainsKey(type))
        {
            _effect[type] = gameObject.AddComponent(type) as EffectBase;
        }

        _effect[type].StartEffect(endAction, value, work);
    }

    public void SkipEffect(Type type)
    {
        if (_effect[type] == null)
            return;
        _effect[type].SkipEffect();
    }
}
