using UnityEngine;
using Log = DebugX.LogUtility.LogUtility;
using LogType = DebugX.LogUtility.LogType;

namespace DebugX.Singleton
{
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance != null) return _instance;
                var objs = FindObjectsOfType(typeof(T)) as T[];
                if (objs is { Length: > 0 })
                {
                    _instance = objs[0];
                }

                if (objs is { Length: > 1 })
                {
                    Log.SentLog("There is more than one" + typeof(T).Name + "in the scene.", LogType.Error);
                }

                if (_instance != null) return _instance;
                var obj = new GameObject
                {
                    name = $"_{typeof(T).Name}"
                };
                _instance = obj.AddComponent<T>();

                return _instance;
            }
        }
    }
}