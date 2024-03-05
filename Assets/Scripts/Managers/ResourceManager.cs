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
        foreach(List<SetenceData> setences in data.Setence)
        {
            foreach(SetenceData setence in setences)
            {
                foreach(string sprite in setence.Ch1Info.ImageCode.Split("-"))
                {
                    if (sprite.Trim() != "@None")
                        LoadSprite(sprite.Trim());
                }
            }
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
