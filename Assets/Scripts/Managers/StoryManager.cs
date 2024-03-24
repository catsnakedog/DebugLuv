//-------------------------------------------------------------------------------------------------
// @file	.cs
//
// @brief	을 위한 매니저
//
// @date	2024-03-14
//
// Copyright 2024 Team One-eyed Games. All Rights Reserved.
//-------------------------------------------------------------------------------------------------


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
        EpisodeManager.StartEpisode(data, InGameData.Branch,  InGameData.Setence);
    }
}