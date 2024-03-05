using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EffectBase : MonoBehaviour
{
    private Action<Action> _endAction;
    private Action _work;

    public Value Value;

    public void StartEffect(Action<Action> endAction, Value value, Action work) // Effect 시작
    {
        _endAction = endAction;
        _work = work;
        Value = value;
        DoEffect();
    }

    public abstract void DoEffect(); // Effect 실행

    public void EndEffect() // Effect가 끝난 뒤 실행시켜줘야한다
    {
        _endAction.Invoke(_work);
    }

    public abstract void ShowResult(); // Effect가 끝난 뒤 결과를 만들어둬야한다

    public abstract void StopAll(); // 코루틴 같은 실행 중인 것들은 전부 멈춰줘야한다

    public void SkipEffect() // Effect 연출을 스킵 - 실행시 바로 과정은 스킵하고 결과가 나오고 EndEffect 실행
    {
        StopAll();
        ShowResult();
        EndEffect();
    }
}
