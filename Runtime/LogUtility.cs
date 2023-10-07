using DebugX.Attribute;
using UnityEngine;

namespace DebugX.LogUtility
{
    public static class LogUtility
    {
        public static void SentLog(string log, LogUtilityType type, int size = -1, bool bold = false,
            bool italic = false)
        {
            if (!LogUtilityAttribute.IsOn) return;
            var message = log;
            if (LogUtilityAttribute.Initialized)
            {
                message = log.Color(InternalUtility.GetStringFromColor(LogUtilityAttribute.TypeColor[(int)type]));
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
}