using System;

namespace DebugX.Extensions
{
    public static class StringExtension
    {
        public static bool IsNullOrEmpty(this string str) => string.IsNullOrEmpty(str);
        public static bool NotNullOrEmpty(this string str) => !string.IsNullOrEmpty(str);

        public static string RemoveStart(this string str, string remove)
        {
            var index = str.IndexOf(remove, StringComparison.Ordinal);
            return index < 0 ? str : str.Remove(index, remove.Length);
        }
		
        public static string RemoveEnd(this string str, string remove)
        {
            return !str.EndsWith(remove) ? str : str.Remove(str.LastIndexOf(remove, StringComparison.Ordinal));
        }
        public static string Bold(this string str) => "<b>" + str + "</b>";
        public static string Color(this string str, string color) => $"<color={color}>{str}</color>";
        public static string Italic(this string str) => "<i>" + str + "</i>";
        public static string Size(this string str, int size) => $"<size={size}>{str}</size>";
        public static string Suffix(this string str, string suffix) => $"{str}{suffix}";
        public static string Prefix(this string str, string prefix) => $"{prefix}{str}";
    }
}