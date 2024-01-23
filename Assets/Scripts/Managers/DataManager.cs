using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class DataManager // 데이터를 관리하는 Manager이다
{
    JsonManager _jsonManager; // Json 데이터를 읽고, 쓰는 Manager이다
    InGameData _inGameData;
    DebugLuvData _debugLuvData;

    public void Init()
    {
        GameObject go = GameObject.Find("@Data"); // Data의 값들을 시각적으로 확인하기 위해서 @Data라는 GameObject를 만들어서 값을 저장해준다
        if (go == null)
        {
            GameData gameData = new GameData();

            _jsonManager = new JsonManager();

            _jsonManager.LoadJsonData<SaveData>("SaveData", out gameData.SaveData); // Json데이터들을 가져온다
            _jsonManager.LoadJsonData<DebugLuvData>("DebugLuvData", out gameData.DebugLuvData);
            _jsonManager.LoadJsonData<CheckData>("CheckData", out gameData.CheckData);

            go = new GameObject { name = "@Data" };
            go.AddComponent<Data>(); // @Data라는 게임 오브젝트를 만들어서 Data 스크립트를 달아준다
            UnityEngine.Object.DontDestroyOnLoad(go);
            Data.GameData = gameData; // 데이터는 Data라는 클래스에 보관한다, 데이터에 접근할 때는 Data.GameData로 접근하면 된다
            _inGameData = Data.GameData.InGameData;
            _debugLuvData = Data.GameData.DebugLuvData;

            TextDataSetting();
        }
    }

    public void LoadJsonData()
    {

    }

    public void LoadSaveData()
    {

    }

    public void Save() // SaveData를 저장한다
    {
        _jsonManager.SaveJson(Data.GameData.SaveData);
    }

    public void SortingDebugLuvData(DebugLuvData data)
    {

    }

    public void TextDataSetting() // 해당 ChName, StoryNumber의 데이터를 가공한다
    {
        TextDataClear();

        string chName = "None";
        int storyNumber = -1;
        int branchNumber = -1;

#if UNITY_EDITOR
        if (_debugLuvData.TextData.Length > 0)
        {
            if (string.IsNullOrEmpty(_debugLuvData.TextData[0].ChName) || _debugLuvData.TextData[0].StoryNumber == -1 || _debugLuvData.TextData[0].BranchNumber == -1)
            {
                Debug.LogWarning($"error_DebugLuvData : TextData의 첫번 째 행에 ChName, StoryNumber, BranchNumber 중 빈칸이 존재합니다. 빈칸이 오면 안됩니다 채워주세요.");
                return;
            }
        }
        else
        {
            Debug.LogWarning($"error_DebugLuvData : TextData가 없습니다. 데이터를 확인해주세요");
            return;
        }

        List<string> bugReport = new();
#endif

        int cnt = 0;

        foreach (TextData textData in _debugLuvData.TextData)
        {
            bool isExtraTask = false;

            if (string.IsNullOrEmpty(textData.ChName) || textData.StoryNumber == -1 || textData.BranchNumber == -1) // 한개라도 비었으면 ExtraTask로 판별
                isExtraTask = true;
            else
            {
                chName = textData.ChName;
                storyNumber = textData.StoryNumber;
                branchNumber = textData.BranchNumber;
            }

#if UNITY_EDITOR
            string bugFirst;
            if (!FirstCheckTextData(textData, cnt, isExtraTask, out bugFirst))
            {
                Debug.LogWarning($"error_DebugLuvData : {cnt + 7}번 줄 데이터가 이상합니다. 로그를 확인해주세요.");
                bugReport.Add(bugFirst);
            }
#endif

            if (!_inGameData.TextData.ContainsKey(chName))
                _inGameData.TextData[chName] = new();
            if (!_inGameData.TextData[chName].ContainsKey(storyNumber))
                _inGameData.TextData[chName][storyNumber] = new();
            if (!_inGameData.TextData[chName][storyNumber].ContainsKey(branchNumber))
                _inGameData.TextData[chName][storyNumber][branchNumber] = new();

            var target = _inGameData.TextData[chName][storyNumber][branchNumber];

            if (isExtraTask)
            {
#if UNITY_EDITOR
                if(target.Count == 0)
                {
                    Debug.LogWarning($"error_DebugLuvData : {chName}의 {storyNumber}번 스토리의 {branchNumber}브랜치의 첫 데이터가 ExtraTask입니다. 데이터를 확인해주세요.");
                    bugReport.Add($"{chName}-{storyNumber}-{branchNumber} : First Paragraph is ExtraTask");
                    return;
                }
#endif
                target[^1].Add(textData);
            }
            else
            {
                target.Add(new List<TextData> { textData });
            }

            cnt++;
        }
#if UNITY_EDITOR
        string bugLast;
        if(!LastCheckTextData(out bugLast))
        {
            bugReport.Add(bugLast);
        }

        MakeBugReport(bugReport);
        Debug.LogWarning($"error_DebugLuvData : 이상한 데이터가 있습니다. 로그를 확인해주세요. Assets/Log/DataBugReport.txt 파일을 확인해주세요.");
#endif
    }

    public void TextDataClear()
    {
        Data.GameData.InGameData.TextData = new();
    }

    bool FirstCheckTextData(TextData data, int cnt, bool isExtraTask, out string bugReport) // 이상한 데이터가 있는지 검사
    {
        var checkData = Data.GameData.CheckData;
        bool isVaildData = true;
        StringBuilder bug = new StringBuilder($"{cnt + 7}번 줄 : ");

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
            // Story 체크
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
                Debug.LogWarning($"error_DebugLuvData : {cnt + 7}번 줄 데이터의 Story가 StoryData에 존재하지 않습니다. 데이터를 확인해주세요.");
                bug.Append("| Story ");
                isVaildData = false;
            }

            // ChName 체크
            if (!vaildChName.Contains(data.ChName))
            {
                Debug.LogWarning($"error_DebugLuvData : {cnt + 7}번 줄 데이터의 ChName이 이상합니다. 데이터를 확인해주세요.");
                bug.Append("| ChName ");
                isVaildData = false;
            }
        }

        // TaskOrder 체크
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
                Debug.LogWarning($"error_DebugLuvData : {cnt + 7}번 줄 데이터의 TaskOrder가 이상합니다. 데이터를 확인해주세요.");
                bug.Append("| TaskOrder ");
                isVaildData = false;
            }
        }

        // Task 체크
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
                Debug.LogWarning($"error_DebugLuvData : {cnt + 7}번 줄 데이터의 {key}가 이상합니다. 데이터를 확인해주세요.");
                bug.Append($"| {key} ");
                isVaildData = false;
            }
        }

        // value 체크
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
            // BGImage 체크
            if (!vaildImage.Contains(data.BGImage))
            {
                Debug.LogWarning($"error_DebugLuvData : {cnt + 7}번 줄 데이터의 BGImage가 이상합니다. 데이터를 확인해주세요.");
                bug.Append("| BGImage ");
                isVaildData = false;
            }

            // ChoiceNumber 체크
            if (data.ChoiceNumber != -1 && !vaildChoiceNumber.Contains(data.ChoiceNumber.ToString()))
            {
                Debug.LogWarning($"error_DebugLuvData : {cnt + 7}번 줄 데이터의 ChoiceNumber가 이상합니다. 데이터를 확인해주세요.");
                bug.Append("| ChoiceNumber ");
                isVaildData = false;
            }

            // ImageCode 체크
            if (!string.IsNullOrEmpty(data.Ch1ImageCode) && data.Ch1ImageCode.Split("-").Length != 6)
            {
                Debug.LogWarning($"error_DebugLuvData : {cnt + 7}번 줄 데이터의 Ch1ImageCode가 이상합니다. 데이터를 확인해주세요.");
                bug.Append("| Ch1ImageCode ");
                isVaildData = false;
            }
            if (!string.IsNullOrEmpty(data.Ch2ImageCode) && data.Ch2ImageCode.Split("-").Length != 6)
            {
                Debug.LogWarning($"error_DebugLuvData : {cnt + 7}번 줄 데이터의 Ch2ImageCode가 이상합니다. 데이터를 확인해주세요.");
                bug.Append("| Ch2ImageCode ");
                isVaildData = false;
            }
            if (!string.IsNullOrEmpty(data.Ch3ImageCode) && data.Ch3ImageCode.Split("-").Length != 6)
            {
                Debug.LogWarning($"error_DebugLuvData : {cnt + 7}번 줄 데이터의 Ch3ImageCode가 이상합니다. 데이터를 확인해주세요.");
                bug.Append("| Ch3ImageCode ");
                isVaildData = false;
            }
            if (!string.IsNullOrEmpty(data.Ch4ImageCode) && data.Ch4ImageCode.Split("-").Length != 6)
            {
                Debug.LogWarning($"error_DebugLuvData : {cnt + 7}번 줄 데이터의 Ch4ImageCode가 이상합니다. 데이터를 확인해주세요.");
                bug.Append("| Ch4ImageCode ");
                isVaildData = false;
            }

            // ChPos 체크
            float x;
            float y;
            bool isVaildVector2;

            if (!string.IsNullOrEmpty(data.Ch1ImageCode))
            {
                string[] datas = data.Ch1Pos.Split(",");
                if (datas.Length != 2)
                {
                    Debug.LogWarning($"error_DebugLuvData : {cnt + 7}번 줄 데이터의 Ch1Pos 값이 이상합니다. 데이터를 확인해주세요.");
                    bug.Append("| Ch1Pos ");
                    isVaildData = false;
                }
                else
                {
                    isVaildVector2 = float.TryParse(datas[0].Trim(), out x);
                    if (!isVaildVector2)
                    {
                        Debug.LogWarning($"error_DebugLuvData : {cnt + 7}번 줄 데이터의 Ch1Pos x값이 이상합니다. 데이터를 확인해주세요.");
                        bug.Append("| Ch1Pos ");
                        isVaildData = false;
                    }
                    isVaildVector2 = float.TryParse(datas[1].Trim(), out y);
                    if (!isVaildVector2)
                    {
                        Debug.LogWarning($"error_DebugLuvData : {cnt + 7}번 줄 데이터의 Ch1Pos y값이 이상합니다. 데이터를 확인해주세요.");
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
                    Debug.LogWarning($"error_DebugLuvData : {cnt + 7}번 줄 데이터의 Ch2Pos 값이 이상합니다. 데이터를 확인해주세요.");
                    bug.Append("| Ch2Pos ");
                    isVaildData = false;
                }
                else
                {
                    isVaildVector2 = float.TryParse(datas[0].Trim(), out x);
                    if (!isVaildVector2)
                    {
                        Debug.LogWarning($"error_DebugLuvData : {cnt + 7}번 줄 데이터의 Ch2Pos x값이 이상합니다. 데이터를 확인해주세요.");
                        bug.Append("| Ch2Pos ");
                        isVaildData = false;
                    }
                    isVaildVector2 = float.TryParse(datas[1].Trim(), out y);
                    if (!isVaildVector2)
                    {
                        Debug.LogWarning($"error_DebugLuvData : {cnt + 7}번 줄 데이터의 Ch2Pos y값이 이상합니다. 데이터를 확인해주세요.");
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
                    Debug.LogWarning($"error_DebugLuvData : {cnt + 7}번 줄 데이터의 Ch3Pos 값이 이상합니다. 데이터를 확인해주세요.");
                    bug.Append("| Ch3Pos ");
                    isVaildData = false;
                }
                else
                {
                    isVaildVector2 = float.TryParse(datas[0].Trim(), out x);
                    if (!isVaildVector2)
                    {
                        Debug.LogWarning($"error_DebugLuvData : {cnt + 7}번 줄 데이터의 Ch3Pos x값이 이상합니다. 데이터를 확인해주세요.");
                        bug.Append("| Ch3Pos ");
                        isVaildData = false;
                    }
                    isVaildVector2 = float.TryParse(datas[1].Trim(), out y);
                    if (!isVaildVector2)
                    {
                        Debug.LogWarning($"error_DebugLuvData : {cnt + 7}번 줄 데이터의 Ch3Pos y값이 이상합니다. 데이터를 확인해주세요.");
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
                    Debug.LogWarning($"error_DebugLuvData : {cnt + 7}번 줄 데이터의 Ch4Pos 값이 이상합니다. 데이터를 확인해주세요.");
                    bug.Append("| Ch4Pos ");
                    isVaildData = false;
                }
                else
                {
                    isVaildVector2 = float.TryParse(datas[0].Trim(), out x);
                    if (!isVaildVector2)
                    {
                        Debug.LogWarning($"error_DebugLuvData : {cnt + 7}번 줄 데이터의 Ch4Pos x값이 이상합니다. 데이터를 확인해주세요.");
                        bug.Append("| Ch4Pos ");
                        isVaildData = false;
                    }
                    isVaildVector2 = float.TryParse(datas[1].Trim(), out y);
                    if (!isVaildVector2)
                    {
                        Debug.LogWarning($"error_DebugLuvData : {cnt + 7}번 줄 데이터의 Ch4Pos y값이 이상합니다. 데이터를 확인해주세요.");
                        bug.Append("| Ch4Pos ");
                        isVaildData = false;
                    }
                }
            }

            // ChScale 체크
            if (!string.IsNullOrEmpty(data.Ch1ImageCode))
            {
                if (data.Ch1Scale <= 0)
                {
                    Debug.LogWarning($"error_DebugLuvData : {cnt + 7}번 줄 데이터의 Ch1Scale값은 0보다 작거나 같을 수 없습니다. 데이터를 확인해주세요.");
                    bug.Append("| Ch1Scale ");
                    isVaildData = false;
                }
            }
            if (!string.IsNullOrEmpty(data.Ch2ImageCode))
            {
                if (data.Ch2Scale <= 0)
                {
                    Debug.LogWarning($"error_DebugLuvData : {cnt + 7}번 줄 데이터의 Ch2Scale값은 0보다 작거나 같을 수 없습니다. 데이터를 확인해주세요.");
                    bug.Append("| Ch2Scale ");
                    isVaildData = false;
                }
            }
            if (!string.IsNullOrEmpty(data.Ch3ImageCode))
            {
                if (data.Ch3Scale <= 0)
                {
                    Debug.LogWarning($"error_DebugLuvData : {cnt + 7}번 줄 데이터의 Ch3Scale값은 0보다 작거나 같을 수 없습니다. 데이터를 확인해주세요.");
                    bug.Append("| Ch3Scale ");
                    isVaildData = false;
                }
            }
            if (!string.IsNullOrEmpty(data.Ch4ImageCode))
            {
                if (data.Ch4Scale <= 0)
                {
                    Debug.LogWarning($"error_DebugLuvData : {cnt + 7}번 줄 데이터의 Ch4Scale값은 0보다 작거나 같을 수 없습니다. 데이터를 확인해주세요.");
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
            Debug.LogWarning($"error_DebugLuvData : {cnt + 7}번 줄 데이터의 {valueName.ToString()}의 값이 없습니다. 데이터를 확인해주세요.");
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
                    Debug.LogWarning($"error_DebugLuvData : {cnt + 7}번 줄 데이터의 {valueName.ToString()}가 이상합니다. 데이터를 확인해주세요.");
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
                    Debug.LogWarning($"error_DebugLuvData : {cnt + 7}번 줄 데이터의 {valueName.ToString()}가 이상합니다. 데이터를 확인해주세요.");
                    bug.Append($"| {valueName.ToString()}-int ");
                    valueBugReport = bug.ToString();
                    return false;
                }
                valueBugReport = "";
                return true;
            case "stringName":
                if (!vaildImage.Contains(value))
                {
                    Debug.LogWarning($"error_DebugLuvData : {cnt + 7}번 줄 데이터의 {valueName.ToString()}가 이상합니다. 데이터를 확인해주세요.");
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
                    Debug.LogWarning($"error_DebugLuvData : {cnt + 7}번 줄 데이터의 {valueName.ToString()}의 Pos값이 이상합니다. 데이터를 확인해주세요.");
                    bug.Append($"| {valueName.ToString()}-Vector2 ");
                    isReturnFalse = true;
                }
                else
                {
                    isVaildVector2 = float.TryParse(datas[0].Trim(), out x);
                    if (!isVaildVector2)
                    {
                        Debug.LogWarning($"error_DebugLuvData : {cnt + 7}번 줄 데이터의 {valueName.ToString()}의 x값이 이상합니다. 데이터를 확인해주세요.");
                        bug.Append($"| {valueName.ToString()}-Vector2x ");
                        isReturnFalse = true;
                    }
                    isVaildVector2 = float.TryParse(datas[1].Trim(), out y);
                    if (!isVaildVector2)
                    {
                        Debug.LogWarning($"error_DebugLuvData : {cnt + 7}번 줄 데이터의 {valueName.ToString()}의 y값이 이상합니다. 데이터를 확인해주세요.");
                        bug.Append($"| {valueName.ToString()}-Vector2y ");
                        isReturnFalse = true;
                    }
                }
                valueBugReport = bug.ToString();
                return !isReturnFalse;
            default:
                Debug.LogWarning($"error_DebugLuvData : {cnt + 7}번 줄 데이터의 {valueName.ToString()}의 타입이 안적혀있습니다. 데이터를 확인해주세요.");
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
                    Debug.LogWarning($"error_DebugLuvData : {ChKey} 캐릭터의 {StoryKey}번 스토리 데이터에 0번 브랜치가 없습니다. 모든 스토리는 0번 브랜치를 가져야합니다. 데이터를 확인해주세요.");
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
        sw.WriteLine($"발견된 버그 갯수 : {bugs.Count}\n\n");
        sw.WriteLine("----------------------------------------------------\n");
        foreach(string bug in bugs)
        {
            sw.WriteLine(bug);
            sw.WriteLine("\n");
        }

        sw.Flush();
        sw.Close();
    }
}