using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DebugX.HelperUtility
{
    public static class Internal
    {
        public static Color32 StringToColor(string colorCode)
        {
            if (colorCode.Length == 6)
            {
                colorCode += "FF";
            }

            if (uint.TryParse(colorCode, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out uint hex))
            {
                byte r = (byte)((hex & 0xFF000000) >> 24);
                byte g = (byte)((hex & 0x00FF0000) >> 16);
                byte b = (byte)((hex & 0x0000FF00) >> 8);
                byte a = (byte)(hex & 0x000000FF);

                return new Color32(r, g, b, a);
            }
            else
            {
                // Handle invalid color code
                return Color.black;
            }
        }

        public static string ColorToString(Color32 color, bool includeAlpha = true)
        {
            return includeAlpha
                ? $"#{color.r:X2}{color.g:X2}{color.b:X2}{color.a:X2}"
                : $"#{color.r:X2}{color.g:X2}{color.b:X2}";
        }

        public static bool CompareColor32(Color32 color, Color32 compareColor, byte tolerance = 0)
        {
            return Math.Abs(color.r - compareColor.r) <= tolerance &&
                   Math.Abs(color.g - compareColor.g) <= tolerance &&
                   Math.Abs(color.b - compareColor.b) <= tolerance &&
                   Math.Abs(color.a - compareColor.a) <= tolerance;
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

            var result = new StringBuilder(length);
            var random = new System.Random();

            for (int i = 0; i < length; i++)
            {
                int index = random.Next(0, characters.Length);
                result.Append(characters[index]);
            }

            return result.ToString();
        }

        public static List<int> RandomUniqueListNumber(int length, int from, int to)
        {
            if (length <= to - from)
            {
                var list = new List<int>(length);
                var availableNumbers = new HashSet<int>(Enumerable.Range(from, to - from));

                for (int i = 0; i < length; i++)
                {
                    var randomNumber = availableNumbers.ElementAt(Random.Range(0, availableNumbers.Count));
                    list.Add(randomNumber);
                    availableNumbers.Remove(randomNumber);
                }

                return list;
            }
            else
            {
                return null;
            }
        }

        public static string ByteToMegabyte(float value)
        {
            return (value / 1048576).ToString("F1");
        }

        public static string GetCurrentUnixTimestamp()
        {
            return new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds() + "";
        }
    }
}