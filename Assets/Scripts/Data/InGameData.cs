using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InGameData
{
    public string ChName;
    public int StoryNumber;

    public InGameData()
    {
        ChName = "Java";
        StoryNumber = 1;

    }
    public InGameData(string chName, int storyNumber)
    {
        ChName = chName;
        StoryNumber = storyNumber;
    }
}