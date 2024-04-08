//-------------------------------------------------------------------------------------------------
// @file	.cs
//
// @brief	을 위한 매니저
//
// @date	2024-03-14
//
// Copyright 2024 Team One-eyed Games. All Rights Reserved.
//-------------------------------------------------------------------------------------------------


using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.IO;
using UnityEngine;


public class SheetManager
{
    private SheetData _sheetData;

    public SheetData SheetData { get { return _sheetData; } }

    /// <summary>
    /// 모든 시트 데이터를 파싱
    /// </summary>
    /// <returns></returns>
    public SheetData SetSheetData()
    {
        if(_sheetData == null) _sheetData = new();

        TextAsset[] textAssets = Resources.LoadAll<TextAsset>("Data");
        ParsingSheetData(textAssets);

        return _sheetData;
    }


    /// <summary>
    /// 특정 시트 데이터만을 세팅
    /// </summary>
    /// <param name="path"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    public SheetData SetSheetData(string path, Type type)
    {
        if(_sheetData == null) _sheetData = new();

        string fileData = File.ReadAllText(path);
        ParsingSheetData(type, fileData);

        return _sheetData;
    }


    public SheetData GetSheetData()
    {
        return _sheetData;
    }

    void ParsingSheetData(TextAsset[] textAssets)
    {
        foreach (TextAsset textAsset in textAssets)
        {
            Type variableType = Type.GetType(textAsset.name);
            ParsingSheetData(variableType, textAsset.text);
        }
    }

    void ParsingSheetData(Type variableType, string fileData)
    {
        IList myList = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(variableType));

        Debug.Log(variableType.FullName);
        foreach (object element in ParsingData(variableType, fileData))
        {
            myList.Add(Convert.ChangeType(element, variableType));
        }
        typeof(SheetData).GetField(variableType.FullName).SetValue(_sheetData, Convert.ChangeType(myList, typeof(SheetData).GetField(variableType.FullName).FieldType));


    }

    Dictionary<string, int> SetVariableIndex(string line)
    {
        Dictionary<string, int> variableIndex = new();

        string[] fields = line.Split("\t");

        for (int i = 0; i < fields.Length; i++)
            fields[i] = fields[i].Trim();

        for (int i = 0; i < fields.Length; i++)
        {
            variableIndex[fields[i]] = i;
        }

        return variableIndex;
    }

    /// <summary>
    /// 해당 TextAsset Data를 파싱
    /// </summary>
    /// <param name="type"> 파싱 타입 </param>
    /// <param name="textAsset"> Raw TextAsset Data  </param>
    /// <returns> 파싱된 List {type} </returns>
    List<object> ParsingData(Type type, TextAsset textAsset)
    {
        if (textAsset != null)
            return ParsingData(type, textAsset.text);
        else
            return null;
    }

    /// <summary>
    /// 해당 string Data를 파싱
    /// </summary>
    /// <param name="type"> 파싱 타입 </param>
    /// <param name="stringText"> Raw string Data </param>
    /// <returns> 파싱된 List {type} </returns>
    List<object> ParsingData(Type type, string stringText)
    {
        List<object> data = new();

        string[] lines = stringText.Split("\n");

        foreach (string line in lines.Skip(1))
        {
            if (string.IsNullOrEmpty(line.Trim()))
                continue;
            object lineData = SetLineData(Activator.CreateInstance(type), line, SetVariableIndex(lines[0]));
            data.Add(lineData);
        }

        return data;
    }

    object SetLineData(object data, string line, Dictionary<string, int> variableIndex)
    {
        string[] values = line.Split("\t");

        foreach(KeyValuePair<string, int> pair in variableIndex)
        {
            string[] variableNames = pair.Key.Split("_");

            Type type = data.GetType();
            FieldInfo fieldInfo = null;
            object target = data;

            bool isFirst = true;
            foreach (string  variableName in variableNames)
            {
                if (!isFirst)
                {
                    if(fieldInfo.GetValue(target) == null)
                        fieldInfo.SetValue(target, Activator.CreateInstance(type));
                    target = fieldInfo.GetValue(target);
                }

                fieldInfo = type.GetField(variableName.Trim());
                type = fieldInfo.FieldType;

                isFirst = false;
            }

            fieldInfo.SetValue(target, Util.StringToType(values[pair.Value] ,type));
        }

        return data;
    }
}
