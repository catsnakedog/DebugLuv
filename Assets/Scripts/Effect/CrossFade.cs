//-------------------------------------------------------------------------------------------------
// @file	CrossFade.cs
//
// @brief	교차  FadeOut FadeIn을 위한 Effect
//
// @date	2024-03-14
//
// Copyright 2024 Team One-eyed Games. All Rights Reserved.
//-------------------------------------------------------------------------------------------------


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 교차 Fade 
/// </summary>
public class CrossFade : EffectBase
{
    private Coroutine effectC   = null;
    private GameObject fadeOut  = null;

    public override void DoEffect()
    {
        effectC = StartCoroutine(CrossFadeEffect());
    }

    private IEnumerator CrossFadeEffect()
    {
        float a     = 1.0f;
        float size  = 1.0f;
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        Image image = gameObject.GetComponent<Image>();
        
        SpriteRenderer fadeOutSpriteRenderer    = null;
        Image          fadeOutImage             = null;

        Sprite fadeInImage = null;
        if (Value.Value2 != null)
        {
            fadeInImage = ResourceManager.GetSprite(Value.Value2);
        }

        if (Value.Value3 != null) size = Util.StringToFloat(Value.Value3);

        fadeOut = new GameObject("@FadeOut");
        fadeOut.transform.parent = this.transform;
        fadeOut.transform.localScale = new Vector3(size, size, 1);

        if (spriteRenderer != null)
        {
            fadeOutSpriteRenderer        = Util.GetOrAddComponent<SpriteRenderer>(fadeOut);
            fadeOutSpriteRenderer.sprite = spriteRenderer.sprite;
            spriteRenderer.sprite        = fadeInImage;
        }
        if (image != null)
        {
            fadeOutImage        = Util.GetOrAddComponent<Image>(fadeOut);
            Sprite tmp = image.sprite;
            fadeOutImage.sprite = tmp;
            fadeOutImage.rectTransform.sizeDelta = UI_Manager.ScreenSize;
            image.sprite        = fadeInImage;
        }

        
        

        while (a > 0)
        {
            a -= Time.deltaTime / Util.StringToFloat(Value.Value1);

            if (spriteRenderer != null)
            {
                Color color          = new Color(1.0f, 1.0f, 1.0f, a);
                fadeOutSpriteRenderer.color = color;
            }
            if (image != null)
            {
                Color color = new Color(1.0f, 1.0f, 1.0f, a);
                fadeOutImage.color = color;
            }

            yield return null;
        }

        if (spriteRenderer != null)
        {
            fadeOutSpriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, 0);

        }
        if (image != null)
        {
            fadeOutImage.color = new Color(1.0f, 1.0f, 1.0f, 0);
        }
        if(fadeOut != null)
        {
            Destroy(fadeOut);
        }
        EndEffect();
    }

    public override void ShowResult()
    {
        if (fadeOut != null)
        {
            Destroy(fadeOut);
        }
    }

    public override void StopAll()
    {
        if (effectC != null)
            StopCoroutine(effectC);
    }
}

