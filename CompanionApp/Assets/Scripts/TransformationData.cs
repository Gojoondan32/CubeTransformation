using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TransformationData 
{
    // These points should be in world space when coming into this project
    public Vector3[] shapePoints;
    public Vector3[] playerPoints;
    public Vector3[] reflectionPoints;
}
