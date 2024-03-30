using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StorageSpawn_FadeIn : EffectBase
{
    /// <summary> </summary>
    private Coroutine effectC;

    /// <summary>
    /// 
    /// </summary>
    public override void DoEffect()
    {
        effectC = StartCoroutine(StorageSpawn_FadeInEffect());
    }

    private IEnumerator StorageSpawn_FadeInEffect()
    {
        // Storage Index
        int idx = 0;
        if (Value.Value4 == null || Value.Value4 == "")
        {
            Util.DebugLogError("Storage Index NULL");
        }
        else
        {
            idx = Util.StringToInt(Value.Value4);
            EpisodeManager.ScheduleDropStorageIdx(idx);
        }

        // Storage 이름
        EpisodeManager.GetInGameObjPack().Storage.Add(idx, new GameObject($"Storage_{idx}"));
        GameObject storageObject = EpisodeManager.GetInGameObjPack().Storage[idx];

        // 이미지 넣기
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        Image image = gameObject.GetComponent<Image>();
        
        if(image != null)
        {
            image.sprite = ResourceManager.GetSprite(Value.Value2);
            image.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        }
        else if (spriteRenderer = Util.GetOrAddComponent<SpriteRenderer>(storageObject))
        {
            spriteRenderer.sprite = ResourceManager.GetSprite(Value.Value2);
            spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        }

        // Position 위치
        if (Value.Value3 != null)
        {
            if(image != null)
            {
                image.rectTransform.localPosition = Util.StringToVector3(Value.Value3);
            }
            if(storageObject != null)
            {
                storageObject.transform.localPosition = Util.StringToVector2(Value.Value3);
            }
        }
        else
        {
            if (image != null)
            {
                image.rectTransform.localPosition = Vector3.zero;
            }
            if (storageObject != null)
            {
                storageObject.transform.localPosition = Vector3.zero;
            }
        }

        // 페이드인
        float a = 0f;
        float time = Util.StringToFloat(Value.Value1);
        while (a < 1)
        {
            a += Time.deltaTime / time;

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
            spriteRenderer.color = Color.white;
        if (image != null)
            image.color = new Color(image.color.r, image.color.g, image.color.b, 1);

        yield return null;
        EndEffect();
    }

    /// <summary>
    /// 
    /// </summary>
    public override void ShowResult()
    {

    }

    /// <summary>
    /// 
    /// </summary>
    public override void StopAll()
    {

    }
}