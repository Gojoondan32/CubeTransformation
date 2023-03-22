using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateShape : MonoBehaviour
{
    [SerializeField] private GameObject testobject;
    public Vector2 CreateRotationQuestion(List<Vector3> points){
        // Convert grid space points to world space
        List<Vector3> translatedPoints = new List<Vector3>();
        foreach (Vector3 point in points){
            translatedPoints.Add(LevelGrid.Instance.gridSystem.TransposeGridPositionToWorldPosition(point));
        }

        Debug.Log("Got here");
        // Take the components of the rotation point away from each componenet of the points 
        // Flip those values 
        // Invert the Y sign
        return FindRotationPoint(translatedPoints);
        
    }

    private Vector2 FindRotationPoint(List<Vector3> points){
        List<Vector3> rotatedPoints = new List<Vector3>();
        Vector2 rotationPoint; 
        do{
            int x = Random.Range(0, LevelGrid.Instance.gridSystem.GetWidth());
            int y = Random.Range(0, LevelGrid.Instance.gridSystem.GetHeight());
            rotationPoint = new Vector2(x, y);
            rotatedPoints = DoRotation(points, rotationPoint);
        }
        while(ArePointsOnTheGrid(rotatedPoints) == false);

        //Debug.Log(rotationPoint);
        for (int i = 0; i < rotatedPoints.Count; i++){
            Vector3 pos = LevelGrid.Instance.gridSystem.TransposeWorldPositionToGridPosition(rotatedPoints[i]);
            pos.z = LevelGrid.Instance.Marker.position.z;
            Instantiate(testobject, pos, Quaternion.identity);
        }
        return rotationPoint;
    }
    private List<Vector3> DoRotation(List<Vector3> points, Vector3 rotationPoint){
        List<Vector3> rotatedPoints = new List<Vector3>();
        for(int i = 0; i < points.Count; i++){
            float x = ((points[i].x - rotationPoint.x) * -1) + rotationPoint.y;
            float y = (points[i].y - rotationPoint.y) + rotationPoint.x;
            //Vector3 rotatedPoint = LevelGrid.Instance.gridSystem.TransposeWorldPositionToGridPosition(new Vector3(y, x, 0));
            Vector3 rotatedPoint = new Vector3(y, x, 0);
            //rotatedPoint.z = LevelGrid.Instance.Marker.position.z;   
            rotatedPoints.Add(rotatedPoint);
        }
        return rotatedPoints;
    }

    private bool ArePointsOnTheGrid(List<Vector3> points){
        for(int i = 0; i < points.Count; i++){
            if(points[i].x > LevelGrid.Instance.gridSystem.GetWidth() - 1 || points[i].x < 0 || 
                points[i].y > LevelGrid.Instance.gridSystem.GetHeight() - 1 || points[i].y < 0) return false;
        }
        Debug.Log("Correct x: " + points[0].x);
        Debug.Log("Correct y: " + points[0].y);
        return true;
    }
}
