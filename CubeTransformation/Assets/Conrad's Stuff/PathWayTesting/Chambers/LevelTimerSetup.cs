using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTimerSetup : MonoBehaviour
{
    private void Awake()
    {
        for (int i = 0; i < LevelTimer.LevelTimersRot.Length; i++)
        {
            LevelTimer.LevelTimersRot[i] = 200f;
        }
        for (int i = 0; i < LevelTimer.LevelTimersTra.Length; i++)
        {
            LevelTimer.LevelTimersTra[i] = 200f;
        }
        for (int i = 0; i < LevelTimer.LevelTimersRef.Length; i++)
        {
            LevelTimer.LevelTimersRef[i] = 200f;
        }

    }
}