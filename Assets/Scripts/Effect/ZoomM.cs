using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomM : Zoom
{
    protected Vector2 _newPivot = new Vector2(0.5f, 0.5f);

    public override void DoEffect()
    {
        effectC = StartCoroutine(ZoomBREffect(_newPivot));
    }
}