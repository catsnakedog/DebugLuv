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
        Wait();
    }

    private void Wait()
    {
        Value value = new Value();
        string waitTime = "1";
        value.Value1 = waitTime;
        EffectManager.Instance.PlayEffect(typeof(Wait), Get(UI.Logo), value, LogoFadeOut);
    }

    private void LogoFadeOut()
    {
        Value value = new Value();
        string fadeOutTime = "1";
        value.Value1 = fadeOutTime;
        EffectManager.Instance.PlayEffect(typeof(FadeOut), Get(UI.Logo), value, GoMain);
    }

    private void GoMain()
    {
        SceneManagerEX.Instance.LoadScene("Main");
    }
}
