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
using UnityEngine;


public class SheetManager
{
    private SheetData _sheetData;

    public SheetData SheetData { get { return _sheetData; } }

    public SheetData SetSheetData()
    {
        _sheetData = new();

        TextAsset[] textAssets = Resources.LoadAll<TextAsset>("Data");
        ParsingSheetData(textAssets);

        return _sheetData;
    }

    public SheetData GetSheetData()
    {
        return _sheetData;
    }

    void ParsingSheetData(TextAsset[] textAssets)
    {
        Type dataType = _sheetData.GetType();
        foreach (TextAsset textAsset in textAssets)
        {
            Type variableType = Type.GetType(textAsset.name);

            IList myList = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(variableType));

            foreach (object element in ParsingData(variableType, textAsset))
            {
                myList.Add(Convert.ChangeType(element, variableType));
            }

            typeof(SheetData).GetField(textAsset.name).SetValue(_sheetData, Convert.ChangeType(myList, typeof(SheetData).GetField(textAsset.name).FieldType));
        }
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

    List<object> ParsingData(Type type, TextAsset textAsset)
    {
        List<object> data = new();

        string[] lines = textAsset.text.Split("\n");

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
