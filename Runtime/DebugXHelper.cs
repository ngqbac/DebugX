using System;
using UnityEngine;

public class DebugXHelper
{
    public static Color32 GetColorFromString(string colorCode)
    {
        if (colorCode.Length == 6)
        {
            colorCode += "FF";
        }

        var hex = Convert.ToUInt32(colorCode, 16);
        var r = ((hex & 0xff000000) >> 0x18) / 255f;
        var g = ((hex & 0xff0000) >> 0x10) / 255f;
        var b = ((hex & 0xff00) >> 8) / 255f;
        var a = (hex & 0xff) / 255f;

        return new Color(r, g, b, a);
    }
    
    public static string GetStringFromColor(Color32 color)
    {
        return $"#{color.r:X2}{color.g:X2}{color.b:X2}{color.a:X2}";
    }

    public static bool CompareColor32(Color32 color, Color32 compareColor)
    {
        return color.r == compareColor.r && color.g == compareColor.g &&
               color.b == compareColor.b && color.a == compareColor.a;
    }
}
