using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGame : BaseScene
{
    TaskController _taskController;
    int _textDataIdx;
    int _textDataBranch;


    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.InGame;
        Managers.UI_Manager.ShowSceneUI<UI_InGame>();
        _taskController = gameObject.AddComponent<TaskController>();

        StartCoroutine(InGameStart());
    }

    public override void Clear()
    {
        base.Init();
    }

    IEnumerator InGameStart()
    {
        (SceneUI as UI_InGame).Init();
        yield return StartCoroutine((SceneUI as UI_InGame).StartGame());
        _textDataIdx = 0;
        _textDataBranch = 0;
        ParagraphStart();
    }

    public void ParagraphStart()
    {
        if(ParagraphDataSetting(out List<TextData> data))
        {
            StartCoroutine(_taskController.TaskSetting(data));
        }
        else
        {
            Debug.Log("∞‘¿” ≥°");
            Managers.Scene.LoadScene(Define.Scene.Main);
        }
    }

    public void ParagraphEnd()
    {
        (SceneUI as UI_InGame).NextParagraphOn();
    }

    bool ParagraphDataSetting(out List<TextData> data)
    {
        data = new List<TextData>();
        if(Data.GameData.InGameData.TextData[_textDataBranch].Count > _textDataIdx)
        {
            data.Add(Data.GameData.InGameData.TextData[_textDataBranch][_textDataIdx]);
            _textDataIdx++;
            for(int i = _textDataIdx; i < Data.GameData.InGameData.TextData[_textDataBranch].Count; i++)
            {
                if (string.IsNullOrEmpty(Data.GameData.InGameData.TextData[_textDataBranch][_textDataIdx].ChName))
                {
                    data.Add(Data.GameData.InGameData.TextData[_textDataBranch][_textDataIdx]);
                    _textDataIdx++;
                }
                else
                    break;
            }

            return true;
        }
        else
        {
            return false;
        }
    }

    void ChangeBranch(int number)
    {
        _textDataBranch = number;
    }
}
