using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public SaveData SaveData; // �����ϴ� ������

    public GameData()
    {
        SaveData = new SaveData();
    }
}