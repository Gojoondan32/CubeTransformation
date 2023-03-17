using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTimer : MonoBehaviour
{
    public static float[] LevelTimers = new float[10];
    public static bool[] ChambersComplete = new bool[3];
    public static int Failures;
    private void Awake()
    {
        for(int i = 0; i < LevelTimers.Length; i++)
        {
            LevelTimers[i] = 200f;
        }
        for(int i = 0; i < ChambersComplete.Length; i++)
        {
            ChambersComplete[i] = false;
        }
        Failures = 0;
    }
}
