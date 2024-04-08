using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BGWipe : EffectBase
{
    protected Coroutine effectC = null;
    private GameObject newBG    = null;
    public override void DoEffect()
    {
    }

    protected IEnumerator BGWipeEffect(Vector2 startVec)
    {
        float per        = 0f;
        float startTime  = 0f;
        float endTime    = 0.5f;
        Image oldImage   = Util.GetOrAddComponent<Image>(gameObject);
        Image newImage   = null;
        Sprite newSprite = null;
        float x_StartPos = UI_Manager.ScreenSize.x * startVec.x;
        float y_StartPos = UI_Manager.ScreenSize.y * startVec.y;

        newBG                      = new GameObject("NewBG");
        newBG.transform.parent     = this.transform;
        newBG.transform.position   = Vector3.zero;
        newBG.transform.localScale = Vector3.one;
        
        newImage = Util.GetOrAddComponent<Image>(newBG);
        newImage.rectTransform.position = new Vector3(x_StartPos, y_StartPos);

        // Value.Value2 == Sprite name
        if (Value.Value2 != null)
        {
            newSprite = ResourceManager.GetSprite(Value.Value2);
        }

        newImage.sprite = newSprite;


        if (oldImage != null)
        {
            newImage.rectTransform.sizeDelta = UI_Manager.ScreenSize;
        }

        if (Value.Value1 != null && Value.Value1 != "")
        {
            endTime = Util.StringToFloat(Value.Value1);
        }
        
        while (startTime < endTime)
        {
            startTime += Time.deltaTime;
            per = startTime / endTime;

            float nowX = Mathf.Lerp(x_StartPos, 0, per);
            float nowY = Mathf.Lerp(y_StartPos, 0, per);

            //newBG.transform.position = new Vector3(nowX, nowY, 0);
            newImage.rectTransform.localPosition = new Vector3(nowX, nowY, 0);
            yield return null;
        }

        oldImage.sprite = newSprite;

        if (newBG != null)
        {
            Destroy(newBG);
        }
        EndEffect();
    }

    public override void ShowResult()
    {
        if (newBG != null)
        {
            Destroy(newBG);
        }

        Image oldImage = Util.GetOrAddComponent<Image>(gameObject);
        if (Value.Value2 != null)
        {
            oldImage.sprite = ResourceManager.GetSprite(Value.Value2);
        }
    }

    public override void StopAll()
    {
        if (effectC != null)
            StopCoroutine(effectC);
    }
}

