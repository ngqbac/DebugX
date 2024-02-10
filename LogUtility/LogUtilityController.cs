using DebugX.Attribute;
using UnityEngine;

namespace DebugX
{
    public class LogUtilityController : MonoBehaviour
    {
        public static LogUtilityController Instance;

        public bool isOn;

        private void Awake()
        {
            name = "LogUtility";
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                if (Instance != this)
                {
                    Destroy(this);
                }
            }
            LogUtilityAttribute.IsOn = isOn;
            LogUtilityAttribute.Initialized = true;
        }
    }
}