using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearningTest : MonoBehaviour
{
    [ContextMenu("RangeTest")]
    private void RangeTest()
    {
        Debug.Log(LevelTimer.LevelTimersTra[0]);
        Debug.Log(LevelTimer.LevelTimersTra[1]);
        Debug.Log(LevelTimer.LevelTimersTra[2]);
        Debug.Log(CurrentPosition.CurrentDimension);
        Debug.Log(CurrentPosition.CurrentChamber);
    }
}
