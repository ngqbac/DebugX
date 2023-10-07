using UnityEngine;

namespace DebugX.Attribute
{
    public enum LogUtilityType
    {
        Notification,
        Success,
        Warning,
        Query,
        Error
    }

    public static class LogUtilityAttribute
    {
        public static bool Initialized;
        public static bool IsOn;
        public static Color32[] TypeColor = new Color32[5];
    }
}