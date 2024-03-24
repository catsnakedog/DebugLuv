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
        Util.DebugLog($"{Value.Value1}");
        Util.DebugLog($"{Value.Value2}");
        Util.DebugLog($"{Value.Value3}");

        SpriteRenderer spriteRenderer = null;
        if (spriteRenderer = Util.GetOrAddComponent< SpriteRenderer >(gameObject))
        {
            spriteRenderer.sprite = ResourceManager.GetSprite(Value.Value1);
            spriteRenderer.color = Color.white;
        }
        

        if(Value.Value2 != null)
        {
            float scale = Util.StringToFloat(Value.Value2);
            transform.localScale = new Vector3(scale, scale, 1.0f);
        }
        else
        {
            transform.localScale = Vector3.one;
        }

        if (Value.Value3 != null)
        {
            transform.localPosition = Util.StringToVector2(Value.Value3);
        }
        else
        {
            transform.localPosition = Vector3.zero;
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
