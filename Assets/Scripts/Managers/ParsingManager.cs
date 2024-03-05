using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParsingManager
{
    private TextAsset _episodeTypeData;
    private TextAsset _storyData;
    private TextAsset _choiceData;
    private TextAsset _chImageData;

    public void StartParsing(out DebugLuvData data)
    {
        data = new DebugLuvData();

        SetData();
        ParsingEpisodeTypeData(out data.EpisodeType);
        ParsingStoryData(out data.Story);
        ParsingChoiceData(out data.Choice);
        ParsingChImageData(out data.ChImage);
    }

    void SetData()
    {
        _episodeTypeData = Resources.Load("Data/EpisodeTypeData") as TextAsset;
        _storyData = Resources.Load("Data/StoryData") as TextAsset;
        _choiceData = Resources.Load("Data/ChoiceData") as TextAsset;
        _chImageData = Resources.Load("Data/ChImageData") as TextAsset;
    }

    #region ParsingEpisodeTypeData
    void ParsingEpisodeTypeData(out List<EpisodeTypeData> data)
    {
        data = new();

        string[] lines = _episodeTypeData.text.Split("\n");

        foreach (string line in lines)
        {
            EpisodeTypeData episodeTypeData = MakeEpisodeTypeData(line);
            if (episodeTypeData == null)
                continue;
            
            data.Add(episodeTypeData);
        }
    }

    EpisodeTypeData MakeEpisodeTypeData(string line)
    {
        if (CheckNullLine(line))
            return null;

        EpisodeTypeData episodeTypeData = new();
        string[] fields = line.Split("\t");
        for(int i = 0; i < fields.Length; i++)
            fields[i] = fields[i].Trim();

        episodeTypeData.Story = fields[0];
        episodeTypeData.Episode = Util.StringToInt(fields[1]);
        episodeTypeData.StoryName = fields[2];

        return episodeTypeData;
    }
#endregion

    #region ParsingStoryData
    void ParsingStoryData(out Dictionary<string, StoryData> data)
    {
        data = new();
        string[] lines = _storyData.text.Split("\n");

        string crruentStory = "";
        int crruentEpisode = 0;

        foreach (string line in lines)
        {
            SetenceData setenceData = MakeSetenceData(line);
            if (setenceData == null)
                continue;

            if (string.IsNullOrEmpty(setenceData.Story) || setenceData.Episode == -1 || setenceData.Branch == -1)
            {
                data[crruentStory].Episode[crruentEpisode].Setence[^1].Add(setenceData);
                continue;
            }

            if (!data.ContainsKey(setenceData.Story))
            {
                data[setenceData.Story] = new();
                data[setenceData.Story].Episode = new();
            }

            if (!data[setenceData.Story].Episode.ContainsKey(setenceData.Episode))
            {
                data[setenceData.Story].Episode[setenceData.Episode] = new();
                data[setenceData.Story].Episode[setenceData.Episode].Setence = new();
            }

            data[setenceData.Story].Episode[setenceData.Episode].Setence.Add(new());
            data[setenceData.Story].Episode[setenceData.Episode].Setence[^1].Add(setenceData);

            crruentStory = setenceData.Story;
            crruentEpisode = setenceData.Episode;
        }
    }

    SetenceData MakeSetenceData(string line)
    {
        if (CheckNullLine(line))
            return null;

        SetenceData setenceData = new();
        string[] fields = line.Split("\t");
        for (int i = 0; i < fields.Length; i++)
            fields[i] = fields[i].Trim();

        setenceData.Story = fields[0];
        setenceData.Episode = Util.StringToInt(fields[1]);
        setenceData.Branch = Util.StringToInt(fields[2]);
        setenceData.TasksOrder = fields[3];
        setenceData.Type = fields[4];
        setenceData.Plus = Util.StringToInt(fields[5]);
        setenceData.Storage = Util.StringToInt(fields[6]);
        int num = 7;
        setenceData.EtcTask = MakeTask(fields[num], new string[8] { fields[num + 1], fields[num + 2], fields[num + 3], fields[num + 4], fields[num + 5], fields[num + 6], fields[num + 7], fields[num + 8] } );
        num = 16;
        setenceData.BgTask = MakeTask(fields[num], new string[8] { fields[num + 1], fields[num + 2], fields[num + 3], fields[num + 4], fields[num + 5], fields[num + 6], fields[num + 7], fields[num + 8] });
        num = 25;
        setenceData.Ch1Task = MakeTask(fields[num], new string[8] { fields[num + 1], fields[num + 2], fields[num + 3], fields[num + 4], fields[num + 5], fields[num + 6], fields[num + 7], fields[num + 8] });
        num = 34;
        setenceData.Ch2Task = MakeTask(fields[num], new string[8] { fields[num + 1], fields[num + 2], fields[num + 3], fields[num + 4], fields[num + 5], fields[num + 6], fields[num + 7], fields[num + 8] });
        num = 43;
        setenceData.Ch3Task = MakeTask(fields[num], new string[8] { fields[num + 1], fields[num + 2], fields[num + 3], fields[num + 4], fields[num + 5], fields[num + 6], fields[num + 7], fields[num + 8] });
        num = 52;
        setenceData.Ch4Task = MakeTask(fields[num], new string[8] { fields[num + 1], fields[num + 2], fields[num + 3], fields[num + 4], fields[num + 5], fields[num + 6], fields[num + 7], fields[num + 8] });
        num = 61;
        setenceData.Ch1Info = MakeChInfo(fields[num], fields[num + 1], fields[num + 2]);
        num = 64;
        setenceData.Ch2Info = MakeChInfo(fields[num], fields[num + 1], fields[num + 2]);
        num = 67;
        setenceData.Ch3Info = MakeChInfo(fields[num], fields[num + 1], fields[num + 2]);
        num = 70;
        setenceData.Ch4Info = MakeChInfo(fields[num], fields[num + 1], fields[num + 2]);
        setenceData.BgImage = fields[73];
        setenceData.Name = fields[74];
        setenceData.Text = fields[75];
        setenceData.Choice = Util.StringToInt(fields[76]);

        return setenceData;
    }

    Task MakeTask(string name, string[] values)
    {
        Task task = new();

        task.Name = name;
        task.Value = MakeValue(values);

        return task;
    }

    Value MakeValue(string[] values)
    {
        Value value = new();

        value.Value1 = values[0];
        value.Value2 = values[1];
        value.Value3 = values[2];
        value.Value4 = values[3];
        value.Value5 = values[4];
        value.Value6 = values[5];
        value.Value7 = values[6];
        value.Value8 = values[7];

        return value;
    }

    ChInfo MakeChInfo(string imageCode, string pos, string scale)
    {
        ChInfo chInfo = new();

        chInfo.ImageCode = imageCode;
        chInfo.Pos = Util.StringToVector2(pos);
        chInfo.Scale = Util.StringToFloat(scale);

        return chInfo;
    }
#endregion

    #region  ParsingChoiceData
    void ParsingChoiceData(out Dictionary<int, ChoiceData> data)
    {
        data = new();
        string[] lines = _choiceData.text.Split("\n");

        foreach(string line in lines)
        {
            SelectData selectData = MakeSelectData(line);
            if (selectData == null)
                continue;

            if (!data.ContainsKey(selectData.Choice))
            {
                data[selectData.Choice] = new();
                data[selectData.Choice].Select = new();
            }

            data[selectData.Choice].Select.Add(selectData);
        }
    }

    SelectData MakeSelectData(string line)
    {
        if (CheckNullLine(line))
            return null;

        SelectData selectData = new();
        string[] fields = line.Split("\t");
        for (int i = 0; i < fields.Length; i++)
            fields[i] = fields[i].Trim();

        selectData.Choice = Util.StringToInt(fields[0]);
        selectData.Branch = Util.StringToInt(fields[1]);
        selectData.Text= fields[2];

        return selectData;
    }
    #endregion

    #region ParsingChImageData
    void ParsingChImageData(out Dictionary<int, Dictionary<int, ChImageData>> data)
    {
        data = new();
        string[] lines = _chImageData.text.Split("\n");

        foreach (string line in lines)
        {
            ChImageData chImageData = MakeChImageData(line);
            if (chImageData == null)
                continue;

            if (!data.ContainsKey(chImageData.ChCode))
            {
                data[chImageData.ChCode] = new();
                data[chImageData.ChCode][chImageData.BodyCode] = new();
            }

            data[chImageData.ChCode][chImageData.BodyCode] = chImageData;
        }
    }

    ChImageData MakeChImageData(string line)
    {
        if (CheckNullLine(line))
            return null;

        ChImageData chImageData = new();
        string[] fields = line.Split("\t");
        for (int i = 0; i < fields.Length; i++)
            fields[i] = fields[i].Trim();

        chImageData.ChCode = Util.StringToInt(fields[0]);
        chImageData.BodyCode = Util.StringToInt(fields[1]);
        chImageData.FacePos = Util.StringToVector2(fields[2]);
        chImageData.ClothesPos = Util.StringToVector2(fields[2]);

        return chImageData;
    }
    #endregion

    bool CheckNullLine(string line)
    {
        bool isNullLine = true;

        string[] fields = line.Split("\t");
        for (int i = 0; i < fields.Length; i++)
            fields[i] = fields[i].Trim();

        foreach (string field in fields)
        {
            if (!string.IsNullOrEmpty(field))
            {
                isNullLine = false;
                break;
            }
        }

        return isNullLine;
    }
}
