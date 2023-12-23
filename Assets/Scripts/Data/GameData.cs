using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
public class GameData
{
    public SaveData SaveData; // 저장하는 데이터
    public DebugLuvData DebugLuvData;
    public InGameData InGameData;

    public GameData()
    {
        SaveData = new SaveData();
        DebugLuvData = new DebugLuvData();
        InGameData = new InGameData();
    }
}

[System.Serializable]
public class InGameData
{
    public string ChName;
    public int StoryNumber;
    public Dictionary<int, List<TextData>> TextData;
    public Dictionary<string, Sprite> InGameSprite;

    public InGameData()
    {
        ChName = "Java";
        StoryNumber = 1;
        TextData = new Dictionary<int, List<TextData>>();
        InGameSprite = new Dictionary<string, Sprite>();
    }
    public InGameData(string chName, int storyNumber, Dictionary<int, List<TextData>> textData, Dictionary<string, Sprite> inGameSprite)
    {
        ChName = chName;
        StoryNumber = storyNumber;
        TextData = textData;
        InGameSprite = inGameSprite;
    }
}