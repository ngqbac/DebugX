using System;
using MyBox;
using UnityEngine;

public class DebugXController : MonoBehaviour
{
    public static DebugXController Instance;

    public bool isOn;
    [ConditionalField(nameof(isOn))] public ColorCollection color = new();
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
    }

    private void OnValidate()
    {
        if (DebugXHelper.CompareColor32(color.Value[0], new Color32()))
        {
            color.Value[0] = DebugXHelper.GetColorFromString("008000");
        }

        if (DebugXHelper.CompareColor32(color.Value[1], new Color32()))
        {
            color.Value[1] = DebugXHelper.GetColorFromString("87CEEB");
        }

        if (DebugXHelper.CompareColor32(color.Value[2], new Color32()))
        {
            color.Value[2] = DebugXHelper.GetColorFromString("FFA500");
        }

        if (DebugXHelper.CompareColor32(color.Value[3], new Color32()))
        {
            color.Value[3] = DebugXHelper.GetColorFromString("FF69B4");
        }

        if (DebugXHelper.CompareColor32(color.Value[4], new Color32()))
        {
            color.Value[4] = DebugXHelper.GetColorFromString("FF0000");
        }
    }
}

[Serializable]
public class ColorCollection : CollectionWrapper<Color32>
{
    public ColorCollection()
    {
        Value = new Color32[5];
    }
}