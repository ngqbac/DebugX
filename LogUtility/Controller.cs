using DebugX.Singleton;

namespace DebugX.LogUtility
{
    public class Controller: SingletonPersistent<Controller>
    {
        public bool isOn;
        public LogUtilityType supportType = LogUtilityType.Everything;

        public override void Awake()
        {
            base.Awake();
            Attribute.IsOn = isOn;
            Attribute.Initialized = true;
        }
    }
}