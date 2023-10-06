using UnityEngine;

public class DebugX
{
    
    
    public static void Log(string log)
    {
        if (DebugXController.Instance != null)
        {
            Debug.Log(log);
        }
        else
        {
            Debug.Log("DebugXController has not been initialized");
        }
    }
}
