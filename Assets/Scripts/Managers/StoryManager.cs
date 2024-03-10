using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryManager : ManagerSingle<StoryManager>
{
    public InGameData InGameData;

    public void StartInGame()
    {
        EpisodeData data = DataManager.Instance.GetEpisodeData(InGameData.Story, InGameData.Episode);
        ResourceManager.Instance.SetSpriteData(data);
        EpisodeManager.Instance.StartEpisode(data, InGameData.Branch,  InGameData.Setence);
    }
}