using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGame : BaseScene
{
    void Start()
    {
        DataManager.Instance.ParsingDebugLuvData();
        StoryManager.Instance.Story = "Com";
        StoryManager.Instance.Episode = 0;
        StoryManager.Instance.StartEpisode();
    }
}
