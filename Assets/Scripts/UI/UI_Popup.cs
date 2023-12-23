using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Popup : UI_Base
{
    public override void Init()
    {
        Managers.UI_Manager.SetCanavas(gameObject, true); // 팝업같은 경우 정렬이 필요함으로 true값을 전달한다
    }

    public virtual void ClosePopupUI()
    {
        Managers.UI_Manager.ClosePopupUI(this);
    }
}
