using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Move : EffectBase
{
    protected Coroutine effectC = null;

    public override void DoEffect()
    {
        effectC = StartCoroutine(MoveEffect());
    }
    private IEnumerator MoveEffect()
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

            if(isUI)
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
        EndEffect();
    }

    public override void ShowResult()
    {

        Image image = GetComponent<Image>();
        Vector2 endPos = Util.StringToVector2(Value.Value2);

        if (image != null)
        {
            image.rectTransform.localPosition = endPos;
        }
        else 
        {
            transform.localPosition = endPos;
        }
    }

    public override void StopAll()
    {
        if (effectC != null)
            StopCoroutine(effectC);
    }
}
