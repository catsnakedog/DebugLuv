using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Logo : UI_Base
{
    enum UI
    {
        Logo
    }

    public void StartEffect()
    {
        Wait(1, LogoFadeOut);
    }

    private void Wait(float time, Action action)
    {
        Value value = new Value();
        string waitTime = time.ToString();
        value.Value1 = waitTime;
        EffectManager.Instance.PlayEffect(typeof(Wait), Get(UI.Logo), value, action);
    }

    private void LogoFadeOut()
    {
        Value value = new Value();
        string fadeOutTime = "1";
        value.Value1 = fadeOutTime;
        EffectManager.Instance.PlayEffect(typeof(FadeOut), Get(UI.Logo), value, () =>
        {
            Wait(0.5f, GoMain);
        });
    }

    private void GoMain()
    {
        SceneManagerEX.Instance.LoadScene("Main");
    }
}
