using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class DataManager : ManagerSingle<DataManager>, IInit // 데이터를 관리하는 Manager이다
{
    private JsonManager _jsonManager; // Json 데이터를 읽고, 쓰는 Manager이다
    private ParsingManager _parsingManager;
    private SheetManager _sheetManager;

    public DebugLuvData DebugLuvData;
    public SheetData SheetData;

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

    public void ParsingDebugLuvData()
    {
        SheetData = _sheetManager.SetSheetData();
        DebugLuvData = _parsingManager.ParsingSheetData(SheetData);
    }

    public void LoadJsonData(GameData gameData)
    {
        _jsonManager.LoadJsonData<SaveData>("SaveData", out gameData.SaveData); // Json데이터들을 가져온다
        //_jsonManager.LoadJsonData<CheckData>("CheckData", out gameData.CheckData);
    }

    public void Save() // SaveData를 저장한다
    {
        _jsonManager.SaveJson(Data.GameData.SaveData);
    }

    public EpisodeData GetEpisodeData(string Story, int Episode)
    {
        return DebugLuvData.Story[Story].Episode[Episode];
    }

    public SheetData GetSheetData()
    {
        return SheetData;
    }
}