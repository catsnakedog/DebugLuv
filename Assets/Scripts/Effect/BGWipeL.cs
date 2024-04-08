using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BGWipeL : BGWipe
{
    Vector2 startVec = new Vector2(1f, 0f);
    
    public override void DoEffect()
    {
        effectC = StartCoroutine(BGWipeEffect(startVec));
    }
}

