using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DebugX.Internal
{
    public static class InternalUtility
    {
        public static Color32 GetColor(string colorCode)
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
    
        public static string ColorToString(Color32 color)
        {
            return $"#{color.r:X2}{color.g:X2}{color.b:X2}{color.a:X2}";
        }

        public static bool CompareColor32(Color32 color, Color32 compareColor)
        {
            return color.r == compareColor.r && color.g == compareColor.g &&
                   color.b == compareColor.b && color.a == compareColor.a;
        }
        
        public static Color[] GenerateSimilarColors(Color baseColor, int count, float delta)
        {
            var colors = new Color[count + 1];
            colors[0] = baseColor;
            for (var i = 1; i < count + 1; i++)
            {
                var offset = i * delta;
                var similarColor = new Color(
                    baseColor.r + offset,
                    baseColor.g + offset,
                    baseColor.b + offset
                );
                colors[i] = similarColor;
            }

            return colors;
        }
    
        public static string GenerateRandomString(int length)
        {
            const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

            var result = new char[length];
            var random = new System.Random();

            for (int i = 0; i < length; i++)
            {
                int index = random.Next(0, characters.Length);
                result[i] = characters[index];
            }

            return new string(result);
        }
    
        public static string ByteToMegabyte(float value)
        {
            return (value / 1048576).ToString("F1");
        }
    
        public static string GetCurrentUnixTimestamp()
        {
            return new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds() + "";
        }
    
        public static List<int> RandomUniqueListNumber(int length, int from, int to)
        {
            var list = new List<int>();
            if (length <= to - from)
            {
                var index = 0;
                while (index < length)
                {
                    var tmp = Random.Range(from, to);
                    if (list.Contains(tmp))
                    {
                        tmp = Random.Range(from, to);
                    }
                    else
                    {
                        list.Add(tmp);
                        index++;
                    }
                }

                return list;
            }
            else
            {
                return null;
            }
        }
        
        
        
    }
}
