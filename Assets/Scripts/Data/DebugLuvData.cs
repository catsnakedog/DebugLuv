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
    public Dictionary<int, List<List<LineData>>> Setence;
}
#endregion

#region ChoiceData
[System.Serializable]
public class ChoiceData
{
    public List<SelectData> Select;
}
#endregion