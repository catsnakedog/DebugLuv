//-------------------------------------------------------------------------------------------------
// @file	EffectManager.cs
//
// @brief	����Ʈ�� ���� �Ŵ���
//
// @date	2024-03-14
//
// Copyright 2024 Team One-eyed Games. All Rights Reserved.
//-------------------------------------------------------------------------------------------------


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary> ����Ʈ�� ���� �Ŵ��� </summary>
public class EffectManager : ManagerSingle<EffectManager>
{
    List<Effect> effectList = new List<Effect>();
    /// <summary>
    /// ���� Play �� ����� Effect�� Play(Start)�Ѵ�.
    /// </summary>
    /// <param name="type"></param>
    /// <param name="target"></param>
    /// <param name="value"></param>
    /// <param name="work"></param>
    public void PlayEffect(Type type, GameObject target,Value value, Action work = null, ChInfo chInfo = null)
    {
        Effect effect = target.GetOrAddComponent<Effect>().PlayEffect(type, EndEffect, value, work, chInfo);
        if(effect != null)
        {
            effectList.Add(effect);
        }

    }

    /// <summary>
    /// ���� Effect�� Skip�Ѵ�.
    /// </summary>
    /// <param name="type"></param>
    /// <param name="target"></param>
    public static void SkipEffect()
    {
        //target.GetOrAddComponent<Effect>().SkipEffect(type);
        foreach(var effect in Instance.effectList)
        {
            if (effect == null) continue;
            effect.SkipEffect();
        }
    }

    /// <summary>
    /// Effect�� ������ ����.
    /// </summary>
    /// <param name="work"></param>
    public void EndEffect(Action work = null)
    {
        work?.Invoke();
    }
}
