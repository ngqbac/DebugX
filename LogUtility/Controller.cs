using DebugX.Singleton;
using DebugX.Attributes;

namespace DebugX.LogUtility
{
    public class Controller: SingletonPersistent<Controller>
    {
        public bool isOn = true;
        
        public LogFormat logFormat = LogFormat.Color;
        
        [ConditionalField(nameof(logFormat), false, LogFormat.Color)]
        public LogColor logColor;
        
        [ConditionalField(nameof(logFormat), false, LogFormat.Affix)]
        public LogAffix logAffix = new("StartOfLog", "EndOfLog");
        
        public LogType logFilter = LogType.Everything;
        
        public LogStyle logStyle;

        public override void Awake()
        {
            base.Awake();

            Attribute.LogFormat = logFormat;
            Attribute.LogColor = logColor;
            Attribute.LogAffix = logAffix;
            
            Attribute.LogFilter = logFilter;

            Attribute.LogStyle = logStyle;
            
            Attribute.IsOn = isOn;
            Attribute.Initialized = true;
            
            LogUtility.SentLog($"LogUtility initialized!!!\n{Attribute.ToJson()}");
        }
    }
}