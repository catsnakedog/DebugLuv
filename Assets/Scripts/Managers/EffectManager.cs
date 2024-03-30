//-------------------------------------------------------------------------------------------------
// @file	EffectManager.cs
//
// @brief	이펙트를 위한 매니저
//
// @date	2024-03-14
//
// Copyright 2024 Team One-eyed Games. All Rights Reserved.
//-------------------------------------------------------------------------------------------------


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary> 이펙트를 위한 매니저 </summary>
public class EffectManager : ManagerSingle<EffectManager>
{
    /// <summary>
    /// 게임 Play 중 사용할 Effect를 Play(Start)한다.
    /// </summary>
    /// <param name="type"></param>
    /// <param name="target"></param>
    /// <param name="value"></param>
    /// <param name="work"></param>
    public void PlayEffect(Type type, GameObject target,Value value, Action work = null, ChInfo chInfo = null)
    {
        target.GetOrAddComponent<Effect>().PlayEffect(type, EndEffect, value, work, chInfo);
    }

    /// <summary>
    /// 현재 Effect를 Skip한다.
    /// </summary>
    /// <param name="type"></param>
    /// <param name="target"></param>
    public void SkipEffect(Type type, GameObject target)
    {
        target.GetOrAddComponent<Effect>().SkipEffect(type);
    }

    /// <summary>
    /// Effect의 끝으로 간다.
    /// </summary>
    /// <param name="work"></param>
    public void EndEffect(Action work = null)
    {
        work?.Invoke();
    }
}
