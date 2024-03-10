using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGame : BaseScene
{
    void Start()
    {
        StoryManager.Instance.InGameData = new(); // ÀÓ½Ã°ª
        StoryManager.Instance.InGameData.Story = "Com";
        StoryManager.Instance.InGameData.Episode = 0;
        ChMakingManager.Instance.Set();
        StoryManager.Instance.StartInGame();
    }
}
