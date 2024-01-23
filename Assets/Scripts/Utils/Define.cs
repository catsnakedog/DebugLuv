using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Define : MonoBehaviour
{

    public enum Scene
    {
        Unknown,
        Main,
        InGame,
        Loading,
    }

    public enum Sound
    {
        Bgm,
        Sfx,
        MaxCount,
    }

    public enum UIEvent
    {
        Click,
        Down,
        Up,
        Drag,
        Effect,
    }
}