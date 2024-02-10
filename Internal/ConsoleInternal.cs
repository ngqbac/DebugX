using DebugX.Attributes;
using UnityEngine;

namespace DebugX.Console
{
    public static class ConsoleInternal
    {
        public static void TurnOn()
        {
            if (ConsoleAttribute.Initialized)
            {
                ConsoleAttribute.IsOn = true;
            }
            else
            {
                new GameObject("Console").AddComponent<ConsoleController>();
                ConsoleAttribute.IsOn = true;
            }
        }

        public static void TurnOff()
        {
            if (ConsoleAttribute.Initialized)
            {
                ConsoleAttribute.IsOn = false;
            }
        }
    }
}
