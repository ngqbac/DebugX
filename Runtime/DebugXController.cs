using UnityEngine;

public class DebugXController : MonoBehaviour
{
    public static DebugXController Instance;

    public bool isOn;
    public Color32[] color = new Color32[5];

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
    }

    private void OnValidate()
    {
        if (DebugXHelper.CompareColor32(color[0], new Color32()))
        {
            color[0] = DebugXHelper.GetColorFromString("008000");
        }

        if (DebugXHelper.CompareColor32(color[1], new Color32()))
        {
            color[1] = DebugXHelper.GetColorFromString("87CEEB");
        }

        if (DebugXHelper.CompareColor32(color[2], new Color32()))
        {
            color[2] = DebugXHelper.GetColorFromString("FFA500");
        }

        if (DebugXHelper.CompareColor32(color[3], new Color32()))
        {
            color[3] = DebugXHelper.GetColorFromString("FF69B4");
        }

        if (DebugXHelper.CompareColor32(color[4], new Color32()))
        {
            color[4] = DebugXHelper.GetColorFromString("FF0000");
        }
    }
}