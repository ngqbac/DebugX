using UnityEngine;

namespace DebugX.LogUtility
{
    public class Controller: MonoBehaviour
    {
        public static Controller Instance;

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
            
            Attribute.IsOn = isOn;
            Attribute.Initialized = true;
        }
    }
}