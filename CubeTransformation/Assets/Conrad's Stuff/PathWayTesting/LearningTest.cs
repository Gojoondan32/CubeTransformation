using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearningTest : MonoBehaviour
{
    [ContextMenu("RangeTest")]
    private void RangeTest()
    {
        Debug.Log(LevelTimer.LevelTimersTra[CurrentPosition.CurrentChamber]);
    }
}
