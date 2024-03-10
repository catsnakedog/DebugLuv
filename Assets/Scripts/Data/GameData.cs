using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public SaveData SaveData; // 저장하는 데이터

    public GameData()
    {
        SaveData = new SaveData();
    }
}