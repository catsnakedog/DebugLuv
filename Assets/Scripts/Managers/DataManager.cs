using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager // �����͸� �����ϴ� Manager�̴�
{
    JsonManager _jsonManager; // Json �����͸� �а�, ���� Manager�̴�

    public void Init()
    {
        GameObject go = GameObject.Find("@Data"); // Data�� ������ �ð������� Ȯ���ϱ� ���ؼ� @Data��� GameObject�� ���� ���� �������ش�
        if (go == null)
        {
            GameData gameData = new GameData();

            _jsonManager = new JsonManager();

            _jsonManager.LoadJsonData<SaveData>("SaveData", out gameData.SaveData); // Json�����͵��� �����´�
            _jsonManager.LoadJsonData<DebugLuvData>("DebugLuvData", out gameData.DebugLuvData);

            go = new GameObject { name = "@Data" };
            UnityEngine.Object.DontDestroyOnLoad(go);
            Data.GameData = gameData; // �����ʹ� Data��� Ŭ������ �����Ѵ�, �����Ϳ� ������ ���� Data.GameData�� �����ϸ� �ȴ�
            go.AddComponent<Data>(); // @Data��� ���� ������Ʈ�� ���� Data ��ũ��Ʈ�� �޾��ش�
        }
    }

    public void Save() // SaveData�� �����Ѵ�
    {
        _jsonManager.SaveJson(Data.GameData.SaveData);
    }
}