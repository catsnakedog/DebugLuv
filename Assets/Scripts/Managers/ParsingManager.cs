//-------------------------------------------------------------------------------------------------
// @file	ParsingManager.cs
//
// @brief	Parsing 메니저 
//
// @date	2024-03-14
//
// Copyright 2024 Team One-eyed Games. All Rights Reserved.
//-------------------------------------------------------------------------------------------------


using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using System;


/// <summary> Parsing 메니저 </summary>
public class ParsingManager
{
    /// <summary>
    /// DebugLuvData 파싱 함수
    /// </summary>
    /// <param name="sheetData"></param>
    /// <returns></returns>
    public DebugLuvData ParsingSheetData(SheetData sheetData)
    {
        DebugLuvData data = new();

        data.EpisodeType = ParsingEpisodeTypeData(sheetData);
        data.Story       = ParsingStoryData(sheetData);
        data.Choice      = ParsingChoiceData(sheetData);
        data.ChImage     = ParsingChImageData(sheetData);

        return data;
    }

    /// <summary>
    /// Episode 파싱 함수
    /// </summary>
    /// <param name="sheetData"></param>
    /// <returns></returns>
    private List<EpisodeTypeData> ParsingEpisodeTypeData(SheetData sheetData)
    {
        List<EpisodeTypeData> data = sheetData.EpisodeTypeData;

        return data;
    }

    /// <summary>
    /// 스토리 데이터 파싱 함수
    /// </summary>
    /// <param name="sheetData"></param>
    /// <returns></returns>
    private Dictionary<string, StoryData> ParsingStoryData(SheetData sheetData)
    {
        Dictionary<string, StoryData> data = new();

        string crruentStory = "";
        int crruentEpisode = 0;
        int crruentBranch = 0;

        foreach (var setenceData in sheetData.LineData)
        {
            if (string.IsNullOrEmpty(setenceData.Story) || setenceData.Episode == -1 || setenceData.Branch == -1)
            {
                data[crruentStory].Episode[crruentEpisode].Setence[crruentBranch][^1].Add(setenceData);
                continue;
            }

            if (!data.ContainsKey(setenceData.Story))
            {
                data[setenceData.Story] = new();
                data[setenceData.Story].Episode = new();
            }

            if (!data[setenceData.Story].Episode.ContainsKey(setenceData.Episode))
            {
                data[setenceData.Story].Episode[setenceData.Episode] = new();
                data[setenceData.Story].Episode[setenceData.Episode].Setence = new();
            }
            if (!data[setenceData.Story].Episode[setenceData.Episode].Setence.ContainsKey(setenceData.Branch))
                data[setenceData.Story].Episode[setenceData.Episode].Setence[setenceData.Branch] = new();

            data[setenceData.Story].Episode[setenceData.Episode].Setence[setenceData.Branch].Add(new());
            data[setenceData.Story].Episode[setenceData.Episode].Setence[setenceData.Branch][^1].Add(setenceData);

            crruentStory = setenceData.Story;
            crruentEpisode = setenceData.Episode;
            crruentBranch = setenceData.Branch;
        }

        return data;
    }


    /// <summary>
    /// ChoiceData 파싱 함수 
    /// </summary>
    /// <param name="sheetData"></param>
    /// <returns></returns>
    private Dictionary<int, ChoiceData> ParsingChoiceData(SheetData sheetData)
    {
        Dictionary<int, ChoiceData> data = new();

        foreach (SelectData selectData in sheetData.SelectData)
        {
            if (!data.ContainsKey(selectData.Choice))
            {
                data[selectData.Choice] = new();
                data[selectData.Choice].Select = new();
            }

            data[selectData.Choice].Select.Add(selectData);
        }

        return data;
    }

    /// <summary>
    /// 케릭터 이미지 파싱 함수
    /// </summary>
    /// <param name="sheetData"></param>
    /// <returns></returns>
    private Dictionary<int, Dictionary<int, ChImageData>> ParsingChImageData(SheetData sheetData)
    {
        Dictionary<int, Dictionary<int, ChImageData>> data = new();

        foreach(ChImageData chImageData in sheetData.ChImageData)
        {
            if (!data.ContainsKey(chImageData.ChCode))
            {
                data[chImageData.ChCode] = new();
                data[chImageData.ChCode][chImageData.BodyCode] = new();
            }

            data[chImageData.ChCode][chImageData.BodyCode] = chImageData;
        }

        return data;
    }
}
