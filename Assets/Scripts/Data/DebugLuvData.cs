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
        ChName = "";
        StoryNumber = 1;
        StoryName = "";
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
    public string BGTask;
    public string Ch1Task;
    public string Ch2Task;
    public string Ch3Task;
    public string Ch4Task;
    public string Ch1Image;
    public float Ch1Scale;
    public string Ch2Image;
    public float Ch2Scale;
    public string Ch3Image;
    public float Ch3Scale;
    public string Ch4Image;
    public float Ch4Scale;
    public string BGImage;
    public string Name;
    public string Text;
    public int ChoiceNumber;
    public string Value1;
    public string Value2;
    public string Value3;
    public string Value4;
    public string Value5;
    public string Value6;
    public string Value7;
    public string Value8;

    public TextData()
    {
        ChName = "Java";
        StoryNumber = 1;
        BranchNumber = 1;
        TaskOrder = "BG/Ch\nText";
        BGTask = "";
        Ch1Task = "";
        Ch2Task = "";
        Ch3Task = "";
        Ch4Task = "";
        Ch1Image = "JavaDefault";
        Ch1Scale = 1f;
        Ch2Image = "";
        Ch2Scale = 0f;
        Ch3Image = "";
        Ch3Scale = 0f;
        Ch4Image = "";
        Ch4Scale = 0f;
        BGImage = "JavaHouse";
        Name = "자바";
        Text = "자바는 자바자바하고 웃는데요";
        ChoiceNumber = 0;
        Value1 = "";
        Value2 = "";
        Value3 = "";
        Value4 = "";
        Value5 = "";
        Value6 = "";
        Value7 = "";
        Value8 = "";
    }
    public TextData(string chName, int storyNumber, int branchNumber, string taskOrder, string bGTask, string ch1Task, string ch2Task, string ch3Task, string ch4Task, string ch1Image, float ch1Scale, string ch2Image, float ch2Scale, string ch3Image, float ch3Scale, string ch4Image, float ch4Scale, string bGImage, string name, string text, int choiceNumber, string value1, string value2, string value3, string value4, string value5, string value6, string value7, string value8)
    {
        ChName = chName;
        StoryNumber = storyNumber;
        BranchNumber = branchNumber;
        TaskOrder = taskOrder;
        BGTask = bGTask;
        Ch1Task = ch1Task;
        Ch2Task = ch2Task;
        Ch3Task = ch3Task;
        Ch4Task = ch4Task;
        Ch1Image = ch1Image;
        Ch1Scale = ch1Scale;
        Ch2Image = ch2Image;
        Ch2Scale = ch2Scale;
        Ch3Image = ch3Image;
        Ch3Scale = ch3Scale;
        Ch4Image = ch4Image;
        Ch4Scale = ch4Scale;
        BGImage = bGImage;
        Name = name;
        Text = text;
        ChoiceNumber = choiceNumber;
        Value1 = value1;
        Value2 = value2;
        Value3 = value3;
        Value4 = value4;
        Value5 = value5;
        Value6 = value6;
        Value7 = value7;
        Value8 = value8;
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
        Text = "";
    }
    public ChoiceData(int choiceNumber, int branchNumber, string text)
    {
        ChoiceNumber = choiceNumber;
        BranchNumber = branchNumber;
        Text = text;
    }
}