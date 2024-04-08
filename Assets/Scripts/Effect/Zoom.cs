using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoom : EffectBase
{
    protected Coroutine effectC;
    bool X, Y;
    public override void DoEffect()
    {
    }

    protected IEnumerator ZoomBREffect(Vector2 newPivot, bool x = true, bool y = true)
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Sprite sprite = spriteRenderer.sprite;
        Texture2D spriteTexture = sprite.texture;
        Rect spriteRect = sprite.rect;
        X = x;
        Y = y;
        // 货 Sprite 积己
        Sprite newSprite = Sprite.Create(spriteTexture, spriteRect, newPivot, sprite.pixelsPerUnit);

        // SpriteRenderer俊 货肺款 Sprite 且寸
        spriteRenderer.sprite = newSprite;

        float per = 0f;
        float originalSize = transform.localScale.x;
        float finalSize = Util.StringToFloat(Value.Value2);
        float endTime = Util.StringToFloat(Value.Value1);
        float startTime = 0f;
        while (startTime < endTime)
        {
            startTime += Time.deltaTime;
            per = startTime / endTime;
            float currentValue = Mathf.Lerp(originalSize, finalSize, per);
            transform.localScale = new Vector3((x ? currentValue : 1), (x ? currentValue : 1), 1f);

            yield return null;
        }

        SoundManager.Instance.Play(Value.Value1);
        yield return null;
        EndEffect();
    }

    public override void ShowResult()
    {
        float finalSize = Util.StringToFloat(Value.Value2);
        transform.localScale = new Vector3((X ? finalSize : 1), (Y ? finalSize : 1), 1f);
    }

    public override void StopAll()
    {
        if (effectC != null)
            StopCoroutine(effectC);
    }
}