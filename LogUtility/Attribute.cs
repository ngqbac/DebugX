using System;
using UnityEngine;

namespace DebugX.LogUtility
{
    public enum LogFormat
    {
        Color,
        Affix
    }
    
    [Flags]
    public enum LogType
    {
        /// <summary>
        /// Flag only
        /// </summary>
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

        /// <summary>
        /// Flag only
        /// </summary>
        Everything = ~None
    }

    [Serializable]
    public struct LogStyle
    {
        public int size;
        public bool bold;
        public bool italic;
    }

    [Serializable]
    public struct LogAffix
    {
        public string prefix;
        public string suffix;
    }

    [Serializable]
    public class LogColor
    {
        public Color32 error = new(255, 0, 0, 255);
        public Color32 warn = new(255, 165, 0, 255);
        public Color32 info = new(0, 128, 0, 255);
        public Color32 verbose = new(0, 0, 255, 255);
        public Color32 debug = new(128, 128, 128, 255);
        public Color32 silly = new(211, 211, 211, 255);
        private string[] _colorCode;
        public string GetColorCode(int index)
        {
            _colorCode = new[]
            {
                $"#{ColorUtility.ToHtmlStringRGB(error)}",
                $"#{ColorUtility.ToHtmlStringRGB(warn)}",
                $"#{ColorUtility.ToHtmlStringRGB(info)}",
                $"#{ColorUtility.ToHtmlStringRGB(verbose)}",
                $"#{ColorUtility.ToHtmlStringRGB(debug)}",
                $"#{ColorUtility.ToHtmlStringRGB(silly)}",
            };
            return _colorCode[index];
        }

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

        public static LogFormat LogFormat;
        public static LogColor LogColor;
        public static LogAffix LogAffix;
        public static LogType LogFilter;
        public static LogStyle LogStyle;

        public static bool Notified;

        public static string ToJson()
        {
            return
                $"IsOn: {_isOn}\nLogFormat: {LogFormat.ToString()}\nLogFilter: {JsonUtility.ToJson(LogFilter)}\nAffix: {JsonUtility.ToJson(LogAffix)}\nLogStyle: {JsonUtility.ToJson(LogStyle)}";
        }

        // public static readonly string[] ColorCode =
        // {
        //     "#FF0000",
        //     "#FFA500",
        //     "#008000",
        //     "#0000FF",
        //     "#808080",
        //     "#D3D3D3"
        // };
    }
}