using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonshow : MonoBehaviour
{
    public void SumbitData(){
        Debug.Log("Sumbitting Data");
        HandleSerialisation handleSerialisation = GameObject.FindGameObjectWithTag("Handle").GetComponent<HandleSerialisation>();
        handleSerialisation.CreateTransformationData();
    }
}
