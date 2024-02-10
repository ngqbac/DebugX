using System;

namespace DebugX.LogUtility
{
    [Flags]
    public enum LogUtilityType
    {
        /// <summary>
        /// Indicates a critical issue or error in the application.
        /// </summary>
        Error,

        /// <summary>
        /// Highlights potential issues or situations that may cause problems in the future.
        /// </summary>
        Warn,

        /// <summary>
        /// Provides general information about the application's state or activities.
        /// </summary>
        Info,

        /// <summary>
        /// Detailed information that may be useful for understanding the inner workings of the application.
        /// </summary>
        Verbose,

        /// <summary>
        /// Messages used for debugging purposes.
        /// </summary>
        Debug,

        /// <summary>
        /// Extremely detailed and verbose information.
        /// </summary>
        Silly
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

        public static bool Notified;

        public static readonly string[] ColorCode = {
            "#FF0000",
            "#FFA500",
            "#008000",
            "#0000FF",
            "#808080",
            "#D3D3D3"
        };
    }
}