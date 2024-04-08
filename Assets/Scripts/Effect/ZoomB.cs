using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomB : Zoom
{
    protected Vector2 _newPivot = new Vector2(0.5f, 0);
    
    public override void DoEffect()
    {
        effectC = StartCoroutine(ZoomBREffect(_newPivot, false, true));
    }
}