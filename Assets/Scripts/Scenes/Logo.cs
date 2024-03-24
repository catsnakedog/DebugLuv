//-------------------------------------------------------------------------------------------------
// @file	Logo.cs
//
// @brief	�� ���� �Ŵ���
//
// @date	2024-03-14
//
// Copyright 2024 Team One-eyed Games. All Rights Reserved.
//-------------------------------------------------------------------------------------------------


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logo : BaseScene
{
    private void Start()
    {
        DataManager.Instance.ParsingDebugLuvData();
        UI_Manager.GetUI<UI_Logo>().StartEffect();
    }
}
