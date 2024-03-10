using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageChange : EffectBase
{
    public override void DoEffect()
    {
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        Image image = gameObject.GetComponent<Image>();

        if (spriteRenderer != null)
            spriteRenderer.sprite = ResourceManager.Instance.LoadSprite(Value.Value1);
        if (image != null)
            image.sprite = ResourceManager.Instance.LoadSprite(Value.Value1);

        EndEffect();
    }

    public override void ShowResult()
    {
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        Image image = gameObject.GetComponent<Image>();

        if (spriteRenderer != null)
            spriteRenderer.sprite = ResourceManager.Instance.LoadSprite(Value.Value1);
        if (image != null)
            image.sprite = ResourceManager.Instance.LoadSprite(Value.Value1);
    }

    public override void StopAll()
    {
    }
}
