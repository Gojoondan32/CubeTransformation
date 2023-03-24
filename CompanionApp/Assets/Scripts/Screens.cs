using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Screens : MonoBehaviour
{
    

    public void MoveToMiddleScreen(Transform middleScreen)
    {
        LeanTween.move(gameObject, middleScreen, 0.6f).setEaseOutQuad();
    } 
    public void MoveToOffScreen(Transform offScreen)
    {
        LeanTween.move(gameObject, offScreen, 0.6f).setEaseOutQuad();
    }

    
    
}
