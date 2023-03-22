using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTimer
{
    public static float[] LevelTimersRef = new float[3];
    public static float[] LevelTimersTra = new float[3];
    public static float[] LevelTimersRot = new float[3];
    public static bool[] ChambersComplete = new bool[3] {false,false,false};
    public static int Failures = 0;
}
