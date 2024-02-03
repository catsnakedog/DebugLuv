using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InGameData
{
    public string ChName;
    public int StoryNumber;
    public Dictionary<string, Dictionary<int, Dictionary<int, List<List<TextData>>>>> TextData; // DebugLuvData.ChName - DebugLuvData.Branch - 해당 브랜치 데이터 묶음 - 해당 브랜치 데이터
    public Dictionary<string, Sprite> InGameSprite;

    public InGameData()
    {
        ChName = "Java";
        StoryNumber = 1;
        TextData = new();
        InGameSprite = new();
    }
    public InGameData(string chName, int storyNumber, Dictionary<string, Dictionary<int, Dictionary<int, List<List<TextData>>>>> textData, Dictionary<string, Sprite> inGameSprite)
    {
        ChName = chName;
        StoryNumber = storyNumber;
        TextData = textData;
        InGameSprite = inGameSprite;
    }
}