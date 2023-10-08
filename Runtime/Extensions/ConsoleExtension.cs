using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DebugX.Console
{
    /// <summary>
    /// A console to display Unity's debug logs in-game.
    /// </summary>
    class ConsoleExtension : MonoBehaviour
    {
        public static Version Version = new Version(1, 1, 0);

        #region Inspector Settings
        /// <summary>
        /// Whether to open as soon as the game starts.
        /// </summary>
        public bool openOnStart = false;

        /// <summary>
        /// Whether to only keep a certain number of logs, useful if memory usage is a concern.
        /// </summary>
        public bool restrictLogCount = false;

        /// <summary>
        /// Number of logs to keep before removing old ones.
        /// </summary>
        public int maxLogCount = 1000;

        #endregion

        private static readonly GUIContent CloseButton = new GUIContent("Close", "Close.");
        private static readonly GUIContent ClearLabel = new GUIContent("Clear", "Clear the contents of the console.");
        private static readonly GUIContent CollapseLabel = new GUIContent("Collapse", "Hide repeated messages.");
        private const int Margin = 20;
        private const string WindowTitle = "Console";

        private static readonly Dictionary<LogType, Color> LOGTypeColors = new()
        {
            { LogType.Assert, Color.white },
            { LogType.Error, Color.red },
            { LogType.Exception, Color.red },
            { LogType.Log, Color.white },
            { LogType.Warning, Color.yellow },
        };

        private bool _isCollapsed;
        private bool _isVisible;
        private readonly List<Log> _logs = new List<Log>();
        private Vector2 _scrollPosition;
        private readonly Rect _titleBarRect = new Rect(0, 0, 10000, 20);
        private Rect _windowRect = new Rect(Margin, Margin, Screen.width - (Margin * 2), Screen.height - (Margin * 2));

        private readonly Dictionary<LogType, bool> _logTypeFilters = new Dictionary<LogType, bool>
        {
            { LogType.Assert, true },
            { LogType.Error, true },
            { LogType.Exception, true },
            { LogType.Log, true },
            { LogType.Warning, true },
        };

        #region MonoBehaviour Messages

        void OnDisable()
        {
            Application.logMessageReceivedThreaded -= HandleLog;
        }

        void OnEnable()
        {
            Application.logMessageReceivedThreaded += HandleLog;
        }

        void OnGUI()
        {
            if (!_isVisible)
            {
                return;
            }

            _windowRect = GUILayout.Window(123456, _windowRect, DrawWindow, WindowTitle);
        }

        void Start()
        {
            if (openOnStart)
            {
                _isVisible = true;
            }
        }

        public void TurnOn()
        {
            _isVisible = true;
        }

        public void TurnOff()
        {
            _isVisible = false;
        }

        #endregion

        void DrawCollapsedLog(Log log)
        {
            GUILayout.BeginHorizontal();

                GUILayout.Label(log.Message);
                GUILayout.FlexibleSpace();
                GUILayout.Label(log.Count.ToString(), GUI.skin.box);

            GUILayout.EndHorizontal();
        }

        void DrawExpandedLog(Log log)
        {
            for (var i = 0; i < log.Count; i += 1)
            {
                GUILayout.Label(log.Message);
            }
        }

        void DrawLog(Log log)
        {
            GUI.contentColor = LOGTypeColors[log.Type];
            if (_isCollapsed)
            {
                DrawCollapsedLog(log);
            }
            else
            {
                DrawExpandedLog(log);
            }
        }

        void DrawLogList()
        {
            _scrollPosition = GUILayout.BeginScrollView(_scrollPosition);

            // Used to determine height of accumulated log labels.
            GUILayout.BeginVertical();

                var visibleLogs = _logs.Where(IsLogVisible);

                foreach (Log log in visibleLogs)
                {
                    DrawLog(log);
                }

            GUILayout.EndVertical();
            var innerScrollRect = GUILayoutUtility.GetLastRect();
            GUILayout.EndScrollView();
            var outerScrollRect = GUILayoutUtility.GetLastRect();

            // If we're scrolled to bottom now, guarantee that it continues to be in next cycle.
            if (Event.current.type == EventType.Repaint && IsScrolledToBottom(innerScrollRect, outerScrollRect))
            {
                ScrollToBottom();
            }

            // Ensure GUI colour is reset before drawing other components.
            GUI.contentColor = Color.white;
        }

        void DrawToolbar()
        {
            GUILayout.BeginHorizontal();

                if (GUILayout.Button(ClearLabel))
                {
                    _logs.Clear();
                }

                foreach (LogType logType in Enum.GetValues(typeof(LogType)))
                {
                    var currentState = _logTypeFilters[logType];
                    var label = logType.ToString();
                    _logTypeFilters[logType] = GUILayout.Toggle(currentState, label, GUILayout.ExpandWidth(false));
                    GUILayout.Space(20);
                }

                _isCollapsed = GUILayout.Toggle(_isCollapsed, CollapseLabel, GUILayout.ExpandWidth(false));

                if (GUILayout.Button(CloseButton))
                {
                    TurnOff();
                }

            GUILayout.EndHorizontal();
        }

        void DrawWindow(int windowID)
        {
            DrawLogList();
            DrawToolbar();

            // Allow the window to be dragged by its title bar.
            GUI.DragWindow(_titleBarRect);
        }

        Log? GetLastLog()
        {
            if (_logs.Count == 0)
            {
                return null;
            }

            return _logs.Last();
        }

        void HandleLog(string message, string stackTrace, LogType type)
        {
            var log = new Log {
                Count = 1,
                Message = message,
                StackTrace = stackTrace,
                Type = type,
            };

            var lastLog = GetLastLog();
            var isDuplicateOfLastLog = lastLog.HasValue && log.Equals(lastLog.Value);

            if (isDuplicateOfLastLog)
            {
                // Replace previous log with incremented count instead of adding a new one.
                log.Count = lastLog.Value.Count + 1;
                _logs[^1] = log;
            }
            else
            {
                _logs.Add(log);
                TrimExcessLogs();
            }
        }

        bool IsLogVisible(Log log)
        {
            return _logTypeFilters[log.Type];
        }

        bool IsScrolledToBottom(Rect innerScrollRect, Rect outerScrollRect)
        {
            var innerScrollHeight = innerScrollRect.height;

            // Take into account extra padding added to the scroll container.
            var outerScrollHeight = outerScrollRect.height - GUI.skin.box.padding.vertical;

            // If contents of scroll view haven't exceeded outer container, treat it as scrolled to bottom.
            if (outerScrollHeight > innerScrollHeight)
            {
                return true;
            }

            // Scrolled to bottom (with error margin for float math)
            return Mathf.Approximately(innerScrollHeight, _scrollPosition.y + outerScrollHeight);
        }

        void ScrollToBottom()
        {
            _scrollPosition = new Vector2(0, Int32.MaxValue);
        }

        void TrimExcessLogs()
        {
            if (!restrictLogCount)
            {
                return;
            }

            var amountToRemove = _logs.Count - maxLogCount;

            if (amountToRemove <= 0)
            {
                return;
            }

            _logs.RemoveRange(0, amountToRemove);
        }
    }

    /// <summary>
    /// A basic container for log details.
    /// </summary>
    struct Log
    {
        public int Count;
        public string Message;
        public string StackTrace;
        public LogType Type;

        public bool Equals(Log log)
        {
            return Message == log.Message && StackTrace == log.StackTrace && Type == log.Type;
        }
    }
}
