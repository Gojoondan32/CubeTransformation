using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateShape : MonoBehaviour
{
    public void CreateRotationQuestion(List<Vector3> points){
        // Convert grid space points to world space
        List<Vector3> translatedPoints = new List<Vector3>();
        foreach (Vector3 point in points){
            translatedPoints.Add(LevelGrid.Instance.gridSystem.TransposeGridPositionToWorldPosition(point));
        }

        
    }
}
