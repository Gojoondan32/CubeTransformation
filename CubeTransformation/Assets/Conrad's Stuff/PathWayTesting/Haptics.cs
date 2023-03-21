using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Haptics : MonoBehaviour
{
    public void HapticEffect(int HapticVersion)
    {
        switch (HapticVersion)
        {
            case 100:
                // Slight Haptic Shake for 1 Second 1 Second Wait then Second Rumble For 1 Second
                // No Crumbling
                break;
            case 70:
                // Haptic Shake for 3 Seconds
                // Slight Crumbling
                break;
            case 40:
                // Haptics Shake for 5 Seconds
                // Little Crumbling
                break;
            case 10:
                // Itermitant Shaking for 10 Seconds
                // Major Crumbling
                break;
            case 0:
                // Repeated Shaking
                // Room Collapse
                // Return Player Back to HubWorld
                break;
            default:

                break;
        }
    }

}
