using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadlyEnviroment : MonoBehaviour
{
    [SerializeField]
    private GameObject[] Rubble;
    [SerializeField]
    private Transform [] RubbleOrigins;
    private Material [] RubbleDimensions;

    
    public void Collapse(int Varient, int Dimension,int Room)
    {
        Vector3 Randnum = new Vector3(0,0,0);
        GameObject RubbleObj = Instantiate(Rubble[Varient], RubbleOrigins[Dimension * Room].transform.position + Randnum, Quaternion.identity);
        RubbleObj.GetComponent<Renderer>().material = RubbleDimensions[Dimension];
    }
}
