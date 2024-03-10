using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Text;
using Unity.VisualScripting;

public class ChMakingManager : ManagerSingle<ChMakingManager>
{
    GameObject[] _ch;
    ChPart[] _chPart;
    GameObject _chPool;

    public void SetDefault(int num, ChInfo chInfo)
    {
        SetCh(_ch[num], _chPart[num], chInfo);
    }

    public void Set()
    {
        _chPool = new GameObject("@ChPool");
        _ch = new GameObject[4];
        _chPart = new ChPart[4];
        _chPart[0] = new ChPart();
        _chPart[1] = new ChPart();
        _chPart[2] = new ChPart();
        _chPart[3] = new ChPart();

        for (int i = 0; i < 4; i++)
        {
            _ch[i] = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Ch"), _chPool.transform);
            _ch[i].name = $"Ch{i}";

            _chPart[i].Body = _ch[i].transform.GetChild(0).GetComponent<SpriteRenderer>();
            _chPart[i].Clothes = _ch[i].transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>();
            _chPart[i].Eye = _ch[i].transform.GetChild(0).GetChild(1).GetComponent<SpriteRenderer>();
            _chPart[i].Eyebrow = _ch[i].transform.GetChild(0).GetChild(2).GetComponent<SpriteRenderer>();
            _chPart[i].Mouth = _ch[i].transform.GetChild(0).GetChild(3).GetComponent<SpriteRenderer>();
            _chPart[i].Effect = new SpriteRenderer[4];
            _chPart[i].Effect[0] = _ch[i].transform.GetChild(0).GetChild(4).GetChild(0).GetComponent<SpriteRenderer>();
            _chPart[i].Effect[1] = _ch[i].transform.GetChild(0).GetChild(4).GetChild(1).GetComponent<SpriteRenderer>();
            _chPart[i].Effect[2] = _ch[i].transform.GetChild(0).GetChild(4).GetChild(2).GetComponent<SpriteRenderer>();
            _chPart[i].Effect[3] = _ch[i].transform.GetChild(0).GetChild(4).GetChild(3).GetComponent<SpriteRenderer>();

            _chPart[i].Body.color = new Color(1f, 1f, 1f, 0f);
        }
    }

    public void ChMaking(ChInfo chInfo, int num)
    {
        SetCh(_ch[num], _chPart[num], chInfo);
    }

    void SetCh(GameObject ch, ChPart chPart, ChInfo chInfo)
    {
        if (chInfo.Scale == -1)
            ch.transform.localScale = Vector3.one;
        else
            ch.transform.localScale = new Vector2() * chInfo.Scale;

        if(chInfo.Pos != new Vector2(-1000, -1000))
            ch.transform.localPosition = chInfo.Pos;

        if (chInfo.Opacity == -1)
            chPart.Body.color = new Color(1f, 1f, 1f, 0f);
        else
            chPart.Body.color = new Color(1f, 1f, 1f, chInfo.Opacity);

        if (string.IsNullOrEmpty(chInfo.ImageCode.Trim()) || chInfo.ImageCode.Split("-").Length != 7)
        {
            chPart.Body.sprite = null;
            chPart.Clothes.sprite = null;
            chPart.Eyebrow.sprite = null;
            chPart.Eye.sprite = null;
            chPart.Mouth.sprite = null;
            chPart.Effect[0].sprite = null;
            chPart.Effect[1].sprite = null;
            chPart.Effect[2].sprite = null;
            chPart.Effect[3].sprite = null;
            return;
        }

        string[] code = chInfo.ImageCode.Split("-");

        int chCode = int.Parse(code[0]);
        int bodyCode = int.Parse(code[1]);

        Vector2 clothesCorrection = DataManager.Instance.DebugLuvData.ChImage[chCode][bodyCode].ClothesPos;
        Vector2 faceCorrection = DataManager.Instance.DebugLuvData.ChImage[chCode][bodyCode].FacePos;

        clothesCorrection = CorrectionVector(clothesCorrection);
        faceCorrection = CorrectionVector(faceCorrection);

        var pos = Vector3.zero;
        Vector3 clothesPos = new Vector3(pos.x + clothesCorrection.x, pos.y + clothesCorrection.y, 0);
        Vector3 facePos = new Vector3(pos.x + faceCorrection.x, pos.y + faceCorrection.y, 0);

        chPart.Clothes.transform.localPosition = clothesPos;
        chPart.Eyebrow.transform.localPosition = facePos;
        chPart.Eye.transform.localPosition = facePos;
        chPart.Mouth.transform.localPosition = facePos;
        chPart.Effect[0].transform.localPosition = facePos;
        chPart.Effect[1].transform.localPosition = facePos;
        chPart.Effect[2].transform.localPosition = facePos;
        chPart.Effect[3].transform.localPosition = facePos;


        chPart.Body.sprite = LoadPartImage($"{code[0]}_{code[1]}");

        if (code[2] != "@None")
            chPart.Clothes.sprite = LoadPartImage(code[2]);
        else
            chPart.Clothes.sprite = null;

        if (code[3] != "@None")
            chPart.Eyebrow.sprite = LoadPartImage(code[3]);
        else
            chPart.Eyebrow.sprite = null;

        if (code[4] != "@None")
            chPart.Eye.sprite = LoadPartImage(code[4]);
        else
            chPart.Eye.sprite = null;

        if (code[5] != "@None")
            chPart.Mouth.sprite = LoadPartImage(code[5]);
        else
            chPart.Mouth.sprite = null;

        string[] effects = code[6].Split("/");
        for (int i = 0; i < effects.Length; i++)
        {
            var text = effects[i].Trim();
            if (text != "@None")
                chPart.Effect[i].sprite = LoadPartImage(text);
            else
                chPart.Effect[i].sprite = null;
        }
    }

    Vector2 CorrectionVector(Vector2 correction)
    {
        float pixelPerUnit = 100f;

        correction /= pixelPerUnit;

        return correction;
    }

    Sprite LoadPartImage(string name)
    {
        Sprite image = ResourceManager.Instance.LoadChSprite(name);

        return image;
    }
}

public class ChPart
{
    public SpriteRenderer Body;
    public SpriteRenderer Clothes;
    public SpriteRenderer Eyebrow;
    public SpriteRenderer Eye;
    public SpriteRenderer Mouth;
    public SpriteRenderer[] Effect;
}