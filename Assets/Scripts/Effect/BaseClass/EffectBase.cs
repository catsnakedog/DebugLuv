//-------------------------------------------------------------------------------------------------
// @file	EffectBase.cs
//
// @brief	메니저 ???
//
// @date	2024-03-14
//
// Copyright 2024 Team One-eyed Games. All Rights Reserved.
//-------------------------------------------------------------------------------------------------


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 이펙트 베이스 클래스 </summary>
public abstract class EffectBase : MonoBehaviour
{
    /// <summary> 종료 후 실행 Action </summary>
    private Action<Action> _endAction;
    /// <summary> 작업 Action </summary>
    private Action _work;

    private Value _value;
    private ChInfo _chInfo;
    protected Value Value { get { return _value; } set { _value = value; } }
    protected ChInfo ChInfo { get { return _chInfo; } set { _chInfo = value; } }


    /// <summary>
    /// Effect 시작
    /// </summary>
    /// <param name="endAction"></param>
    /// <param name="value"></param>
    /// <param name="work"></param>
    public void StartEffect(Action<Action> endAction, Value value, Action work, ChInfo chInfo = null)
    {
        _endAction = endAction;
        _work = work;
        Value = value;
        ChInfo = chInfo;
        DoEffect();
    }

    /// <summary>
    /// Effect 실행
    /// </summary>
    public abstract void DoEffect();
    
    /// <summary>
    /// Effect가 끝난 뒤 결과를 만들어둬야한다
    /// </summary>
    public abstract void ShowResult();

    /// <summary>
    /// 코루틴 같은 실행 중인 것들은 전부 멈춰줘야한다
    /// </summary>
    public abstract void StopAll();

    /// <summary>
    /// Effect가 끝난 뒤 실행시켜줘야한다
    /// </summary>
    public void EndEffect() 
    {
        _endAction.Invoke(_work);
    }

    /// <summary>
    /// Effect 연출을 스킵 - 실행시 바로 과정은 스킵하고 결과가 나오고 EndEffect 실행
    /// </summary>
    public void SkipEffect()
    {
        StopAll();
        ShowResult();
        EndEffect();
    }
}
