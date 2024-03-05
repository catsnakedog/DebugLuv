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

    public static int StringToInt(string text)
    {
        int value;

        if (string.IsNullOrEmpty(text))
            value = -1;
        else
            value = int.Parse(text);

        return value;
    }
    public static float StringToFloat(string text)
    {
        float value;

        if (string.IsNullOrEmpty(text))
            value = -1f;
        else
            value = float.Parse(text);

        return value;
    }
    public static Vector2 StringToVector2(string text)
    {
        if (string.IsNullOrEmpty(text))
            return Vector2.zero;

        string[] xy = text.Split(",");
        Vector2 value = new Vector2(float.Parse(xy[0]), float.Parse(xy[1]));

        return value;
    }
}
