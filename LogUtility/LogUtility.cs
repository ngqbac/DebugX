using DebugX.Extensions;
using DebugX.HelperUtility;
using UnityEngine;

namespace DebugX.LogUtility
{
    public static class LogUtility
    {
        public static void SentLog(string log, LogType type, LogStyle logStyle = default)
        {
            if (!Attribute.Initialized)
            {
                if (Attribute.Notified) return;
                Debug.Log("LogUtility has not been initialized.");
                Attribute.Notified = true;
            }

            if (!Attribute.IsOn) return;

            if (type == LogType.None) return;

            if (type == LogType.Everything) return;

            if (Attribute.LogFormat == LogFormat.Color)
            {
                log = log.Color(Attribute.ColorCode[Helper.GetEnumIndex(type)]);
                ApplyStyle(ref log, logStyle);
            }
            else
            {
                log = log.Prefix(Attribute.LogAffix.prefix);
                log = log.Suffix(Attribute.LogAffix.suffix);
            }

            WriteLog(log, type);
        }
        
        private static void ApplyStyle(ref string message, LogStyle logStyle)
        {
            if (logStyle.Equals(default(LogStyle)))
            {
                logStyle = Attribute.LogStyle;
            }
            
            message = message.Size(logStyle.size);
            
            if (logStyle.bold)
            {
                message = message.Bold();
            }

            if (logStyle.italic)
            {
                message = message.Italic();
            }
        }

        private static void WriteLog(string message, LogType type)
        {
            if ((type & Attribute.LogFilter) != 0)
            {
                Debug.Log(message);
            }
        }

        public static void TurnOn()
        {
            if (Attribute.Initialized)
            {
                Attribute.IsOn = true;
            }
            else
            {
                new GameObject().AddComponent<Controller>();
                Attribute.IsOn = true;
            }
        }

        public static void TurnOff()
        {
            if (Attribute.Initialized)
            {
                Attribute.IsOn = false;
            }
        }
    }
}