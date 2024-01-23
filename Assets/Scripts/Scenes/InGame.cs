using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGame : BaseScene
{
    TaskController _taskController;
    int _textDataIdx;
    int _textDataBranch;
    InGameData _inGameData;

    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.InGame;
        Managers.UI_Manager.ShowSceneUI<UI_InGame>();
        _taskController = gameObject.GetOrAddComponent<TaskController>();
        _inGameData = Data.GameData.InGameData;

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
            _taskController.TaskSetting(data);
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
        var storage = _inGameData.TextData[_inGameData.ChName][_inGameData.StoryNumber][_textDataBranch];

        if(storage.Count > _textDataIdx)
        {
            data = storage[_textDataIdx];
            return true;
        }
        else
        {
            data = storage[_textDataIdx];
            return false;
        }
    }

    void ChangeBranch(int number)
    {
        _textDataBranch = number;
    }
}
