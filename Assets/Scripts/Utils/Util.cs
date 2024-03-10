using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util{

    public static T GetOrAddComponent<T>(GameObject go) where T : UnityEngine.Component
    {
        T component = go.GetComponent<T>();
        if(component == null)
            component = go.AddComponent<T>();
        return component;
    }

    public static object StringToType(string text, Type type)
    {
        object value;

        if(type == typeof(int))
        {
            value = StringToInt(text);
        }
        else if (type == typeof(float))
        {
            value = StringToFloat(text);
        }
        else if (type == typeof(Vector2))
        {
            value = StringToVector2(text);
        }
        else if (type == typeof(string))
        {
            value = text;
        }
        else
        {
            Debug.LogWarning($"error_ParsingManager : {type.Name} 타입은 파싱 머신이 없습니다. 따로 만들어주세요.");
            value = null;
        }

        return value;
    }

    public static int StringToInt(string text)
    {
        int value;

        if (string.IsNullOrEmpty(text.Trim()))
            value = -1;
        else
            value = int.Parse(text);

        return value;
    }
    public static float StringToFloat(string text)
    {
        float value;

        if (string.IsNullOrEmpty(text.Trim()))
            value = -1f;
        else
            value = float.Parse(text);

        return value;
    }
    public static Vector2 StringToVector2(string text)
    {
        if (string.IsNullOrEmpty(text.Trim()))
            return Vector2.zero;

        string[] xy = text.Split(",");
        Vector2 value = new Vector2(float.Parse(xy[0]), float.Parse(xy[1]));

        return value;
    }
}
