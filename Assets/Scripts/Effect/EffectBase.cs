using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EffectBase : MonoBehaviour
{
    private Action<Action> _endAction;
    private Action _work;

    public Value Value;

    public void StartEffect(Action<Action> endAction, Value value, Action work) // Effect ����
    {
        _endAction = endAction;
        _work = work;
        Value = value;
        DoEffect();
    }

    public abstract void DoEffect(); // Effect ����

    public void EndEffect() // Effect�� ���� �� �����������Ѵ�
    {
        _endAction.Invoke(_work);
    }

    public abstract void ShowResult(); // Effect�� ���� �� ����� �����־��Ѵ�

    public abstract void StopAll(); // �ڷ�ƾ ���� ���� ���� �͵��� ���� ��������Ѵ�

    public void SkipEffect() // Effect ������ ��ŵ - ����� �ٷ� ������ ��ŵ�ϰ� ����� ������ EndEffect ����
    {
        StopAll();
        ShowResult();
        EndEffect();
    }
}
