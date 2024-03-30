//-------------------------------------------------------------------------------------------------
// @file	ResourceManager.cs
//
// @brief	���� ������ ���� �Ŵ���
//
// @date	2024-03-14
//
// Copyright 2024 Team One-eyed Games. All Rights Reserved.
//-------------------------------------------------------------------------------------------------


using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.TextCore.Text;


/// <summary> Resourc(����) ������ ���� �޴��� </summary>
public class ResourceManager : ManagerSingle<ResourceManager>, IClearable, IInit // Resource�� �����ϴ� Manager�̴�
{
    public Dictionary<string, Sprite> InGameSprite;

    public void Init()
    {
        InGameSprite = new();
    }

    public static Sprite GetSprite(string name)
    {
        
        if (Instance.InGameSprite == null) Instance.Init();

        string path = $"Sprites/{name}";

        if ( !Instance.InGameSprite.ContainsKey(path) )
        {
            Sprite _sprite = Resources.Load<Sprite>(path);
            Instance.InGameSprite.Add(path, _sprite);

            if(Instance.InGameSprite[path] == null)
            {
                Util.DebugLogWarning($"error_ResourceManager : [{name}] �̹��� ������ �������� �ʽ��ϴ�.");
                Instance.InGameSprite[path] = Resources.Load<Sprite>("Sprites/BugJava");
            }
        }

        return Instance.InGameSprite[path];
    }

    public static Sprite GetChSprite(string name)
    {
        return GetSprite($"CH/{name}");
    }
   
    public GameObject Instantiate(string path)
    {
        GameObject target = Resources.Load<GameObject>(path);
        if (target == null)
        {
            Debug.LogWarning($"error_ResourceManager : {path} Prefab�� �������� �ʽ��ϴ�.");
            return null;
        }
        else
            return GameObject.Instantiate(target);
    }

    public void SetSpriteData(EpisodeData data)
    {
        InGameSprite["@None"] = null;
        InGameSprite[""] = null;
        foreach(List<List<LineData>> branch in data.Setence.Values)
        {
            foreach (List<LineData> setence in branch)
            {
                string[] parts;
                foreach (LineData line in setence)
                {
                    parts = line.Ch1Info.ImageCode.Split("-");
                    SetImageCode(parts);
                    parts = line.Ch2Info.ImageCode.Split("-");
                    SetImageCode(parts);
                    parts = line.Ch3Info.ImageCode.Split("-");
                    SetImageCode(parts);
                    parts = line.Ch4Info.ImageCode.Split("-");
                    SetImageCode(parts);
                }
            }
        }
    }

    public void SetImageCode(string[] parts)
    {
        for (int i = 0; i < parts.Length; i++)
        {
            if (i == 0)
                continue;
            if (i == 1)
            {
                GetChSprite($"{parts[0]}_{parts[1]}");
                continue;
            }
            if (i == parts.Length - 1)
            {
                foreach (string effect in parts[i].Split("/"))
                {
                    if (effect.Trim() != "@None" && !string.IsNullOrEmpty(effect.Trim()))
                        GetChSprite(effect.Trim());
                }
                continue;
            }
            if (parts[i].Trim() != "@None" && !string.IsNullOrEmpty(parts[i].Trim()))
                GetChSprite(parts[i].Trim());
        }
    }

    public void SpriteDataClear() // InGame�� ���� �� ȣ��
    {
        InGameSprite.Clear();
    }

    public void Clear()
    {
        SpriteDataClear();
    }
}
