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
            message = log.Color(DebugXHelper.GetStringFromColor(DebugXController.Instance.color.Value[(int)type]));
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