using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class DataManager : ManagerSingle<DataManager>, IInit // �����͸� �����ϴ� Manager�̴�
{
    private JsonManager _jsonManager; // Json �����͸� �а�, ���� Manager�̴�
    private ParsingManager _parsingManager;
    public DebugLuvData DebugLuvData;

    public void Init()
    {
        GameObject data = GameObject.Find("@Data"); // Data�� ������ �ð������� Ȯ���ϱ� ���ؼ� @Data��� GameObject�� ���� ���� �������ش�
        if (data == null)
        {
            GameObject root = GameObject.Find("@Root");
            if (root == null)
                root = new GameObject("@Root");

            GameData gameData = new GameData();

            _jsonManager = new JsonManager();
            _parsingManager = new ParsingManager();

            LoadJsonData(gameData);

            data = new GameObject { name = "@Data" };
            data.transform.SetParent(root.transform);
            data.AddComponent<Data>(); // @Data��� ���� ������Ʈ�� ���� Data ��ũ��Ʈ�� �޾��ش�
            Data.GameData = gameData; // �����ʹ� Data��� Ŭ������ �����Ѵ�, �����Ϳ� ������ ���� Data.GameData�� �����ϸ� �ȴ�
        }
    }

    public void ParsingDebugLuvData()
    {
        _parsingManager.StartParsing(out DebugLuvData); // tsv �������� ����� �����͵��� �����Ѵ�
    }

    public void LoadJsonData(GameData gameData)
    {
        _jsonManager.LoadJsonData<SaveData>("SaveData", out gameData.SaveData); // Json�����͵��� �����´�
        _jsonManager.LoadJsonData<CheckData>("CheckData", out gameData.CheckData);
    }

    public void Save() // SaveData�� �����Ѵ�
    {
        _jsonManager.SaveJson(Data.GameData.SaveData);
    }

    public EpisodeData GetEpisodeData(string Story, int Episode)
    {
        return DebugLuvData.Story[Story].Episode[Episode];
    }

    /*
    public void TextDataSetting() // �ش� ChName, StoryNumber�� �����͸� �����Ѵ�
    {
#if UNITY_EDITOR
        List<string> bugReport = StartBugReport();
#endif

#if UNITY_EDITOR
            string bugFirst;
            if (!FirstCheckTextData(textData, cnt, isExtraTask, out bugFirst))
            {
                bugReport.Add(bugFirst);
            }
#endif

#if UNITY_EDITOR
                if(target.Count == 0)
                {
                    bugReport.Add($"{chName}-{storyNumber}-{branchNumber} : First Paragraph is ExtraTask");
                    return;
                }
#endif
#if UNITY_EDITOR
        string bugLast;
        if(!LastCheckTextData(out bugLast))
        {
            bugReport.Add(bugLast);
        }

        MakeBugReport(bugReport);
#endif
    }

    public void ClearTextData()
    {
        Data.GameData.InGameData.TextData = new();
    }

    bool FirstCheckTextData(TextData data, int cnt, bool isExtraTask, out string bugReport) // �̻��� �����Ͱ� �ִ��� �˻�
    {
        var checkData = Data.GameData.CheckData;
        bool isVaildData = true;
        StringBuilder bug = new StringBuilder($"{cnt + 7}�� �� : ");

        string[] vaildStory = checkData.VaildStory.Split("|", StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < vaildStory.Length; i++)
        {
            vaildStory[i] = vaildStory[i].Trim();
        }
        string[] vaildChName = checkData.VaildChName.Split("|", StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < vaildChName.Length; i++)
        {
            vaildChName[i] = vaildChName[i].Trim();
        }
        string[] vaildTask = checkData.VaildTask.Split("|", StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < vaildTask.Length; i++)
        {
            vaildTask[i] = vaildTask[i].Trim();
        }
        string[] vaildTaskValue = checkData.VaildTaskValue.Split("|");
        for (int i = 0; i < vaildTaskValue.Length; i++)
        {
            vaildTaskValue[i] = vaildTaskValue[i].Trim();
        }
        string[] vaildTaskOrder = checkData.VaildTaskOrder.Split("|", StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < vaildTaskOrder.Length; i++)
        {
            vaildTaskOrder[i] = vaildTaskOrder[i].Trim();
        }
        string[] vaildImage = checkData.VaildImage.Split("|", StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < vaildImage.Length; i++)
        {
            vaildImage[i] = vaildImage[i].Trim();
        }
        string[] vaildChoiceNumber = checkData.VaildChoiceNumber.Split("|", StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < vaildChoiceNumber.Length; i++)
        {
            vaildChoiceNumber[i] = vaildChoiceNumber[i].Trim();
        }

        if(!isExtraTask)
        {
            // Story üũ
            int storyNumber;

            switch (data.ChName)
            {
                case "Com":
                    storyNumber = 10;
                    break;
                case "Java":
                    storyNumber = 20;
                    break;
                case "Python":
                    storyNumber = 30;
                    break;
                case "C#":
                    storyNumber = 40;
                    break;
                default:
                    storyNumber = 0;
                    break;
            }
            storyNumber += data.StoryNumber;

            if (vaildStory.Contains(storyNumber.ToString()))
            {
                bug.Append("| Story ");
                isVaildData = false;
            }

            // ChName üũ
            if (!vaildChName.Contains(data.ChName))
            {
                bug.Append("| ChName ");
                isVaildData = false;
            }
        }

        // TaskOrder üũ
        string[] lines = data.TaskOrder.Split("\n");
        List<string> taskList = new();

        foreach(string line in lines)
        {
            line.Trim();
            string[] orders = line.Split("-");

            foreach(string order in  orders)
            {
                order.Trim();
                string[] tasks = order.Split("/");

                foreach(string task in tasks)
                {
                    task.Trim();
                    taskList.Add(task);
                }
            }
        }

        foreach(string task in taskList)
        {
            if (!vaildTaskOrder.Contains(task))
            {
                bug.Append("| TaskOrder ");
                isVaildData = false;
            }
        }

        // Task üũ
        Dictionary<string, bool> checkTaskVaild = new();
        if (!string.IsNullOrEmpty(data.EtcTask))
            checkTaskVaild["EtcTask"] = vaildTask.Contains(data.EtcTask);
        if (!string.IsNullOrEmpty(data.BGTask))
            checkTaskVaild["BGTask"] = vaildTask.Contains(data.BGTask);
        if (!string.IsNullOrEmpty(data.Ch1Task))
            checkTaskVaild["Ch1Task"] = vaildTask.Contains(data.Ch1Task);
        if (!string.IsNullOrEmpty(data.Ch2Task))
            checkTaskVaild["Ch2Task"] = vaildTask.Contains(data.Ch2Task);
        if (!string.IsNullOrEmpty(data.Ch3Task))
            checkTaskVaild["Ch3Task"] = vaildTask.Contains(data.Ch3Task);
        if (!string.IsNullOrEmpty(data.Ch4Task))
            checkTaskVaild["Ch4Task"] = vaildTask.Contains(data.Ch4Task);

        foreach(string key in checkTaskVaild.Keys)
        {
            if (!checkTaskVaild[key])
            {
                bug.Append($"| {key} ");
                isVaildData = false;
            }
        }

        // value üũ
        string[] values = new string[] { data.Value_Etc_1, data.Value_Etc_2, data.Value_Etc_3, data.Value_Etc_4, data.Value_Etc_5, data.Value_Etc_6, data.Value_Etc_7, data.Value_Etc_8,
                                        data.Value_BG_1, data.Value_BG_2, data.Value_BG_3, data.Value_BG_4, data.Value_BG_5, data.Value_BG_6, data.Value_BG_7, data.Value_BG_8,
                                        data.Value_Ch1_1, data.Value_Ch1_2, data.Value_Ch1_3, data.Value_Ch1_4, data.Value_Ch1_5, data.Value_Ch1_6, data.Value_Ch1_7, data.Value_Ch1_8,
                                        data.Value_Ch2_1, data.Value_Ch2_2, data.Value_Ch2_3, data.Value_Ch2_4, data.Value_Ch2_5, data.Value_Ch2_6, data.Value_Ch2_7, data.Value_Ch2_8,
                                        data.Value_Ch3_1, data.Value_Ch3_2, data.Value_Ch3_3, data.Value_Ch3_4, data.Value_Ch3_5, data.Value_Ch3_6, data.Value_Ch3_7, data.Value_Ch3_8,
                                        data.Value_Ch4_1, data.Value_Ch4_2, data.Value_Ch4_3, data.Value_Ch4_4, data.Value_Ch4_5, data.Value_Ch4_6, data.Value_Ch4_7, data.Value_Ch4_8
                                        };
        string[] dataTasks = new string[] { data.EtcTask, data.BGTask, data.Ch1Task, data.Ch2Task, data.Ch3Task, data.Ch4Task };
        int[] valueKeys = new int[] { 0, 1, 2, 3, 4, 5};

        for(int i = 0; i < valueKeys.Length; i++)
        {
            string[] taskValues;
            if (string.IsNullOrEmpty(dataTasks[i]))
            {
                continue;
            }
            else
            {
                int index = Array.IndexOf(vaildTask, dataTasks[i]);
                taskValues = vaildTaskValue[index].Split(",");
            }

            int taskValueCnt = 0;
            string valueBugReport;
            foreach(string taskValue in taskValues)
            {
                int valueNumber= valueKeys[i] * 8 + taskValueCnt;

                isVaildData = CheckValue(values[valueNumber], taskValue.Split('_')[1].Trim(), cnt, valueNumber, out valueBugReport);
                bug.Append(valueBugReport);
                taskValueCnt++;
            }
        }

        if(!isExtraTask)
        {
            // BGImage üũ
            if (!vaildImage.Contains(data.BGImage))
            {
                bug.Append("| BGImage ");
                isVaildData = false;
            }

            // ChoiceNumber üũ
            if (data.ChoiceNumber != -1 && !vaildChoiceNumber.Contains(data.ChoiceNumber.ToString()))
            {
                bug.Append("| ChoiceNumber ");
                isVaildData = false;
            }

            // ImageCode üũ
            if (!string.IsNullOrEmpty(data.Ch1ImageCode) && data.Ch1ImageCode.Split("-").Length != 6)
            {
                bug.Append("| Ch1ImageCode ");
                isVaildData = false;
            }
            if (!string.IsNullOrEmpty(data.Ch2ImageCode) && data.Ch2ImageCode.Split("-").Length != 6)
            {
                bug.Append("| Ch2ImageCode ");
                isVaildData = false;
            }
            if (!string.IsNullOrEmpty(data.Ch3ImageCode) && data.Ch3ImageCode.Split("-").Length != 6)
            {
                bug.Append("| Ch3ImageCode ");
                isVaildData = false;
            }
            if (!string.IsNullOrEmpty(data.Ch4ImageCode) && data.Ch4ImageCode.Split("-").Length != 6)
            {
                bug.Append("| Ch4ImageCode ");
                isVaildData = false;
            }

            // ChPos üũ
            float x;
            float y;
            bool isVaildVector2;

            if (!string.IsNullOrEmpty(data.Ch1ImageCode))
            {
                string[] datas = data.Ch1Pos.Split(",");
                if (datas.Length != 2)
                {
                    bug.Append("| Ch1Pos ");
                    isVaildData = false;
                }
                else
                {
                    isVaildVector2 = float.TryParse(datas[0].Trim(), out x);
                    if (!isVaildVector2)
                    {
                        bug.Append("| Ch1Pos ");
                        isVaildData = false;
                    }
                    isVaildVector2 = float.TryParse(datas[1].Trim(), out y);
                    if (!isVaildVector2)
                    {
                        bug.Append("| Ch1Pos ");
                        isVaildData = false;
                    }
                }
            }
            if (!string.IsNullOrEmpty(data.Ch2ImageCode))
            {
                string[] datas = data.Ch2Pos.Split(",");
                if (datas.Length != 2)
                {
                    bug.Append("| Ch2Pos ");
                    isVaildData = false;
                }
                else
                {
                    isVaildVector2 = float.TryParse(datas[0].Trim(), out x);
                    if (!isVaildVector2)
                    {
                        bug.Append("| Ch2Pos ");
                        isVaildData = false;
                    }
                    isVaildVector2 = float.TryParse(datas[1].Trim(), out y);
                    if (!isVaildVector2)
                    {
                        bug.Append("| Ch2Pos ");
                        isVaildData = false;
                    }
                }
            }
            if (!string.IsNullOrEmpty(data.Ch3ImageCode))
            {
                string[] datas = data.Ch3Pos.Split(",");
                if (datas.Length != 2)
                {
                    bug.Append("| Ch3Pos ");
                    isVaildData = false;
                }
                else
                {
                    isVaildVector2 = float.TryParse(datas[0].Trim(), out x);
                    if (!isVaildVector2)
                    {
                        bug.Append("| Ch3Pos ");
                        isVaildData = false;
                    }
                    isVaildVector2 = float.TryParse(datas[1].Trim(), out y);
                    if (!isVaildVector2)
                    {
                        bug.Append("| Ch3Pos ");
                        isVaildData = false;
                    }
                }
            }
            if (!string.IsNullOrEmpty(data.Ch4ImageCode))
            {
                string[] datas = data.Ch4Pos.Split(",");
                if (datas.Length != 2)
                {
                    bug.Append("| Ch4Pos ");
                    isVaildData = false;
                }
                else
                {
                    isVaildVector2 = float.TryParse(datas[0].Trim(), out x);
                    if (!isVaildVector2)
                    {
                        bug.Append("| Ch4Pos ");
                        isVaildData = false;
                    }
                    isVaildVector2 = float.TryParse(datas[1].Trim(), out y);
                    if (!isVaildVector2)
                    {
                        bug.Append("| Ch4Pos ");
                        isVaildData = false;
                    }
                }
            }

            // ChScale üũ
            if (!string.IsNullOrEmpty(data.Ch1ImageCode))
            {
                if (data.Ch1Scale <= 0)
                {
                    bug.Append("| Ch1Scale ");
                    isVaildData = false;
                }
            }
            if (!string.IsNullOrEmpty(data.Ch2ImageCode))
            {
                if (data.Ch2Scale <= 0)
                {
                    bug.Append("| Ch2Scale ");
                    isVaildData = false;
                }
            }
            if (!string.IsNullOrEmpty(data.Ch3ImageCode))
            {
                if (data.Ch3Scale <= 0)
                {
                    bug.Append("| Ch3Scale ");
                    isVaildData = false;
                }
            }
            if (!string.IsNullOrEmpty(data.Ch4ImageCode))
            {
                if (data.Ch4Scale <= 0)
                {
                    bug.Append("| Ch4Scale ");
                    isVaildData = false;
                }
            }
        }

        bugReport = bug.ToString();

        return isVaildData;
    }

    bool CheckValue(string value, string type, int cnt, int number, out string valueBugReport)
    {
        string[] vaildImage = Data.GameData.CheckData.VaildImage.Split("|", StringSplitOptions.RemoveEmptyEntries);
        StringBuilder valueName = new StringBuilder("Value_");
        StringBuilder bug = new StringBuilder();

        switch (System.Math.Truncate(number / 8f))
        {
            case 0:
                valueName.Append("Etc");
                break;
            case 1:
                valueName.Append("BG");
                break;
            case 2:
                valueName.Append("Ch1");
                break;
            case 3:
                valueName.Append("Ch2");
                break;
            case 4:
                valueName.Append("Ch3");
                break;
            case 5:
                valueName.Append("Ch4");
                break;
            default:
                valueName.Append("Strange");
                break;
        }
        valueName.Append("_");
        valueName.Append(number % 8);

        if (string.IsNullOrEmpty(value))
        {
            bug.Append($"| {valueName.ToString()}-None ");
            valueBugReport = bug.ToString();
            return false;
        }

        switch (type)
        {
            case "float":
                float dataFloat;
                if(!float.TryParse(value, out dataFloat))
                {
                    bug.Append($"| {valueName.ToString()}-float ");
                    valueBugReport = bug.ToString();
                    return false;
                }
                valueBugReport = "";
                return true;
            case "int":
                int dataInt;
                if(!int.TryParse(value, out dataInt))
                {
                    bug.Append($"| {valueName.ToString()}-int ");
                    valueBugReport = bug.ToString();
                    return false;
                }
                valueBugReport = "";
                return true;
            case "stringName":
                if (!vaildImage.Contains(value))
                {
                    bug.Append($"| {valueName.ToString()}-stringName ");
                    valueBugReport = bug.ToString();
                    return false;
                }
                valueBugReport = "";
                return true;
            case "stringText":
                valueBugReport = "";
                return true;
            case "vector2":
                float x;
                float y;
                bool isVaildVector2;
                bool isReturnFalse = false;
                string[] datas = value.Split(",");
                if(datas.Length != 2)
                {
                    bug.Append($"| {valueName.ToString()}-Vector2 ");
                    isReturnFalse = true;
                }
                else
                {
                    isVaildVector2 = float.TryParse(datas[0].Trim(), out x);
                    if (!isVaildVector2)
                    {
                        bug.Append($"| {valueName.ToString()}-Vector2x ");
                        isReturnFalse = true;
                    }
                    isVaildVector2 = float.TryParse(datas[1].Trim(), out y);
                    if (!isVaildVector2)
                    {
                        bug.Append($"| {valueName.ToString()}-Vector2y ");
                        isReturnFalse = true;
                    }
                }
                valueBugReport = bug.ToString();
                return !isReturnFalse;
            default:
                bug.Append($"| {valueName.ToString()}-Type ");
                valueBugReport = bug.ToString();
                return false;
        }
    }

    bool LastCheckTextData(out string bugReport)
    {
        var textData = Data.GameData.InGameData.TextData;
        bool isVaildData = true;
        StringBuilder bug = new StringBuilder("0 Branch Null : ");

        foreach (var ChKey in textData.Keys)
        {
            foreach(var StoryKey in textData[ChKey].Keys)
            {
                if (!textData[ChKey][StoryKey].ContainsKey(0))
                {
                    bug.Append($"| {ChKey}-{StoryKey}");
                    isVaildData = false;
                }
            }
        }

        bugReport = bug.ToString();
        return isVaildData;
    }

    void MakeBugReport(List<string> bugs)
    {
        string fullpth = "Assets/Log/DataBugReport";

        StreamWriter sw;
        if (!File.Exists(fullpth))
        {
            sw = new StreamWriter(fullpth + ".txt");
        }
        else
        {
            File.Delete(fullpth);
            sw = new StreamWriter(fullpth + ".txt");
        }

        sw.WriteLine(DateTime.Now.ToString("\nyyyy-MM-dd HH:mm:ss\n\n"));
        sw.WriteLine($"�߰ߵ� ���� ���� : {bugs.Count}\n\n");
        sw.WriteLine("----------------------------------------------------\n");
        foreach(string bug in bugs)
        {
            sw.WriteLine(bug);
            sw.WriteLine("\n");
        }

        sw.Flush();
        sw.Close();

        Debug.LogWarning("TextData�� ���װ� �ֽ��ϴ� \"Assets/Log/DataBugReport\"�� Ȯ�����ּ���.");
    }

    List<string> StartBugReport()
    {
        if (_debugLuvData.TextData.Length > 0)
            if (string.IsNullOrEmpty(_debugLuvData.TextData[0].ChName) || _debugLuvData.TextData[0].StoryNumber == -1 || _debugLuvData.TextData[0].BranchNumber == -1)
                Debug.LogWarning($"error_DebugLuvData : TextData�� ù�� ° �࿡ ChName, StoryNumber, BranchNumber �� ��ĭ�� �����մϴ�. ��ĭ�� ���� �ȵ˴ϴ� ä���ּ���.");
            else
            Debug.LogWarning($"error_DebugLuvData : TextData�� �����ϴ�. �����͸� Ȯ�����ּ���");

        return new List<string>();
    }
    */
}