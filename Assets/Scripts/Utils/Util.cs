using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Util{
    List<int> vs;
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
            return new Vector2(-1000, -1000);

        string[] xy = text.Split(",");
        Vector2 value = new Vector2(float.Parse(xy[0]), float.Parse(xy[1]));

        return value;
    }
    public static Vector3 StringToVector3(string text)
    {
        Vector2 StringToVector2 = Util.StringToVector2(text);
        Vector3 value = new Vector3(StringToVector2.x, StringToVector2.y, 0);

        return value;
    }


    /// <summary>
    /// DebugLog
    /// </summary>
    /// <param name="Log"> 일반 Log </param>
    public static void DebugLog(string Log)
    {
#if UNITY_EDITOR
        Debug.Log(Log);
#endif
    }

    /// <summary>
    /// DebugLogWarning
    /// </summary>
    /// <param name="Log"> Warning Log </param>
    public static void DebugLogWarning(string Log)
    {
#if UNITY_EDITOR
        Debug.LogWarning(Log);
#endif
    }

    /// <summary>
    /// DebugLogError
    /// </summary>
    /// <param name="Log"> Error Log </param>
    public static void DebugLogError(string Log)
    {
#if UNITY_EDITOR
        Debug.LogError(Log);
#endif
    }

}
