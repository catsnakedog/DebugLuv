using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region DebugLuvData
[System.Serializable]
public class DebugLuvData
{
    public List<EpisodeTypeData> EpisodeType;
    public Dictionary<string, StoryData> Story; // ChName, StoryData
    public Dictionary<int, ChoiceData> Choice; // Choice, ChoiceData
    public Dictionary<int, Dictionary<int, ChImageData>> ChImage;
}
#endregion

#region  StoryData
[System.Serializable]
public class StoryData
{
    public Dictionary<int, EpisodeData> Episode; //Episode, EpisodeData
}
#endregion

#region EpisodeData
[System.Serializable]
public class EpisodeData
{
    public List<List<SetenceData>> Setence;
}
#endregion

#region SentenceData
[System.Serializable]
public class SetenceData
{
    public string Story;
    public int Episode;
    public int Branch;
    public string TasksOrder;
    public string Type;
    public int Plus;
    public int Storage;
    public Task EtcTask;
    public Task BgTask;
    public Task Ch1Task;
    public Task Ch2Task;
    public Task Ch3Task;
    public Task Ch4Task;
    public ChInfo Ch1Info;
    public ChInfo Ch2Info;
    public ChInfo Ch3Info;
    public ChInfo Ch4Info;
    public string BgImage;
    public string Name;
    public string Text;
    public int Choice;
}
#endregion

#region Etc
[System.Serializable]
public class Task
{
    public string Name;
    public Value Value;
}
[System.Serializable]
public class Value
{
    public string Value1;
    public string Value2;
    public string Value3;
    public string Value4;
    public string Value5;
    public string Value6;
    public string Value7;
    public string Value8;
}
[System.Serializable]
public class ChInfo
{
    public string ImageCode;
    public Vector2 Pos;
    public float Scale;
}
#endregion

#region EpisodeTypeData
[System.Serializable]
public class EpisodeTypeData
{
    public string Story;
    public int Episode;
    public string StoryName;
}
#endregion

#region ChoiceData
[System.Serializable]
public class ChoiceData
{
    public List<SelectData> Select;
}
#endregion

#region SelectData
[System.Serializable]
public class SelectData
{
    public int Choice;
    public int Branch;
    public string Text;
}
#endregion

#region ChImageData
[System.Serializable]
public class ChImageData
{
    public int ChCode;
    public int BodyCode;
    public Vector2 FacePos;
    public Vector2 ClothesPos;
}
#endregion