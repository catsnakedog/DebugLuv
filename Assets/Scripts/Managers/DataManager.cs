//-------------------------------------------------------------------------------------------------
// @file	DataManager.cs
//
// @brief	������ ������ ���� �Ŵ���
//
// @date	2024-03-14
//
// Copyright 2024 Team One-eyed Games. All Rights Reserved.
//-------------------------------------------------------------------------------------------------


using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


/// <summary> ������ ������ ���� �Ŵ��� </summary>
public class DataManager : ManagerSingle<DataManager>, IInit // �����͸� �����ϴ� Manager�̴�
{

    /// <summary> Json �����͸� �а�, ���� Manager�̴� </summary>
    private JsonManager _jsonManager; 
    /// <summary> �Ľ� �Ŵ��� </summary>
    private ParsingManager _parsingManager;
    /// <summary> ���� ��Ʈ �Ŵ��� </summary>
    private SheetManager _sheetManager;
    /// <summary> ���� ������ </summary>
    public DebugLuvData DebugLuvData;
    /// <summary> ���� ��Ʈ ������ </summary>
    public SheetData SheetData;

    /// <summary>
    /// Initialize 
    /// </summary>
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
            _sheetManager = new SheetManager();

            LoadJsonData(gameData);

            data = new GameObject { name = "@Data" };
            data.transform.SetParent(root.transform);
            data.AddComponent<Data>(); // @Data��� ���� ������Ʈ�� ���� Data ��ũ��Ʈ�� �޾��ش�
            Data.GameData = gameData; // �����ʹ� Data��� Ŭ������ �����Ѵ�, �����Ϳ� ������ ���� Data.GameData�� �����ϸ� �ȴ�
        }
    }

    /// <summary>
    /// ���� �����͸� �Ľ��Ѵ�.
    /// </summary>
    public void ParsingDebugLuvData()
    {
        SheetData = _sheetManager.SetSheetData();
        DebugLuvData = _parsingManager.ParsingSheetData(SheetData);
    }

    /// <summary>
    /// Json Ÿ�� �����͸� �Ľ��Ѵ�.
    /// </summary>
    /// <param name="gameData"></param>
    public void LoadJsonData(GameData gameData)
    {
        _jsonManager.LoadJsonData<SaveData>("SaveData", out gameData.SaveData); // Json�����͵��� �����´�
    }

    /// <summary>
    /// ������� �����͸� �����Ѵ�.
    /// </summary>
    public void Save() // SaveData�� �����Ѵ�
    {
        _jsonManager.SaveJson(Data.GameData.SaveData);
    }

    /// <summary>
    /// ���Ǽҵ� �����͸� get�Ѵ�.
    /// </summary>
    /// <param name="Story"></param>
    /// <param name="Episode"></param>
    /// <returns></returns>
    public EpisodeData GetEpisodeData(string Story, int Episode)
    {
        return DebugLuvData.Story[Story].Episode[Episode];
    }

    /// <summary>
    /// SheetData�� get�Ѵ�.
    /// </summary>
    /// <returns></returns>
    public SheetData GetSheetData()
    {
        return SheetData;
    }
}