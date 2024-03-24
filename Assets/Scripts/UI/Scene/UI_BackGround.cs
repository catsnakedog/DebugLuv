using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_BackGround : UI_Base
{
    enum UI
    {
        DefaultBG,
        BG,
        NextParagraph,
    }

    public Image GetBG(Sprite bg = null)
    {
        if (Get<Image>(UI.BG) == null) return null;

        if (bg != null) Get<Image>(UI.BG).sprite = bg;

        return Get<Image>(UI.BG);
    }
}
