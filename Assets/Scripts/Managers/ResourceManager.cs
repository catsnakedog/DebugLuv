using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class ResourceManager // Resource�� �����ϴ� Manager�̴�
{
    public T Load<T>(string path) where T : Object // Resource.Load�� �������� ������ ���� �߰� �� ���� ������ �߰��صд�
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

    public GameObject Instantiate(string path, Transform parent = null) // �ν���Ʈ �ϴ� ��� GameObject���� Prefabs ���� �ȿ� ������ ���� ��θ� ���� ������ �������ش�
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

    public void Destroy(GameObject go) // GameObject.Destroy�� �������� ������ ���� �߰� �� ���� ������ �߰��صд�
    {
        if(go == null)
        return;

        Object.Destroy(go);
    }

    public void TextDataSetting() // �ش� ChName, StoryNumber�� �����͸� �����Ѵ�
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

        if (data.Count == 0) // �ش�Ǵ� ���丮�� ����
        {
            data[0] = new List<TextData> { new TextData("BugJava", 999, 0, "BG/Ch\nText","", "", "", "", "", "BugJava", 1f, "", 0f, "", 0f, "", 0f, "BugJava", "����", "���׹���", 0, "", "", "", "", "", "", "", "") };
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
