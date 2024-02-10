using DebugX.LogUtility;
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

        private static bool _isOn;

        public static bool IsOn
        {
            get => _isOn;
            set
            {
                if (value == _isOn) return;
                _isOn = value;
                if (Initialized)
                {
                    LogUtilityController.Instance.isOn = value;
                }
            }
        }

        public static bool Notified;
        
        public static Color32[] TypeColor = new Color32[5];
    }
}