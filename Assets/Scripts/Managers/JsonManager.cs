//-------------------------------------------------------------------------------------------------
// @file	JsonManager.cs
//
// @brief	Json 데이터 사용을 위한 매니저
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

/// <summary> Json 데이터 사용을 위한 매니저 </summary>
public class JsonManager // Json데이터를 읽고 쓰는 Manager이다
{
    /// <summary>
    /// Json Type 데이터를 Save ( 안드로이드, Window 대응 )
    /// </summary>
    /// <param name="saveData"> 저장 데이터 </param>
    public void SaveJson(SaveData saveData) // 데이터를 쓰는 함수
    {
        StringBuilder sb = new StringBuilder(Application.dataPath);

        string jsonText;
        string pilePath = sb.Append("/Save").ToString();
        string savePath = sb.Append("/SaveData.json").ToString();
#if UNITY_EDITOR_WIN

#endif
#if UNITY_ANDROID // 안드로이드인 경우 경로를 수정해준다
        sb.Clear();
        sb.Append(Application.persistentDataPath);
        pilePath = sb.Append("/Save").ToString();
        savePath = sb.Append("/SaveData.json").ToString();
#endif
        if (!Directory.Exists(pilePath)) // 파일이 존재하는지 확인한다
        {
            Directory.CreateDirectory(pilePath);
        }
        jsonText = JsonUtility.ToJson(saveData, true);
        FileStream fileStream = new FileStream(savePath, FileMode.Create); // Resource 파일은 동적인 작성에는 적합하지 않기 때문에 따로 작성한다
        byte[] bytes = Encoding.UTF8.GetBytes(jsonText);
        fileStream.Write(bytes, 0, bytes.Length);
        fileStream.Close();
    }

    public void LoadJsonData<T>(string dataName, out T data) where T: new() // 데이터를 읽는 함수
                                                                            // out으로 data를 저장하는 이유는 LoadJsonData 함수 상에서는 어디에다가
                                                                            // 데이터를 저장해야하는지 모르기 때문이다
    {
        if (dataName == "SaveData") // SaveData 같은 경우 다른 경로에 저장되기 때문에 따로 읽어와야한다
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
                Debug.LogWarning($"error_JsonManager : {dataName} Json 파일이 존재하지 않습니다.");
            }
        }
        else
        {
            TextAsset jsonData = (TextAsset)Resources.Load($"Data/{dataName}", typeof(TextAsset)); // Json데이터는 Resources/Data 안에 저장돼있다

            if (jsonData.text != null)
            {
                data = JsonUtility.FromJson<T>(jsonData.ToString());
            }
            else
            {
                data = new T(); // 파일이 없으면 기본 생성자로 세팅한다
                Debug.LogWarning($"error_JsonManager : {dataName} Json 파일이 존재하지 않습니다.");
            }
        }
    }
}
