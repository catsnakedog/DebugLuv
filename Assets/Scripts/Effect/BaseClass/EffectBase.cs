//-------------------------------------------------------------------------------------------------
// @file	EffectBase.cs
//
// @brief	�޴��� ???
//
// @date	2024-03-14
//
// Copyright 2024 Team One-eyed Games. All Rights Reserved.
//-------------------------------------------------------------------------------------------------


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> ����Ʈ ���̽� Ŭ���� </summary>
public abstract class EffectBase : MonoBehaviour
{
    /// <summary> ���� �� ���� Action </summary>
    private Action<Action> _endAction;
    /// <summary> �۾� Action </summary>
    private Action _work;

    private Value _value;
    private ChInfo _chInfo;
    protected Value Value { get { return _value; } set { _value = value; } }
    protected ChInfo ChInfo { get { return _chInfo; } set { _chInfo = value; } }


    /// <summary>
    /// Effect ����
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
    /// Effect ����
    /// </summary>
    public abstract void DoEffect();
    
    /// <summary>
    /// Effect�� ���� �� ����� �����־��Ѵ�
    /// </summary>
    public abstract void ShowResult();

    /// <summary>
    /// �ڷ�ƾ ���� ���� ���� �͵��� ���� ��������Ѵ�
    /// </summary>
    public abstract void StopAll();

    /// <summary>
    /// Effect�� ���� �� �����������Ѵ�
    /// </summary>
    public void EndEffect() 
    {
        _endAction.Invoke(_work);
    }

    /// <summary>
    /// Effect ������ ��ŵ - ����� �ٷ� ������ ��ŵ�ϰ� ����� ������ EndEffect ����
    /// </summary>
    public void SkipEffect()
    {
        StopAll();
        ShowResult();
        EndEffect();
    }
}
