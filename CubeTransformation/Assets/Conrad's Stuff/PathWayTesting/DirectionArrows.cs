using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DirectionArrows : MonoBehaviour
{
    [SerializeField] private Controlablecart Cart;
    [SerializeField] private bool UporDown;
    [SerializeField] private float[] CheckPointList;
    public static int CurrentCheckpoint = -1;

    public void Activated()
    {
        if (UporDown == true)
        {
            Cart.AllAboard(Cart.ChosenPath, CheckPointList[CurrentCheckpoint],CheckPointList[CurrentCheckpoint + 1]);
            CurrentCheckpoint += 1;
        }
        
        if (UporDown == false)
        {
            Cart.AllAboard(Cart.ChosenPath, CheckPointList[CurrentCheckpoint], CheckPointList[CurrentCheckpoint - 1]);
            CurrentCheckpoint -= 1;
        }
        
    }

    private void FixedUpdate()
    {
        if(UporDown == true && Cart.InMovement != true && CurrentCheckpoint != -1 && CurrentCheckpoint != CheckPointList.Length - 1)
        {
            gameObject.GetComponent<Button>().interactable = true;
        }
        else if(UporDown == true)
        {
            gameObject.GetComponent<Button>().interactable = false;
        }
        if (UporDown == false && Cart.InMovement != true && CurrentCheckpoint != -1 && CurrentCheckpoint != 0)
        {
            gameObject.GetComponent<Button>().interactable = true;
        }
        else if(UporDown == false) 
        {
            gameObject.GetComponent<Button>().interactable = false; 
        }
    }

}
