using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGame : BaseScene
{
    void Start()
    {
        StoryManager.Instance.InGameData = new(); // �ӽð�
        StoryManager.Instance.InGameData.Story = "Com";
        StoryManager.Instance.InGameData.Episode = 0;
        StoryManager.Instance.InGameData.Branch = 0;
        ChMakingManager.Instance.Set();
        StoryManager.Instance.StartInGame();
    }
}
