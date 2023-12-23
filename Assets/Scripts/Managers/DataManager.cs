using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager // 데이터를 관리하는 Manager이다
{
    JsonManager _jsonManager; // Json 데이터를 읽고, 쓰는 Manager이다

    public void Init()
    {
        GameObject go = GameObject.Find("@Data"); // Data의 값들을 시각적으로 확인하기 위해서 @Data라는 GameObject를 만들어서 값을 저장해준다
        if (go == null)
        {
            GameData gameData = new GameData();

            _jsonManager = new JsonManager();

            _jsonManager.LoadJsonData<SaveData>("SaveData", out gameData.SaveData); // Json데이터들을 가져온다
            _jsonManager.LoadJsonData<DebugLuvData>("DebugLuvData", out gameData.DebugLuvData);

            go = new GameObject { name = "@Data" };
            UnityEngine.Object.DontDestroyOnLoad(go);
            Data.GameData = gameData; // 데이터는 Data라는 클래스에 보관한다, 데이터에 접근할 때는 Data.GameData로 접근하면 된다
            go.AddComponent<Data>(); // @Data라는 게임 오브젝트를 만들어서 Data 스크립트를 달아준다
        }
    }

    public void Save() // SaveData를 저장한다
    {
        _jsonManager.SaveJson(Data.GameData.SaveData);
    }
}