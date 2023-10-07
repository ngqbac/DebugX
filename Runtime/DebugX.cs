using UnityEngine;

public class DebugX
{
    public enum LogType
    {
        Notification,
        Success,
        Warning,
        Query,
        Error
    }

    public static void Log(string log, LogType type, int size = -1, bool bold = false, bool italic = false)
    {
        var message = log;
        if (DebugXController.Instance != null)
        {
            message = log.Color(DebugXHelper.GetStringFromColor(DebugXController.Instance.color[(int)type]));
            if (size != -1)
            {
                message = message.Size(size);
            }

            if (bold)
            {
                message = message.Bold();
            }

            if (italic)
            {
                message = message.Italic();
            }
        }

        Debug.Log(message);
    }
}

public static class StringExtension
{
    public static string Bold(this string str) => "<b>" + str + "</b>";
    public static string Color(this string str, string clr) => $"<color={clr}>{str}</color>";
    public static string Italic(this string str) => "<i>" + str + "</i>";
    public static string Size(this string str, int size) => $"<size={size}>{str}</size>";
}