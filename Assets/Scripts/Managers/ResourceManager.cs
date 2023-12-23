using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class ResourceManager // Resource를 관리하는 Manager이다
{
    public T Load<T>(string path) where T : Object // Resource.Load와 차이점은 없지만 무언가 추가 될 수도 있으니 추가해둔다
    {
        return Resources.Load<T>(path);
    }

    public Sprite LoadSpirte(string name)
    {
        Sprite sprite = Resources.Load<Sprite>($"Sprites/{name}");
        if(sprite == null)
        {
            sprite = Resources.Load<Sprite>("Sprites/BugJava");
        }

        return sprite;
    }

    public GameObject Instantiate(string path, Transform parent = null) // 인스턴트 하는 모든 GameObject들은 Prefabs 파일 안에 있으니 따로 경로를 쓰는 과정을 생략해준다
    {
        GameObject original = Load<GameObject>($"Prefabs/{path}");
        if(original == null)
        {
            Debug.LogWarning($"Failed to load prefab : {path}");
        }

        GameObject go = Object.Instantiate(original, parent);
        go.name = original.name;

        return go;
    }

    public void Destroy(GameObject go) // GameObject.Destroy와 차이점은 없지만 무언가 추가 될 수도 있으니 추가해둔다
    {
        if(go == null)
        return;

        Object.Destroy(go);
    }

    public void TextDataSetting() // 해당 ChName, StoryNumber의 데이터를 가공한다
    {
        TextDataClear();

        Dictionary<int, List<TextData>> data = new Dictionary<int, List<TextData>>();

        foreach (TextData textData in Data.GameData.DebugLuvData.TextData)
        {
            if (textData.ChName == Data.GameData.InGameData.ChName && textData.StoryNumber == Data.GameData.InGameData.StoryNumber)
            {
                if (data.ContainsKey(textData.BranchNumber))
                    data[textData.BranchNumber].Add(textData);
                else
                {
                    data[textData.BranchNumber] = new List<TextData>();
                    data[textData.BranchNumber].Add(textData);
                }

                if (!Data.GameData.InGameData.InGameSprite.ContainsKey(textData.BGImage) && !string.IsNullOrEmpty(textData.BGImage))
                    Data.GameData.InGameData.InGameSprite.Add(textData.BGImage, LoadSpirte(textData.BGImage));
                if (!Data.GameData.InGameData.InGameSprite.ContainsKey(textData.Ch1Image) && !string.IsNullOrEmpty(textData.Ch1Image))
                    Data.GameData.InGameData.InGameSprite.Add(textData.Ch1Image, LoadSpirte(textData.Ch1Image));
                if (!Data.GameData.InGameData.InGameSprite.ContainsKey(textData.Ch2Image) && !string.IsNullOrEmpty(textData.Ch2Image))
                    Data.GameData.InGameData.InGameSprite.Add(textData.Ch2Image, LoadSpirte(textData.Ch2Image));
                if (!Data.GameData.InGameData.InGameSprite.ContainsKey(textData.Ch3Image) && !string.IsNullOrEmpty(textData.Ch3Image))
                    Data.GameData.InGameData.InGameSprite.Add(textData.Ch3Image, LoadSpirte(textData.Ch3Image));
                if (!Data.GameData.InGameData.InGameSprite.ContainsKey(textData.Ch4Image) && !string.IsNullOrEmpty(textData.Ch4Image))
                    Data.GameData.InGameData.InGameSprite.Add(textData.Ch4Image, LoadSpirte(textData.Ch4Image));
            }
        }

        if (data.Count == 0) // 해당되는 스토리가 없음
        {
            data[0] = new List<TextData> { new TextData("BugJava", 999, 0, "BG/Ch\nText","", "", "", "", "", "BugJava", 1f, "", 0f, "", 0f, "", 0f, "BugJava", "버그", "버그버그", 0, "", "", "", "", "", "", "", "") };
            Data.GameData.InGameData.InGameSprite.Add("BugJava", LoadSpirte("BugJava"));
        }

        Data.GameData.InGameData.TextData = data;
    }

    public void TextDataClear()
    {
        Data.GameData.InGameData.TextData = null;
        Data.GameData.InGameData.InGameSprite = new Dictionary<string, Sprite>();
    }
}
