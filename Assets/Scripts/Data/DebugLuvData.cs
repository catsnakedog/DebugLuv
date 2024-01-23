using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class DebugLuvData
{
    public StoryData[] StoryData;
    public TextData[] TextData;
    public ChoiceData[] ChoiceData;

    public DebugLuvData()
    {
        StoryData = new StoryData[0];
        TextData = new TextData[0];
        ChoiceData = new ChoiceData[0];
    }
    public DebugLuvData(StoryData[] storyData, TextData[] textData, ChoiceData[] choiceData)
    {
        StoryData = storyData;
        TextData = textData;
        ChoiceData = choiceData;
    }
}

[System.Serializable]
public class StoryData
{
    public string ChName;
    public int StoryNumber;
    public string StoryName;

    public StoryData()
    {
        ChName = string.Empty;
        StoryNumber = 0;
        StoryName = string.Empty;
    }
    public StoryData(string chName, int storyNumber, string storyName)
    {
        ChName = chName;
        StoryNumber = storyNumber;
        StoryName = storyName;
    }
}

[System.Serializable]
public class TextData
{
    public string ChName;
    public int StoryNumber;
    public int BranchNumber;
    public string TaskOrder;
    public string EtcTask;
    public string Value_Etc_1;
    public string Value_Etc_2;
    public string Value_Etc_3;
    public string Value_Etc_4;
    public string Value_Etc_5;
    public string Value_Etc_6;
    public string Value_Etc_7;
    public string Value_Etc_8;
    public string BGTask;
    public string Value_BG_1;
    public string Value_BG_2;
    public string Value_BG_3;
    public string Value_BG_4;
    public string Value_BG_5;
    public string Value_BG_6;
    public string Value_BG_7;
    public string Value_BG_8;
    public string Ch1Task;
    public string Value_Ch1_1;
    public string Value_Ch1_2;
    public string Value_Ch1_3;
    public string Value_Ch1_4;
    public string Value_Ch1_5;
    public string Value_Ch1_6;
    public string Value_Ch1_7;
    public string Value_Ch1_8;
    public string Ch2Task;
    public string Value_Ch2_1;
    public string Value_Ch2_2;
    public string Value_Ch2_3;
    public string Value_Ch2_4;
    public string Value_Ch2_5;
    public string Value_Ch2_6;
    public string Value_Ch2_7;
    public string Value_Ch2_8;
    public string Ch3Task;
    public string Value_Ch3_1;
    public string Value_Ch3_2;
    public string Value_Ch3_3;
    public string Value_Ch3_4;
    public string Value_Ch3_5;
    public string Value_Ch3_6;
    public string Value_Ch3_7;
    public string Value_Ch3_8;
    public string Ch4Task;
    public string Value_Ch4_1;
    public string Value_Ch4_2;
    public string Value_Ch4_3;
    public string Value_Ch4_4;
    public string Value_Ch4_5;
    public string Value_Ch4_6;
    public string Value_Ch4_7;
    public string Value_Ch4_8;
    public string Ch1ImageCode;
    public string Ch1Pos;
    public float Ch1Scale;
    public string Ch2ImageCode;
    public string Ch2Pos;
    public float Ch2Scale;
    public string Ch3ImageCode;
    public string Ch3Pos;
    public float Ch3Scale;
    public string Ch4ImageCode;
    public string Ch4Pos;
    public float Ch4Scale;
    public string BGImage;
    public string Name;
    public string Text;
    public int ChoiceNumber;

    public TextData()
    {
        ChName = string.Empty;
        StoryNumber = -1;
        BranchNumber = -1;
        TaskOrder = "BG/Ch\nText\nEtc";
        EtcTask = string.Empty;
        Value_Etc_1 = string.Empty;
        Value_Etc_2 = string.Empty;
        Value_Etc_3 = string.Empty;
        Value_Etc_4 = string.Empty;
        Value_Etc_5 = string.Empty;
        Value_Etc_6 = string.Empty;
        Value_Etc_7 = string.Empty;
        Value_Etc_8 = string.Empty;
        BGTask = string.Empty;
        Value_BG_1 = string.Empty;
        Value_BG_2 = string.Empty;
        Value_BG_3 = string.Empty;
        Value_BG_4 = string.Empty;
        Value_BG_5 = string.Empty;
        Value_BG_6 = string.Empty;
        Value_BG_7 = string.Empty;
        Value_BG_8 = string.Empty;
        Ch1Task = string.Empty;
        Value_Ch1_1 = string.Empty;
        Value_Ch1_2 = string.Empty;
        Value_Ch1_3 = string.Empty;
        Value_Ch1_4 = string.Empty;
        Value_Ch1_5 = string.Empty;
        Value_Ch1_6 = string.Empty;
        Value_Ch1_7 = string.Empty;
        Value_Ch1_8 = string.Empty;
        Ch2Task = string.Empty;
        Value_Ch2_1 = string.Empty;
        Value_Ch2_2 = string.Empty;
        Value_Ch2_3 = string.Empty;
        Value_Ch2_4 = string.Empty;
        Value_Ch2_5 = string.Empty;
        Value_Ch2_6 = string.Empty;
        Value_Ch2_7 = string.Empty;
        Value_Ch2_8 = string.Empty;
        Ch3Task = string.Empty;
        Value_Ch3_1 = string.Empty;
        Value_Ch3_2 = string.Empty;
        Value_Ch3_3 = string.Empty;
        Value_Ch3_4 = string.Empty;
        Value_Ch3_5 = string.Empty;
        Value_Ch3_6 = string.Empty;
        Value_Ch3_7 = string.Empty;
        Value_Ch3_8 = string.Empty;
        Ch4Task = string.Empty;
        Value_Ch4_1 = string.Empty;
        Value_Ch4_2 = string.Empty;
        Value_Ch4_3 = string.Empty;
        Value_Ch4_4 = string.Empty;
        Value_Ch4_5 = string.Empty;
        Value_Ch4_6 = string.Empty;
        Value_Ch4_7 = string.Empty;
        Value_Ch4_8 = string.Empty;
        Ch1ImageCode = string.Empty;
        Ch1Pos = string.Empty;
        Ch1Scale = 1;
        Ch2ImageCode = string.Empty;
        Ch2Pos = string.Empty;
        Ch2Scale = 1;
        Ch3ImageCode = string.Empty;
        Ch3Pos = string.Empty;
        Ch3Scale = 1;
        Ch4ImageCode = string.Empty;
        Ch4Pos = string.Empty;
        Ch4Scale = 1;
        BGImage = string.Empty;
        Name = string.Empty;
        Text = string.Empty;
        ChoiceNumber = -1;
    }
    public TextData(string chName, int storyNumber, int branchNumber, string taskOrder, string etcTask, string value_Etc_1, string value_Etc_2, string value_Etc_3, string value_Etc_4, string value_Etc_5, string value_Etc_6, string value_Etc_7, string value_Etc_8, string bGTask, string value_BG_1, string value_BG_2, string value_BG_3, string value_BG_4, string value_BG_5, string value_BG_6, string value_BG_7, string value_BG_8, string ch1Task, string value_Ch1_1, string value_Ch1_2, string value_Ch1_3, string value_Ch1_4, string value_Ch1_5, string value_Ch1_6, string value_Ch1_7, string value_Ch1_8, string ch2Task, string value_Ch2_1, string value_Ch2_2, string value_Ch2_3, string value_Ch2_4, string value_Ch2_5, string value_Ch2_6, string value_Ch2_7, string value_Ch2_8, string ch3Task, string value_Ch3_1, string value_Ch3_2, string value_Ch3_3, string value_Ch3_4, string value_Ch3_5, string value_Ch3_6, string value_Ch3_7, string value_Ch3_8, string ch4Task, string value_Ch4_1, string value_Ch4_2, string value_Ch4_3, string value_Ch4_4, string value_Ch4_5, string value_Ch4_6, string value_Ch4_7, string value_Ch4_8, string ch1ImageCode, string ch1Pos, float ch1Scale, string ch2ImageCode, string ch2Pos, float ch2Scale, string ch3ImageCode, string ch3Pos, float ch3Scale, string ch4ImageCode, string ch4Pos, float ch4Scale, string bGImage, string name, string text, int choiceNumber)
    {
        ChName = chName;
        StoryNumber = storyNumber;
        BranchNumber = branchNumber;
        TaskOrder = taskOrder;
        EtcTask = etcTask;
        Value_Etc_1 = value_Etc_1;
        Value_Etc_2 = value_Etc_2;
        Value_Etc_3 = value_Etc_3;
        Value_Etc_4 = value_Etc_4;
        Value_Etc_5 = value_Etc_5;
        Value_Etc_6 = value_Etc_6;
        Value_Etc_7 = value_Etc_7;
        Value_Etc_8 = value_Etc_8;
        BGTask = bGTask;
        Value_BG_1 = value_BG_1;
        Value_BG_2 = value_BG_2;
        Value_BG_3 = value_BG_3;
        Value_BG_4 = value_BG_4;
        Value_BG_5 = value_BG_5;
        Value_BG_6 = value_BG_6;
        Value_BG_7 = value_BG_7;
        Value_BG_8 = value_BG_8;
        Ch1Task = ch1Task;
        Value_Ch1_1 = value_Ch1_1;
        Value_Ch1_2 = value_Ch1_2;
        Value_Ch1_3 = value_Ch1_3;
        Value_Ch1_4 = value_Ch1_4;
        Value_Ch1_5 = value_Ch1_5;
        Value_Ch1_6 = value_Ch1_6;
        Value_Ch1_7 = value_Ch1_7;
        Value_Ch1_8 = value_Ch1_8;
        Ch2Task = ch2Task;
        Value_Ch2_1 = value_Ch2_1;
        Value_Ch2_2 = value_Ch2_2;
        Value_Ch2_3 = value_Ch2_3;
        Value_Ch2_4 = value_Ch2_4;
        Value_Ch2_5 = value_Ch2_5;
        Value_Ch2_6 = value_Ch2_6;
        Value_Ch2_7 = value_Ch2_7;
        Value_Ch2_8 = value_Ch2_8;
        Ch3Task = ch3Task;
        Value_Ch3_1 = value_Ch3_1;
        Value_Ch3_2 = value_Ch3_2;
        Value_Ch3_3 = value_Ch3_3;
        Value_Ch3_4 = value_Ch3_4;
        Value_Ch3_5 = value_Ch3_5;
        Value_Ch3_6 = value_Ch3_6;
        Value_Ch3_7 = value_Ch3_7;
        Value_Ch3_8 = value_Ch3_8;
        Ch4Task = ch4Task;
        Value_Ch4_1 = value_Ch4_1;
        Value_Ch4_2 = value_Ch4_2;
        Value_Ch4_3 = value_Ch4_3;
        Value_Ch4_4 = value_Ch4_4;
        Value_Ch4_5 = value_Ch4_5;
        Value_Ch4_6 = value_Ch4_6;
        Value_Ch4_7 = value_Ch4_7;
        Value_Ch4_8 = value_Ch4_8;
        Ch1ImageCode = ch1ImageCode;
        Ch1Pos = ch1Pos;
        Ch1Scale = ch1Scale;
        Ch2ImageCode = ch2ImageCode;
        Ch2Pos = ch2Pos;
        Ch2Scale = ch2Scale;
        Ch3ImageCode = ch3ImageCode;
        Ch3Pos = ch3Pos;
        Ch3Scale = ch3Scale;
        Ch4ImageCode = ch4ImageCode;
        Ch4Pos = ch4Pos;
        Ch4Scale = ch4Scale;
        BGImage = bGImage;
        Name = name;
        Text = text;
        ChoiceNumber = choiceNumber;
    }
}

[System.Serializable]
public class ChoiceData
{
    public int ChoiceNumber;
    public int BranchNumber;
    public string Text;

    public ChoiceData()
    {
        ChoiceNumber = 0;
        BranchNumber = 0;
        Text = string.Empty;
    }
    public ChoiceData(int choiceNumber, int branchNumber, string text)
    {
        ChoiceNumber = choiceNumber;
        BranchNumber = branchNumber;
        Text = text;
    }
}