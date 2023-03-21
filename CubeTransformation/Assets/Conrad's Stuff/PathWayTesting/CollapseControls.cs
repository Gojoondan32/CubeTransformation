using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollapseControls : MonoBehaviour
{
    public int CurrentRoom;

    private void FixedUpdate()
    {
        // Check Player isn't in hub world
        if (CurrentRoom != -1)
        {
            // If CurrentRoom.TimeAmount is one of the following numbers 100, 70, 40 ,10 ,0 Then Activate Haptics and, Deadly Enviroments for those rooms
        }
        // Check Players
         

    }
}
