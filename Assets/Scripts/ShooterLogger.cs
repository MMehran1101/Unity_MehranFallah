using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterLogger : MonoBehaviour
{
    public static void LogInfo(string message)
    {
        Debug.Log(message);
    }
    public static void LogError(string message)
    {
        Debug.LogError(message);
    }
    public static void LogWarning(string message)
    {
        Debug.LogWarning(message);
    }
}