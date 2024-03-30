using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 시트 전체 데이터 
/// </summary>
[System.Serializable]
public class SheetData
{
    public List<LineData> LineData;
    public List<EpisodeTypeData> EpisodeTypeData;
    public List<SelectData> SelectData;
    public List<ChImageData> ChImageData;
}

#region LineData
/// <summary>
/// 엑셀 한 문장
/// </summary>
[System.Serializable]
public class LineData
{
    public string Story;
    public int Episode;
    public int Branch;
    public string TasksOrder;
    public int SetenceIdx;
    public string Connect;
    public int Storage;
    public Task EtcTask;
    public Task BgTask;
    public Task Ch1Task;
    public Task Ch2Task;
    public Task Ch3Task;
    public Task Ch4Task;
    public BgInfo BgInfo;
    public ChInfo Ch1Info;
    public ChInfo Ch2Info;
    public ChInfo Ch3Info;
    public ChInfo Ch4Info;
    public string Name;
    public string Text;
    public string StateUI;
    public int Choice;
}

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
    public float Opacity;
    public int ChIndex = 0;
}
[System.Serializable]
public class BgInfo
{
    public string BgImage;
    public float Opacity;
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