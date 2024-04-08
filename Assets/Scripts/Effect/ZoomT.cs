using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomT : Zoom
{
    protected Vector2 _newPivot = new Vector2(0.5f, 1f);
    
    public override void DoEffect()
    {
        effectC = StartCoroutine(ZoomBREffect(_newPivot, false ,true));
    }
}