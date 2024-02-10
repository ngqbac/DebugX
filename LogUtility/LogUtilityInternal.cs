using DebugX.Attribute;
using UnityEngine;

namespace DebugX.LogUtility
{
    public static class LogUtilityInternal
    {
        public static void TurnOn()
        {
            if (LogUtilityAttribute.Initialized)
            {
                LogUtilityAttribute.IsOn = true;
            }
            else
            {
                new GameObject("LogUtility").AddComponent<LogUtilityController>();
                LogUtilityAttribute.IsOn = true;
            }
        }

        public static void TurnOff()
        {
            if (LogUtilityAttribute.Initialized)
            {
                LogUtilityAttribute.IsOn = false;
            }
        }
    }
}
