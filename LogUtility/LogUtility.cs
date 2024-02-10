using DebugX.Attribute;
using DebugX.Internal;
using UnityEngine;

namespace DebugX.LogUtility
{
    public static class LogUtility
    {
        public static void SentLog(string log, LogUtilityType type, int size = -1, bool bold = false,
            bool italic = false)
        {
            if (!LogUtilityAttribute.Initialized)
            {
                if (LogUtilityAttribute.Notified) return;
                Debug.Log("LogUtility has not been initialized.");
                LogUtilityAttribute.Notified = true;
            }

            if (!LogUtilityAttribute.IsOn) return;
            var message = log.Color(InternalUtility.ColorToString(LogUtilityAttribute.TypeColor[(int)type]));
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
            Debug.Log(message);
        }
    }
}