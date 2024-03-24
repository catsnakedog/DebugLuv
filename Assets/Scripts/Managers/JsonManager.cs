//-------------------------------------------------------------------------------------------------
// @file	JsonManager.cs
//
// @brief	Json ������ ����� ���� �Ŵ���
//
// @date	2024-03-14
//
// Copyright 2024 Team One-eyed Games. All Rights Reserved.
//-------------------------------------------------------------------------------------------------


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

/// <summary> Json ������ ����� ���� �Ŵ��� </summary>
public class JsonManager // Json�����͸� �а� ���� Manager�̴�
{
    /// <summary>
    /// Json Type �����͸� Save ( �ȵ���̵�, Window ���� )
    /// </summary>
    /// <param name="saveData"> ���� ������ </param>
    public void SaveJson(SaveData saveData) // �����͸� ���� �Լ�
    {
        StringBuilder sb = new StringBuilder(Application.dataPath);

        string jsonText;
        string pilePath = sb.Append("/Save").ToString();
        string savePath = sb.Append("/SaveData.json").ToString();
#if UNITY_EDITOR_WIN

#endif
#if UNITY_ANDROID // �ȵ���̵��� ��� ��θ� �������ش�
        sb.Clear();
        sb.Append(Application.persistentDataPath);
        pilePath = sb.Append("/Save").ToString();
        savePath = sb.Append("/SaveData.json").ToString();
#endif
        if (!Directory.Exists(pilePath)) // ������ �����ϴ��� Ȯ���Ѵ�
        {
            Directory.CreateDirectory(pilePath);
        }
        jsonText = JsonUtility.ToJson(saveData, true);
        FileStream fileStream = new FileStream(savePath, FileMode.Create); // Resource ������ ������ �ۼ����� �������� �ʱ� ������ ���� �ۼ��Ѵ�
        byte[] bytes = Encoding.UTF8.GetBytes(jsonText);
        fileStream.Write(bytes, 0, bytes.Length);
        fileStream.Close();
    }

    public void LoadJsonData<T>(string dataName, out T data) where T: new() // �����͸� �д� �Լ�
                                                                            // out���� data�� �����ϴ� ������ LoadJsonData �Լ� �󿡼��� ��𿡴ٰ�
                                                                            // �����͸� �����ؾ��ϴ��� �𸣱� �����̴�
    {
        if (dataName == "SaveData") // SaveData ���� ��� �ٸ� ��ο� ����Ǳ� ������ ���� �о�;��Ѵ�
        {
            StringBuilder sb = new StringBuilder(Application.dataPath);
            string loadPath = sb.Append($"/Save/{dataName}" + ".json").ToString();
#if UNITY_EDITOR_WIN
#endif
#if UNITY_ANDROID
            sb.Clear();
            sb.Append(Application.persistentDataPath);
            loadPath = sb.Append($"/Save/{dataName}" + ".json").ToString();
#endif
            if (File.Exists(loadPath))
            {
                FileStream stream = new FileStream(loadPath, FileMode.Open);
                byte[] bytes = new byte[stream.Length];
                stream.Read(bytes, 0, bytes.Length);
                stream.Close();
                string jsonData = Encoding.UTF8.GetString(bytes);
                data = JsonUtility.FromJson<T>(jsonData);
            }
            else
            {
                data = new T();
                Debug.LogWarning($"error_JsonManager : {dataName} Json ������ �������� �ʽ��ϴ�.");
            }
        }
        else
        {
            TextAsset jsonData = (TextAsset)Resources.Load($"Data/{dataName}", typeof(TextAsset)); // Json�����ʹ� Resources/Data �ȿ� ������ִ�

            if (jsonData.text != null)
            {
                data = JsonUtility.FromJson<T>(jsonData.ToString());
            }
            else
            {
                data = new T(); // ������ ������ �⺻ �����ڷ� �����Ѵ�
                Debug.LogWarning($"error_JsonManager : {dataName} Json ������ �������� �ʽ��ϴ�.");
            }
        }
    }
}
