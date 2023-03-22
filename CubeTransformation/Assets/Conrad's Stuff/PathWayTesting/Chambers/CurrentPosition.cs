using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentPosition
{
    private static int currentDimension = 0;

    public static int CurrentDimension
    {
        get
        {
            return currentDimension;
        }
        set
        {
            currentDimension = value;
            
        }
    }

    private static int currentChamber = 0;

    public static int CurrentChamber
    {
        get
        {
            return currentChamber;
        }
        set
        {
            currentChamber = value;
        }
    }

}
