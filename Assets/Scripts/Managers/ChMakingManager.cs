//-------------------------------------------------------------------------------------------------
// @file	ChMakingManager.cs
//
// @brief	캐릭터 생성 매니저
//
// @date	2024-03-14
//
// Copyright 2024 Team One-eyed Games. All Rights Reserved.
//-------------------------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Text;
using Unity.VisualScripting;


/// <summary> 캐릭터 생성 메니저 </summary>
public class ChMakingManager : ManagerSingle<ChMakingManager>
{
    public enum ChIndex
    {
        Ch1,
        Ch2,
        Ch3,
        Ch4,

        MaxCount

    }
    enum EffectIndex
    {
        Effect0,
        Effect1,
        Effect2,
        Effect3,

        MaxCount

    }

    enum ChData
    { 
        ChCode,
        ChBody,
        Clothes,
        Eyebrows,
        Eyes,
        Mouth,
        Effects,
    }


    /// <summary> 캐릭터 배열 </summary>
    GameObject[] _ch;
    /// <summary> 캐릭터 정보 배열 </summary>
    ChPart[] _chPart;
    /// <summary> 캐릭터 풀링용 Root </summary>
    GameObject _chPool;     

    /// <summary>
    /// 
    /// </summary>
    /// <param name="num"></param>
    /// <param name="chInfo"></param>
    public void SetDefault(int num, ChInfo chInfo)
    {
        SetCh(_ch[num], _chPart[num], chInfo);
    }

    /// <summary>
    /// 메니저 초기 Set
    /// </summary>
    public void Set()
    {
        _chPool                   = new GameObject("@ChPool");
        _ch                       = new GameObject[(int)ChIndex.MaxCount];
        _chPart                   = new ChPart[(int)ChIndex.MaxCount];
        _chPart[(int)ChIndex.Ch1] = new ChPart();
        _chPart[(int)ChIndex.Ch2] = new ChPart();
        _chPart[(int)ChIndex.Ch3] = new ChPart();
        _chPart[(int)ChIndex.Ch4] = new ChPart();

        for (int i = 0; i < (int)ChIndex.MaxCount; i++)
        {
            _ch[i] = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Ch"), _chPool.transform);
            _ch[i].name = $"Ch{i+1}";
            Transform charBody  = _ch[i].transform.GetChild(0);
            _chPart[i].Body     = charBody.GetComponent<SpriteRenderer>();
            _chPart[i].Clothes  = charBody.GetChild(0).GetComponent<SpriteRenderer>();
            _chPart[i].Eye      = charBody.GetChild(1).GetComponent<SpriteRenderer>();
            _chPart[i].Eyebrow  = charBody.GetChild(2).GetComponent<SpriteRenderer>();
            _chPart[i].Mouth    = charBody.GetChild(3).GetComponent<SpriteRenderer>();
            _chPart[i].Effect   = new SpriteRenderer[(int)EffectIndex.MaxCount];
            _chPart[i].Effect[(int)EffectIndex.Effect0] = charBody.GetChild(4).GetChild(0).GetComponent<SpriteRenderer>();
            _chPart[i].Effect[(int)EffectIndex.Effect1] = charBody.GetChild(4).GetChild(1).GetComponent<SpriteRenderer>();
            _chPart[i].Effect[(int)EffectIndex.Effect2] = charBody.GetChild(4).GetChild(2).GetComponent<SpriteRenderer>();
            _chPart[i].Effect[(int)EffectIndex.Effect3] = charBody.GetChild(4).GetChild(3).GetComponent<SpriteRenderer>();

            _chPart[i].Body.color = new Color(1f, 1f, 1f, 0f);
        }
    }

    /// <summary>
    /// 캐릭터 생성 함수 Index를 사용
    /// </summary>
    /// <param name="chInfo"> 캐릭터 정보 </param>
    /// <param name="charIdx"> 캐릭터 번호 </param>
    public static void ChMaking(ChInfo chInfo, int charIdx)
    {
        Instance.SetCh(Instance._ch[charIdx], Instance._chPart[charIdx], chInfo);
    }

    /// <summary>
    /// 캐릭터 생성 오브젝트 이름을 사용
    /// </summary>
    /// <param name="chInfo"> 캐릭터 정보 </param>
    /// <param name="num"> 캐릭터 번호 </param>
    public static void ChMaking(ChInfo chInfo, GameObject ChObj)
    {
        int charIdx = 0;
        if(!Enum.TryParse<ChIndex>(ChObj.name, out ChIndex chIndex))
        {
            Util.DebugLog("ChMaking 파싱 실패");
        }
        Instance.SetCh(Instance._ch[charIdx], Instance._chPart[charIdx], chInfo);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="ch"></param>
    /// <param name="chPart"></param>
    /// <param name="chInfo"></param>
    void SetCh(GameObject ch, ChPart chPart, ChInfo chInfo)
    {
        if (chInfo.Scale == -1)
            ch.transform.localScale = Vector3.one;
        else
            ch.transform.localScale = new Vector2() * chInfo.Scale;

        if(chInfo.Pos != new Vector2(-1000, -1000))
            ch.transform.localPosition = chInfo.Pos;

        if (chInfo.Opacity == -1)
            SetOpacity(chPart, 0f);
        else
            SetOpacity(chPart, chInfo.Opacity);

        if (string.IsNullOrEmpty(chInfo.ImageCode.Trim()) || chInfo.ImageCode.Split("-").Length != 7)
        {
            chPart.Body.sprite                             = null;
            chPart.Clothes.sprite                          = null;
            chPart.Eyebrow.sprite                          = null;
            chPart.Eye.sprite                              = null;
            chPart.Mouth.sprite                            = null;
            chPart.Effect[(int)EffectIndex.Effect0].sprite = null;
            chPart.Effect[(int)EffectIndex.Effect1].sprite = null;
            chPart.Effect[(int)EffectIndex.Effect2].sprite = null;
            chPart.Effect[(int)EffectIndex.Effect3].sprite = null;
            return;
        }

        string[] code = chInfo.ImageCode.Split("-");

        int chCode   = int.Parse(code[0]);
        int bodyCode = int.Parse(code[1]);

        Vector2 clothesCorrection = DataManager.Instance.DebugLuvData.ChImage[chCode][bodyCode].ClothesPos;
        Vector2 faceCorrection    = DataManager.Instance.DebugLuvData.ChImage[chCode][bodyCode].FacePos;

        clothesCorrection = CorrectionVector(clothesCorrection);
        faceCorrection    = CorrectionVector(faceCorrection);

        var pos = Vector3.zero;
        Vector3 clothesPos = new Vector3(pos.x + clothesCorrection.x, pos.y + clothesCorrection.y, 0);
        Vector3 facePos    = new Vector3(pos.x + faceCorrection.x, pos.y + faceCorrection.y, 0);

        chPart.Clothes.transform.localPosition                          = clothesPos;
        chPart.Eyebrow.transform.localPosition                          = facePos;
        chPart.Eye.transform.localPosition                              = facePos;
        chPart.Mouth.transform.localPosition                            = facePos;
        chPart.Effect[(int)EffectIndex.Effect0].transform.localPosition = facePos;
        chPart.Effect[(int)EffectIndex.Effect1].transform.localPosition = facePos;
        chPart.Effect[(int)EffectIndex.Effect2].transform.localPosition = facePos;
        chPart.Effect[(int)EffectIndex.Effect3].transform.localPosition = facePos;


        chPart.Body.sprite = LoadPartImage($"{code[(int)ChData.ChCode]}_{code[(int)ChData.ChBody]}");

        if (code[(int)ChData.Clothes] != "@None")
            chPart.Clothes.sprite = LoadPartImage(code[(int)ChData.Clothes]);
        else
            chPart.Clothes.sprite = null;

        if (code[(int)ChData.Eyebrows] != "@None")
            chPart.Eyebrow.sprite = LoadPartImage(code[(int)ChData.Eyebrows]);
        else
            chPart.Eyebrow.sprite = null;

        if (code[(int)ChData.Eyes] != "@None")
            chPart.Eye.sprite = LoadPartImage(code[(int)ChData.Eyes]);
        else
            chPart.Eye.sprite = null;

        if (code[(int)ChData.Mouth] != "@None")
            chPart.Mouth.sprite = LoadPartImage(code[(int)ChData.Mouth]);
        else
            chPart.Mouth.sprite = null;

        string[] effects = code[(int)ChData.Effects].Split("/");
        for (int i = 0; i < effects.Length; i++)
        {
            var text = effects[i].Trim();
            if (text != "@None")
                chPart.Effect[i].sprite = LoadPartImage(text);
            else
                chPart.Effect[i].sprite = null;
        }
    }

    /// <summary>
    /// 알파 값 조절
    /// </summary>
    /// <param name="opacity"></param>
    public static void SetOpacity(ChPart chPart, float opacity)
    {
        Color AlpaColor = new Color(1f, 1f, 1f, opacity);
        chPart.Body.color    = AlpaColor;
        chPart.Clothes.color = AlpaColor;
        chPart.Eyebrow.color = AlpaColor;
        chPart.Eye.color     = AlpaColor;
        chPart.Mouth.color   = AlpaColor;
    }

    /// <summary>
    /// 알파 값 조절
    /// </summary>
    /// <param name="opacity"></param>
    public static void SetOpacity(GameObject Character, float opacity)
    {
        Color AlpaColor = new Color(1f, 1f, 1f, opacity);
        int num = (int)ParseIndex(Character);
        ChPart chPart = Instance._chPart[num];
        chPart.Body.color = AlpaColor;
        chPart.Clothes.color = AlpaColor;
        chPart.Eyebrow.color = AlpaColor;
        chPart.Eye.color = AlpaColor;
        chPart.Mouth.color = AlpaColor;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="correction"></param>
    /// <returns></returns>
    Vector2 CorrectionVector(Vector2 correction)
    {
        float pixelPerUnit = 100f;

        correction /= pixelPerUnit;

        return correction;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    Sprite LoadPartImage(string name)
    {
        Sprite image = ResourceManager.GetChSprite(name);

        return image;
    }

    static ChIndex ParseIndex(GameObject ChObj)
    {
        ChIndex chIndex = ChIndex.Ch1;
        if (!Enum.TryParse<ChIndex>(ChObj.name, out chIndex))
        {
            Util.DebugLog("ChMaking 파싱 실패");
            return 0;
        }
        return chIndex;
    }
    
}


/// <summary> 캐릭터 정보 Info </summary>
public class ChPart
{
    public SpriteRenderer Body;
    public SpriteRenderer Clothes;
    public SpriteRenderer Eyebrow;
    public SpriteRenderer Eye;
    public SpriteRenderer Mouth;
    public SpriteRenderer[] Effect;
}