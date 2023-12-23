using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : BaseScene
{
    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.Main;
        Managers.UI_Manager.ShowSceneUI<UI_Main>();
    }

    public override void Clear()
    {
        base.Init();
    }
}
