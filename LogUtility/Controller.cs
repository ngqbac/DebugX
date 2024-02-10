using DebugX.Singleton;
using UnityEngine;

namespace DebugX.LogUtility
{
    public class Controller: SingletonPersistent<Controller>
    {
        public bool isOn;

        public override void Awake()
        {
            base.Awake();
            Attribute.IsOn = isOn;
            Attribute.Initialized = true;
        }
    }
}