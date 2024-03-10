using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class ResourceManager : ManagerSingle<ResourceManager>, IClearable, IInit // Resource를 관리하는 Manager이다
{
    public Dictionary<string, Sprite> InGameSprite;

    public void Init()
    {
        InGameSprite = new();
    }

    public Sprite LoadSprite(string name)
    {
        Sprite sprite = Resources.Load<Sprite>($"Sprites/{name}");
        if(sprite == null)
        {
            Debug.LogWarning($"error_ResourceManager : {name} 이미지 파일이 존재하지 않습니다.");
            sprite = Resources.Load<Sprite>("Sprites/BugJava");
        }

        return sprite;
    }

    public Sprite LoadChSprite(string name)
    {
        Sprite sprite = Resources.Load<Sprite>($"Sprites/Ch/{name}");
        if (sprite == null)
        {
            Debug.LogWarning($"error_ResourceManager : {name} 캐릭터 이미지 파일이 존재하지 않습니다.");
            sprite = Resources.Load<Sprite>("Sprites/BugJava");
        }

        return sprite;
    }

    public Sprite GetSprite(string name)
    {
        if (InGameSprite[name] == null)
            InGameSprite[name] = LoadSprite(name);
        return InGameSprite[name];
    }

    public Sprite GetChSprite(string name)
    {
        if (InGameSprite[name] == null)
            InGameSprite[name] = LoadChSprite(name);
        return InGameSprite[name];
    }

    public GameObject Instantiate(string path)
    {
        GameObject target = Resources.Load<GameObject>(path);
        if (target == null)
        {
            Debug.LogWarning($"error_ResourceManager : {path} Prefab이 존재하지 않습니다.");
            return null;
        }
        else
            return GameObject.Instantiate(target);
    }

    public void SetSpriteData(EpisodeData data)
    {
        InGameSprite["@None"] = null;
        InGameSprite[""] = null;
        foreach(List<LineData> setence in data.Setence)
        {
            string[] parts;
            foreach(LineData line in setence)
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

    public void SetImageCode(string[] parts)
    {
        for (int i = 0; i < parts.Length; i++)
        {
            if (i == 0)
                continue;
            if (i == 1)
            {
                LoadChSprite($"{parts[0]}_{parts[1]}");
                continue;
            }
            if (i == parts.Length - 1)
            {
                foreach (string effect in parts[i].Split("/"))
                {
                    if (effect.Trim() != "@None" && !string.IsNullOrEmpty(effect.Trim()))
                        LoadChSprite(effect.Trim());
                }
                continue;
            }
            if (parts[i].Trim() != "@None" && !string.IsNullOrEmpty(parts[i].Trim()))
                LoadChSprite(parts[i].Trim());
        }
    }

    public void SpriteDataClear() // InGame이 끝난 후 호출
    {
        InGameSprite.Clear();
    }

    public void Clear()
    {
        SpriteDataClear();
    }
}
