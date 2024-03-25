using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageSpawn : EffectBase
{
    /// <summary> </summary>
    private Coroutine effectC;
    
    /// <summary>
    /// 
    /// </summary>
    public override void DoEffect()
    {
        effectC = StartCoroutine(_StorageSpawn());
    }

    private IEnumerator _StorageSpawn()
    {
        int storageIdx = EpisodeManager.GetInGameObjPack().Storage.Count;
        EpisodeManager.GetInGameObjPack().Storage.Add(new GameObject($"Storage_{storageIdx}"));
        GameObject storageObject = EpisodeManager.GetInGameObjPack().Storage[storageIdx];

        SpriteRenderer spriteRenderer = null;
        if (spriteRenderer = Util.GetOrAddComponent< SpriteRenderer >(storageObject))
        {
            spriteRenderer.sprite = ResourceManager.GetSprite(Value.Value1);
            spriteRenderer.color = Color.white;
        }
        

        if(Value.Value2 != null)
        {
            float scale = Util.StringToFloat(Value.Value2);
            storageObject.transform.localScale = new Vector3(scale, scale, 1.0f);
        }
        else
        {
            storageObject.transform.localScale = Vector3.one;
        }

        if (Value.Value3 != null)
        {
            storageObject.transform.localPosition = Util.StringToVector2(Value.Value3);
        }
        else
        {
            storageObject.transform.localPosition = Vector3.zero;
        }

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
