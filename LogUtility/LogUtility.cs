using DebugX.Extensions;
using UnityEngine;

namespace DebugX.LogUtility
{
    public static class LogUtility
    {
        public static void SentLog(string log, LogType type, LogStyle logStyle)
        {
            if (!Attribute.Initialized)
            {
                if (Attribute.Notified) return;
                Debug.Log("LogUtility has not been initialized.");
                Attribute.Notified = true;
            }

            if (!Attribute.IsOn) return;
            
            var message = log.Color(Attribute.ColorCode[(int)type]);
            
            ApplyStyle(ref message, logStyle);
            Debug.Log($"{(int)type} {(int)Attribute.LOGFilter}");

            Debug.Log(message);
        }
        
        private static void ApplyStyle(ref string message, LogStyle logStyle)
        {
            if (logStyle.Size != -1)
            {
                message = message.Size(logStyle.Size);
            }

            if (logStyle.Bold)
            {
                message = message.Bold();
            }

            if (logStyle.Italic)
            {
                message = message.Italic();
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