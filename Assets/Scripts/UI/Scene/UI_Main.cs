using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Main : UI_Scene
{
    enum TMP_InputFields
    {
        ChName,
        StoryNumber
    }

    enum TMP_Texts
    {
        WarningText
    }

    enum Buttons
    {
        Start,
    }

    bool _isChInput = false;
    bool _isStoryNumberInput = false;

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        Bind<TMP_InputField>(typeof(TMP_InputFields));
        Bind<TMP_Text>(typeof(TMP_Texts));
        HardBind<Button>(typeof(Buttons));

        Get<TMP_InputField>((int)TMP_InputFields.ChName).onEndEdit.AddListener(ChNameSetting);
        Get<TMP_InputField>((int)TMP_InputFields.StoryNumber).onEndEdit.AddListener(StoryNumberSetting);
        Get<Button>((int)Buttons.Start).gameObject.AddUIEvent(GameStart, Define.UIEvent.Click);
    }

    void GameStart(PointerEventData data)
    {
        Managers.Scene.LoadScene(Define.Scene.InGame);
    }

    void ChNameSetting(string value)
    {
        Data.GameData.InGameData.ChName = value;
        _isChInput = true;

        GameStartCheck();
    }

    void StoryNumberSetting(string value)
    {
        if (int.TryParse(value, out int number))
        {
            Data.GameData.InGameData.StoryNumber = number;
            _isStoryNumberInput = true;
        }
        else
        {
            Get<TMP_Text>((int)TMP_Texts.WarningText).text = "StoryNumber isn't natural number";
        }

        GameStartCheck();
    }

    void GameStartCheck()
    {
        if(_isChInput && _isStoryNumberInput)
        {
            Get<TMP_Text>((int)TMP_Texts.WarningText).text = $"Press Button to Start Game\n{Data.GameData.InGameData.ChName}, {Data.GameData.InGameData.StoryNumber}";
            Get<Button>((int)Buttons.Start).gameObject.SetActive(true);
        }
        else
            Get<Button>((int)Buttons.Start).gameObject.SetActive(false);
    }
}
