using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryManager : ManagerSingle<StoryManager>
{
    EpisodeManager _episodeManager;

    public string Story;
    public int Episode;

    private void Awake()
    {
        _episodeManager = new();
    }

    public void StartEpisode()
    {
        EpisodeData data = DataManager.Instance.GetEpisodeData(Story, Episode);
        ResourceManager.Instance.SetSpriteData(data);
        _episodeManager.Test(data);
    }
}
