using System;

namespace DebugX.LogUtility
{
    [Flags]
    public enum LogType
    {
        None = 0,

        /// <summary>
        /// Indicates a critical issue or error in the application.
        /// </summary>
        Error = 1 << 0,

        /// <summary>
        /// Highlights potential issues or situations that may cause problems in the future.
        /// </summary>
        Warn = 1 << 1,

        /// <summary>
        /// Provides general information about the application's state or activities.
        /// </summary>
        Info = 1 << 2,

        /// <summary>
        /// Detailed information that may be useful for understanding the inner workings of the application.
        /// </summary>
        Verbose = 1 << 3,

        /// <summary>
        /// Messages used for debugging purposes.
        /// </summary>
        Debug = 1 << 4,

        /// <summary>
        /// Extremely detailed and verbose information.
        /// </summary>
        Silly = 1 << 5,

        Everything = ~None
    }

    public struct LogStyle
    {
        public int Size;
        public bool Bold;
        public bool Italic;
    }

    public static class Attribute
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
                    Controller.Instance.isOn = value;
                }
            }
        }

        public static LogType LOGFilter;
        
        public static bool Notified;

        public static readonly string[] ColorCode =
        {
            "#FF0000",
            "#FFA500",
            "#008000",
            "#0000FF",
            "#808080",
            "#D3D3D3"
        };
    }
}