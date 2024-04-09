using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGFadein : EffectBase
{
    private Coroutine  effectC   = null;
    private Sprite     newSprite = null;
    private GameObject fadeIn    = null;
    
    public override void DoEffect()
    {
        effectC = StartCoroutine(BGFadeinEffect());
    }
    private IEnumerator BGFadeinEffect()
    {

        float a = 1.0f;
        float time  = 1.0f;
    
        Image image = gameObject.GetComponent<Image>();
        
        if(image)
        {
            if (Value.Value1 != null) time = Util.StringToFloat(Value.Value1);
            if (Value.Value2 != null) newSprite = ResourceManager.GetSprite(Value.Value2);

            fadeIn = new GameObject("@FadeIn");
            fadeIn.transform.parent = this.transform;
            Image fadeInImage = Util.GetOrAddComponent<Image>(fadeIn);
            fadeInImage.color = new Color(1f, 1f, 1f, 0f);
            fadeInImage.rectTransform.localScale = Vector3.one;
            fadeInImage.sprite = newSprite;

            while(a > 0)
            {
                a -= Time.deltaTime / time;

                Color color = new Color(1.0f, 1.0f, 1.0f, a);
                fadeInImage.color = color;
            }

            yield return null;
            image.sprite = newSprite;
            Destroy(fadeIn);
        }
        yield return null;
        EndEffect();
    }

    public override void ShowResult()
    {
        if (fadeIn)
        {
            Destroy(fadeIn);
        }

        if(newSprite != null) gameObject.GetComponent<Image>().sprite = newSprite;
    }

    public override void StopAll()
    {
        if (effectC != null)
            StopCoroutine(effectC);
    }
}
