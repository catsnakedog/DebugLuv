using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Scene : UI_Base
{
    public override void Init()
    {
        Managers.UI_Manager.SetCanavas(gameObject, false); // SceneUI같은 경우 가장 기본이 되기 때문에 굳이 정렬해줄 필요가 없어 false를 전달해준다
    }
}
