using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGWipeR : BGWipe
{
    Vector2 startVec = new Vector2(-1f, 0f);

    public override void DoEffect()
    {
        effectC = StartCoroutine(BGWipeEffect(startVec));
    }
}
