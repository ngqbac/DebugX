using DebugX.Singleton;

namespace DebugX.LogUtility
{
    public class Controller: SingletonPersistent<Controller>
    {
        public bool isOn;
        public LogType logFilter = LogType.Everything;
        public LogStyle LOGStyle;

        public override void Awake()
        {
            base.Awake();

            LOGStyle.Size = 10;
            LOGStyle.Bold = false;
            LOGStyle.Italic = false;
            
            Attribute.IsOn = isOn;
            Attribute.Initialized = true;
        }
    }
}