using DebugX.Attribute;
using UnityEngine;

namespace DebugX.LogUtility
{
    public class LogUtilityInternal : MonoBehaviour
    {
        public static void TurnOnLogUtility()
        {
            if (LogUtilityAttribute.Initialized)
            {
                LogUtilityController.Instance.IsOn = true;
            }
            else
            {
                new GameObject("LogUtility").AddComponent<LogUtilityController>().IsOn = true;
            }
        }

        public static void TurnOffLogUtility()
        {
            if (LogUtilityAttribute.Initialized)
            {
                LogUtilityController.Instance.IsOn = false;
            }
        }
    }
}
