using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : BaseScene
{
    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.Test;
        Managers.UI_Manager.ShowSceneUI<UI_Test>();
    }

    public override void Clear()
    {
        base.Init();
    }
}
