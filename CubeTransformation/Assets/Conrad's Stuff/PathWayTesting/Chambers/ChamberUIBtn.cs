using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChamberUIBtn : MonoBehaviour
{
    [SerializeField]
    private ChamberCartMovement chamberCartMovement; 
    [SerializeField]
    private float[] Destinations;

    public void BtnPress()
    {
        Debug.Log(chamberCartMovement);
        if (CurrentPosition.CurrentChamber < 3)
        {
            Debug.Log(CurrentPosition.CurrentChamber);
            StartCoroutine(chamberCartMovement.Transition(Destinations[CurrentPosition.CurrentChamber]));
            CurrentPosition.CurrentChamber += 1;
        } 
    }

    private void FixedUpdate()
    {
        if (CurrentPosition.CurrentChamber == 3)
        {
            chamberCartMovement.ForwardButton.GetComponent<Button>().interactable = false;
        }
        
    }

    public Button GetForwardButton() => chamberCartMovement.ForwardButton.GetComponent<Button>();
}
