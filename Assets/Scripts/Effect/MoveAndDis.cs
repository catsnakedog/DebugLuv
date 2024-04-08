using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MoveAndDis : EffectBase
{
    private Coroutine effectC = null;

    public override void DoEffect()
    {
        effectC = StartCoroutine(A_Effect());
    }
    private IEnumerator A_Effect()
    {
        Image image = GetComponent<Image>();

        float per = 0f;
        float endTime = Util.StringToFloat(Value.Value1);
        float startTime = 0f;

        bool isUI = false;
        Vector2 endPos = Util.StringToVector2(Value.Value2);
        if (image != null) isUI = true;

        while (startTime < endTime)
        {
            startTime += Time.deltaTime;
            per = startTime / endTime;

            if (isUI)
            {
                float nowX = Mathf.Lerp(image.rectTransform.position.x, endPos.x, per);
                float nowY = Mathf.Lerp(image.rectTransform.position.y, endPos.y, per);

                image.rectTransform.localPosition = new Vector2(nowX, nowY);
            }
            else
            {
                float nowX = Mathf.Lerp(transform.position.x, endPos.x, per);
                float nowY = Mathf.Lerp(transform.position.y, endPos.y, per);

                transform.localPosition = new Vector2(nowX, nowY);
            }

            yield return null;
        }

        yield return null;

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null) spriteRenderer.color = new Color(1f, 1f, 1f, 0f);
        if (image != null) image.color                   = new Color(1f, 1f, 1f, 0f);

        EndEffect();
    }

    public override void ShowResult()
    {
    }

    public override void StopAll()
    {
        if (effectC != null)
            StopCoroutine(effectC);
    }
}
