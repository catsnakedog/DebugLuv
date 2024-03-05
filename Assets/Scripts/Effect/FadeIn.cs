using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : EffectBase
{
    private Coroutine effectC;

    public override void DoEffect()
    {
        effectC = StartCoroutine(FadeInEffect());
    }

    private IEnumerator FadeInEffect()
    {
        float a = 0f;
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        Image image = gameObject.GetComponent<Image>();
        while (a < 1)
        {
            a += Time.deltaTime / Util.StringToFloat(Value.Value1);

            if (spriteRenderer != null)
            {
                Color color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, a);
                spriteRenderer.color = color;
            }
            if (image != null)
            {
                Color color = new Color(image.color.r, image.color.g, image.color.b, a);
                image.color = color;
            }

            yield return null;
        }

        if (spriteRenderer != null)
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1);
        if (image != null)
            image.color = new Color(image.color.r, image.color.g, image.color.b, 1);

        EndEffect();
    }

    public override void ShowResult()
    {
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        Image image = gameObject.GetComponent<Image>();

        if (spriteRenderer != null)
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1);
        if (image != null)
            image.color = new Color(image.color.r, image.color.g, image.color.b, 1);
    }

    public override void StopAll()
    {
        if (effectC != null)
            StopCoroutine(effectC);
    }
}
