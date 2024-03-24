//-------------------------------------------------------------------------------------------------
// @file	DataManager.cs
//
// @brief	데이터 관리를 위한 매니저
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


/// <summary> 데이터 관리를 위한 매니저 </summary>
public class DataManager : ManagerSingle<DataManager>, IInit // 데이터를 관리하는 Manager이다
{

    /// <summary> Json 데이터를 읽고, 쓰는 Manager이다 </summary>
    private JsonManager _jsonManager; 
    /// <summary> 파싱 매니저 </summary>
    private ParsingManager _parsingManager;
    /// <summary> 구글 시트 매니저 </summary>
    private SheetManager _sheetManager;
    /// <summary> 게임 데이터 </summary>
    public DebugLuvData DebugLuvData;
    /// <summary> 구글 시트 데이터 </summary>
    public SheetData SheetData;

    /// <summary>
    /// Initialize 
    /// </summary>
    public void Init()
    {
        GameObject data = GameObject.Find("@Data"); // Data의 값들을 시각적으로 확인하기 위해서 @Data라는 GameObject를 만들어서 값을 저장해준다
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
            data.AddComponent<Data>(); // @Data라는 게임 오브젝트를 만들어서 Data 스크립트를 달아준다
            Data.GameData = gameData; // 데이터는 Data라는 클래스에 보관한다, 데이터에 접근할 때는 Data.GameData로 접근하면 된다
        }
    }

    /// <summary>
    /// 게임 데이터를 파싱한다.
    /// </summary>
    public void ParsingDebugLuvData()
    {
        SheetData = _sheetManager.SetSheetData();
        DebugLuvData = _parsingManager.ParsingSheetData(SheetData);
    }

    /// <summary>
    /// Json 타입 데이터를 파싱한다.
    /// </summary>
    /// <param name="gameData"></param>
    public void LoadJsonData(GameData gameData)
    {
        _jsonManager.LoadJsonData<SaveData>("SaveData", out gameData.SaveData); // Json데이터들을 가져온다
    }

    /// <summary>
    /// 현재까지 데이터를 저장한다.
    /// </summary>
    public void Save() // SaveData를 저장한다
    {
        _jsonManager.SaveJson(Data.GameData.SaveData);
    }

    /// <summary>
    /// 에피소드 데이터를 get한다.
    /// </summary>
    /// <param name="Story"></param>
    /// <param name="Episode"></param>
    /// <returns></returns>
    public EpisodeData GetEpisodeData(string Story, int Episode)
    {
        return DebugLuvData.Story[Story].Episode[Episode];
    }

    /// <summary>
    /// SheetData를 get한다.
    /// </summary>
    /// <returns></returns>
    public SheetData GetSheetData()
    {
        return SheetData;
    }
}