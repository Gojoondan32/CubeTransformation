using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollapseControls : MonoBehaviour
{

    private float RemainingTime;
    private TextMeshProUGUI TimeText;

    private void FixedUpdate()
    {
        //RemainingTime = TimeText.

        // Check Player isn't in hub world
        if (CurrentPosition.CurrentChamber != 0)
        {
            // If CurrentRoom.TimeAmount is one of the following numbers 100, 70, 40 ,10 ,0 Then Activate Haptics and, Deadly Enviroments for those rooms
            switch (CurrentPosition.CurrentDimension)
            {
                case 1: 

                    break;
                case 2:

                    break;
                case 3:

                    break;
            }

        }
        // Check Players
         

    }
}
