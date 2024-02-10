using DebugX.Extensions;
using UnityEngine;

namespace DebugX.LogUtility
{
    public static class LogUtility
    {
        public static void SentLog(string log, LogUtilityType type, int size = -1, bool bold = false,
            bool italic = false)
        {
            if (!Attribute.Initialized)
            {
                if (Attribute.Notified) return;
                Debug.Log("LogUtility has not been initialized.");
                Attribute.Notified = true;
            }

            if (!Attribute.IsOn) return;
            var message = log.Color(Attribute.ColorCode[(int)type]);
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

        public static void TurnOn()
        {
            if (Attribute.Initialized)
            {
                Attribute.IsOn = true;
            }
            else
            {
                new GameObject().AddComponent<Controller>();
                // new GameObject("LogUtility").AddComponent<Controller>();
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