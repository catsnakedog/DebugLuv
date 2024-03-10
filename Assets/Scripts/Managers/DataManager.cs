using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class DataManager : ManagerSingle<DataManager>, IInit // �����͸� �����ϴ� Manager�̴�
{
    private JsonManager _jsonManager; // Json �����͸� �а�, ���� Manager�̴�
    private ParsingManager _parsingManager;
    private SheetManager _sheetManager;

    public DebugLuvData DebugLuvData;
    public SheetData SheetData;

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

    public void ParsingDebugLuvData()
    {
        SheetData = _sheetManager.SetSheetData();
        DebugLuvData = _parsingManager.ParsingSheetData(SheetData);
    }

    public void LoadJsonData(GameData gameData)
    {
        _jsonManager.LoadJsonData<SaveData>("SaveData", out gameData.SaveData); // Json�����͵��� �����´�
        //_jsonManager.LoadJsonData<CheckData>("CheckData", out gameData.CheckData);
    }

    public void Save() // SaveData�� �����Ѵ�
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