using UnityEngine;

public class Logger : Debug {

	public static void Log(string Message)
    {
        Debug.Log(Message);
    }

    public static void LogError(string Message)
    {
        Debug.LogError(Message);
    }

    public static void LogWarning(string Message)
    {
        Debug.LogWarning(Message);
    }
}
