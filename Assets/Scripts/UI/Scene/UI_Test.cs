using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Test : UI_Scene
{
    enum TMP_Dropdowns
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

        Bind<TMP_Dropdown>(typeof(TMP_Dropdowns));
        Bind<TMP_Text>(typeof(TMP_Texts));
        HardBind<Button>(typeof(Buttons));
        Get<TMP_Dropdown>((int)TMP_Dropdowns.ChName).onValueChanged.AddListener(ChNameChange);
        Get<TMP_Dropdown>((int)TMP_Dropdowns.StoryNumber).onValueChanged.AddListener(StoryNumberChange);
        Get<Button>((int)Buttons.Start).gameObject.AddUIEvent(GameStart, Define.UIEvent.Click);

        UISetting();
    }

    void UISetting()
    {
        ChNameUISetting();
        StoryNumberUISetting();
    }

    void ChNameUISetting()
    {
        foreach(string key in Data.GameData.InGameData.TextData.Keys)
        {
            Get<TMP_Dropdown>((int)TMP_Dropdowns.ChName).options.Add(new TMP_Dropdown.OptionData(key));
        }
        Data.GameData.InGameData.ChName = Get<TMP_Dropdown>((int)TMP_Dropdowns.ChName).options[0].text;
        Get<TMP_Dropdown>((int)TMP_Dropdowns.ChName).RefreshShownValue();
    }

    void StoryNumberUISetting()
    {
        foreach(int key in Data.GameData.InGameData.TextData[Data.GameData.InGameData.ChName].Keys)
        {
            Get<TMP_Dropdown>((int)TMP_Dropdowns.StoryNumber).options.Add(new TMP_Dropdown.OptionData(key.ToString()));
        }
        Get<TMP_Dropdown>((int)TMP_Dropdowns.StoryNumber).RefreshShownValue();
    }

    void GameStart(PointerEventData data)
    {
        Managers.Scene.LoadScene(Define.Scene.InGame);
    }

    void ChNameChange(int value)
    {
        Data.GameData.InGameData.ChName = Get<TMP_Dropdown>((int)TMP_Dropdowns.ChName).options[value].text;
        Get<TMP_Dropdown>((int)TMP_Dropdowns.ChName).ClearOptions();
        StoryNumberUISetting();
    }

    void StoryNumberChange(int value)
    {
        Data.GameData.InGameData.StoryNumber = int.Parse(Get<TMP_Dropdown>((int)TMP_Dropdowns.StoryNumber).options[value].text);
    }
}