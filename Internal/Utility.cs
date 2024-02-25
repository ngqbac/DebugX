using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DebugX
{
    public static class Utility
    {
        public static Color32 StringToColor(string colorCode)
        {
            if (colorCode.Length == 6)
            {
                colorCode += "FF";
            }

            if (uint.TryParse(colorCode, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out uint hex))
            {
                var r = (byte)((hex & 0xFF000000) >> 24);
                var g = (byte)((hex & 0x00FF0000) >> 16);
                var b = (byte)((hex & 0x0000FF00) >> 8);
                var a = (byte)(hex & 0x000000FF);
                return new Color32(r, g, b, a);
            }
            else
            {
                // Handle invalid color code
                return Color.black;
            }
        }
        
        private static Color HexToColor(string hex)
        {
            ColorUtility.TryParseHtmlString(hex, out var color);
            return color;
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

        public static int GetEnumIndex<T>(T value) where T : Enum
        {
            var values = (T[])Enum.GetValues(typeof(T));
            var index = Array.IndexOf(values, value);

            if (Attribute.IsDefined(typeof(T), typeof(FlagsAttribute)))
            {
                // If the enum has the [Flags] attribute, subtract 1 to handle None value
                index -= 1;
            }

            return index;
        }
        
        /// <summary>
        /// Converts a time string in the format hh:mm:ss.fff to a float value.
        /// </summary>
        /// <param name="time">The time string to convert.</param>
        /// <returns>The float representation of the time string (e.g., "00:01:23.456" becomes 83.456).</returns>
        public static float TimeToFloat(string time)
        {
            // Convert time string to float (e.g., "00:01:23.456" to 83.456)
            var timeParts = time.Split(':');
            var secondsWithMilliseconds = timeParts[2].Split('.');
    
            var hours = float.Parse(timeParts[0]);
            var minutes = float.Parse(timeParts[1]);
            var seconds = float.Parse(secondsWithMilliseconds[0]);
            var milliseconds = float.Parse(secondsWithMilliseconds[1]);

            return hours * 3600 + minutes * 60 + seconds + milliseconds / 1000;
        }
        //
        // public static string GetTextWithRichTag(string root, string target)
        // {
        //     var index = root.IndexOf(target, StringComparison.Ordinal);
        //     if (index == -1) return target; // Target word not found
        //
        //     var openingTagIndex = root.IndexOf("<", index, StringComparison.Ordinal);
        //     var closingTagIndex = root.LastIndexOf(">", index + target.Length, StringComparison.Ordinal);
        //
        //     // Check if the '>' character is part of a rich text tag
        //     if (openingTagIndex >= 0 && closingTagIndex >= 0)
        //     {
        //         var richText = root.Substring(openingTagIndex, closingTagIndex - openingTagIndex + 1);
        //         return richText;
        //     }
        //
        //     // If '>' is not part of a rich text tag, check for the closest '<' characters
        //     var leftBracketIndex = root.IndexOf("<", index - 1, StringComparison.Ordinal);
        //     var rightBracketIndex = root.LastIndexOf(">", index + target.Length, StringComparison.Ordinal);
        //
        //     // Check if the closest '<' is before the target and the closest '>' is after the target
        //     if (leftBracketIndex >= 0 && rightBracketIndex >= 0 && leftBracketIndex < index && rightBracketIndex > index + target.Length)
        //     {
        //         var nonRichText = root.Substring(leftBracketIndex, rightBracketIndex - leftBracketIndex + 1);
        //         return nonRichText;
        //     }
        //
        //     return target; // No rich text tags found
        // }



    }
}