using System.Collections;
using System.Collections.Generic;
using TMPro;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Text;

public class UI_InGame : UI_Scene
{
    enum TMP_Texts
    {
        Name,
        Text
    }

    enum Images
    {
        BG,
    }

    enum GameObjects
    {
        TextBox,
        ClickDisable,
        NextParagraph
    }

    [SerializeField] float FadeSpeed = 1f;


    public override void Init()
    {
        base.Init();

        Bind<TMP_Text>(typeof(TMP_Texts));
        Bind<Image>(typeof(Images));
        HardBind<GameObject>(typeof(GameObjects));

        Get<GameObject>((int)GameObjects.NextParagraph).AddUIEvent(ShowNextParagraph);
    }

    void ShowNextParagraph(PointerEventData data)
    {
        (Managers.Scene.CurrentScene as InGame).ParagraphStart();
        Get<GameObject>((int)GameObjects.NextParagraph).SetActive(false);
    }

    public IEnumerator StartGame()
    {
        yield return StartCoroutine(DefaultFirstUIEffect());
    }

    public void LoadGame()
    {

    }

    public void NextParagraphOn()
    {
        Get<GameObject>((int)GameObjects.NextParagraph).SetActive(true);
    }

    public IEnumerator TextShow(TextData data)
    {
        StringBuilder sb = new StringBuilder();
        Get<TMP_Text>((int)TMP_Texts.Name).text = data.Name;
        for(int i = 0; i < data.Text.Length; i++)
        {
            sb.Append(data.Text[i]);
            Get<TMP_Text>((int)TMP_Texts.Text).text = sb.ToString();
            yield return new WaitForSeconds(0.05f); // ÀÓ½Ã°ª
        }
    }

    IEnumerator DefaultFirstUIEffect()
    {
        yield return StartCoroutine(FadeIn());
        yield return StartCoroutine(TextBoxUp());
        Get<GameObject>((int)GameObjects.ClickDisable).SetActive(false);
    }

    IEnumerator FadeIn()
    {
        Get<Image>((int)Images.BG).sprite = Data.GameData.InGameData.InGameSprite[Data.GameData.InGameData.TextData[0][0].BGImage];
        while (Get<Image>((int)Images.BG).color.a < 1)
        {
            Get<Image>((int)Images.BG).color += new Color(0, 0, 0, Time.deltaTime * FadeSpeed);
            yield return null;
        }
    }

    IEnumerator TextBoxUp()
    {
        Get<GameObject>((int)GameObjects.TextBox).transform.localPosition = new Vector3(0, -800);
        var tween = Get<GameObject>((int)GameObjects.TextBox).transform.DOLocalMove(new Vector3(0, -300f, 0), 1f).SetEase(Ease.OutQuad);
        yield return tween.WaitForCompletion();
    }
}
