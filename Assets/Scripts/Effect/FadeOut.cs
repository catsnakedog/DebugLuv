using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOut : EffectBase
{
    private Coroutine effectC;

    public override void DoEffect()
    {
        effectC = StartCoroutine(FadeInEffect());
    }

    private IEnumerator FadeInEffect()
    {
        float a = 1f;
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        Image image = gameObject.GetComponent<Image>();
        float time = 0.5f;
        if (Value.Value1 != null && Value.Value1 != "" )
        {
            time = Util.StringToFloat(Value.Value1);
        }
        
        while (a > 0)
        {
            a -= Time.deltaTime / time;

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
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0);
        if (image != null)
            image.color = new Color(image.color.r, image.color.g, image.color.b, 0);

        EndEffect();
    }

    public override void ShowResult()
    {
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        Image image = gameObject.GetComponent<Image>();

        if (spriteRenderer != null)
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0);
        if (image != null)
            image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
    }

    public override void StopAll()
    {
        if (effectC != null)
            StopCoroutine(effectC);
    }
}
