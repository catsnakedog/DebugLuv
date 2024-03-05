using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logo : BaseScene
{
    private void Start()
    {
        DataManager.Instance.ParsingDebugLuvData();
        UI_Manager.Instance.GetUI<UI_Logo>().StartEffect();
    }
}
