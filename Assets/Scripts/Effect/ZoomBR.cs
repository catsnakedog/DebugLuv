using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomBR : Zoom
{
    protected Vector2 _newPivot = new Vector2(1,0);
    
    public override void DoEffect()
    {
        effectC = StartCoroutine(ZoomBREffect(_newPivot));
    }
}
