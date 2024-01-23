using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public SaveData SaveData; // �����ϴ� ������
    public DebugLuvData DebugLuvData;
    public InGameData InGameData;
#if UNITY_EDITOR
    public CheckData CheckData;
#endif
    public GameData()
    {
        SaveData = new SaveData();
        DebugLuvData = new DebugLuvData();
        InGameData = new InGameData();
#if UNITY_EDITOR
        CheckData = new CheckData();
#endif
    }
}