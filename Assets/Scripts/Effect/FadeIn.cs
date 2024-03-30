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

        float time = 0.5f;
        if (Value.Value1 != null && Value.Value1 != "")
        {
            time = Util.StringToFloat(Value.Value1);
        }

        bool IsCharacter = Util.IsCharacter(gameObject);
        while (a < 1)
        {
            a += Time.deltaTime / time;
            Color color = new Color(1f, 1f, 1f, a);
            if (IsCharacter)
            {
                ChMakingManager.SetOpacity(gameObject, a);
            }
            else
            {
                Util.SetColor(spriteRenderer, color);
                Util.SetColor(image, color);
            }

            yield return null;
        }

        if (IsCharacter)
            ChMakingManager.SetOpacity(gameObject, 1f);
        if (spriteRenderer != null)
            Util.SetColor(spriteRenderer, Color.white);
        if (image != null)
            Util.SetColor(image, Color.white);

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
