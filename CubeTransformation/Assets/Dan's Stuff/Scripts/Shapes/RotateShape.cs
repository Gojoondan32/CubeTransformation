using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateShape : MonoBehaviour
{
    [SerializeField] private GameObject testobject;
    public void CreateRotationQuestion(List<Vector3> points){
        // Convert grid space points to world space
        List<Vector3> translatedPoints = new List<Vector3>();
        foreach (Vector3 point in points){
            translatedPoints.Add(LevelGrid.Instance.gridSystem.TransposeGridPositionToWorldPosition(point));
        }
        Debug.Log("Got here");
        // Take the components of the rotation point away from each componenet of the points 
        // Flip those values 
        // Invert the Y sign
        FindRotationPoint(translatedPoints);
        
    }

    private void FindRotationPoint(List<Vector3> points){
        List<Vector3> rotatedPoints = new List<Vector3>();
        Vector2 rotationPoint; 
        do{
            rotationPoint = new Vector2(Random.Range(0, LevelGrid.Instance.gridSystem.GetWidth() - 1),
                Random.Range(0, LevelGrid.Instance.gridSystem.GetHeight() - 1));
            rotatedPoints = DoRotation(points, rotationPoint);
        }
        while(ArePointsOnTheGrid(rotatedPoints) == false);

        Debug.Log(rotationPoint);
        for (int i = 0; i < rotatedPoints.Count; i++){
            Instantiate(testobject, rotatedPoints[i], Quaternion.identity);
        }
    }
    private List<Vector3> DoRotation(List<Vector3> points, Vector3 rotationPoint){
        List<Vector3> rotatedPoints = new List<Vector3>();
        for(int i = 0; i < points.Count; i++){
            float x = (points[i].x - rotationPoint.x) * -1;
            float y = points[i].y - rotationPoint.y;
            Vector3 rotatedPoint = LevelGrid.Instance.gridSystem.TransposeWorldPositionToGridPosition(new Vector3(y, x, 0));
            rotatedPoint.z = LevelGrid.Instance.Marker.position.z;   
            rotatedPoints.Add(rotatedPoint);
        }
        return rotatedPoints;
    }

    private bool ArePointsOnTheGrid(List<Vector3> points){
        for(int i = 0; i < points.Count; i++){
            if(points[i].x > LevelGrid.Instance.gridSystem.GetWidth() || points[i].x < 0 || 
                points[i].y > LevelGrid.Instance.gridSystem.GetHeight() || points[i].y < 0) return false;
        }
        return true;
    }
}
