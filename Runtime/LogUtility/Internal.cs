using UnityEngine;

namespace DebugX.LogUtility
{
    public static class Internal
    {
        public static void TurnOn()
        {
            if (Attribute.Initialized)
            {
                Attribute.IsOn = true;
            }
            else
            {
                new GameObject("LogUtility").AddComponent<Controller>();
                Attribute.IsOn = true;
            }
        }

        public static void TurnOff()
        {
            if (Attribute.Initialized)
            {
                Attribute.IsOn = false;
            }
        }
    }
}