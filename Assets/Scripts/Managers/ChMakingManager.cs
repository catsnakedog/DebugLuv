using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Text;
using Unity.VisualScripting;

public class ChMakingManager : ManagerSingle<ChMakingManager>, IInit
{
    GameObject[] _ch;
    ChPart[] _chPart;
    GameObject _chPool;

    public void Init()
    {
        _chPool = GameObject.Find("@ChPool");
        _ch = new GameObject[4];
        _chPart = new ChPart[4];
        _chPart[0] = new ChPart();
        _chPart[1] = new ChPart();
        _chPart[2] = new ChPart();
        _chPart[3] = new ChPart();

        for (int i = 0; i < 4; i++)
        {
            _ch[i] = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Ch"), _chPool.transform);
            _ch[i].SetActive(false);
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
        }
    }

    public void ChMaking(ChInfo chInfo, int num)
    {
        SettingCh(_ch[num], _chPart[num], chInfo.ImageCode);
    }

    void SettingCh(GameObject ch, ChPart chPart, string chImageCode)
    {
        if (chImageCode == null || chImageCode.Split("-").Length != 7)
        {
            ch.SetActive(false);
            return;
        }

        string[] code = chImageCode.Split("-");

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


        chPart.Body.sprite = LoadPartImage(code[0]);

        if (code[1] != "@None")
            chPart.Clothes.sprite = LoadPartImage(code[1]);
        else
            chPart.Clothes.sprite = null;

        if (code[2] != "@None")
            chPart.Eyebrow.sprite = LoadPartImage(code[2]);
        else
            chPart.Eyebrow.sprite = null;

        if (code[3] != "@None")
            chPart.Eye.sprite = LoadPartImage(code[3]);
        else
            chPart.Eye.sprite = null;

        if (code[4] != "@None")
            chPart.Mouth.sprite = LoadPartImage(code[4]);
        else
            chPart.Mouth.sprite = null;

        string[] effects = code[5].Split("/");
        for (int i = 0; i < effects.Length; i++)
        {
            var text = effects[i].Trim();
            if (text != "@None")
                chPart.Effect[i].sprite = LoadPartImage(text);
            else
                chPart.Effect[i].sprite = null;
        }

        ch.SetActive(true);
    }

    Vector2 CorrectionVector(Vector2 correction)
    {
        float pixelPerUnit = 100f;

        correction /= pixelPerUnit;

        return correction;
    }

    Sprite LoadPartImage(string name)
    {
        Sprite image;
        if (ResourceManager.Instance.InGameSprite.ContainsKey(name))
            image = ResourceManager.Instance.InGameSprite[name];
        else
            image = Resources.Load<Sprite>("Sprite/BugJava");

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