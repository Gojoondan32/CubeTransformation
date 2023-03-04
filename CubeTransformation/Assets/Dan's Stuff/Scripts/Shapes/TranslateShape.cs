using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslateShape : MonoBehaviour
{
    private List<Vector3> translatedPoints;
    [SerializeField] private Transform testCube;
    private void Awake(){
        translatedPoints = new List<Vector3>();
    }


    public List<Vector3> RandomlyMoveShape(List<Vector3> points){
        translatedPoints.Clear(); // Prevent argument out of range exception

        foreach(Vector3 point in points){
            translatedPoints.Add(LevelGrid.Instance.gridSystem.TransposeGridPositionToWorldPosition(point));
        }

        int x = Random.Range(-LevelGrid.Instance.gridSystem.GetWidth() - 1, LevelGrid.Instance.gridSystem.GetWidth());
        int y = Random.Range(-LevelGrid.Instance.gridSystem.GetHeight() - 1, LevelGrid.Instance.gridSystem.GetHeight());
        while(!TryMoveShape(translatedPoints, x, y)){
            x = Random.Range(-LevelGrid.Instance.gridSystem.GetWidth() - 1, LevelGrid.Instance.gridSystem.GetWidth());
            y = Random.Range(-LevelGrid.Instance.gridSystem.GetHeight() - 1, LevelGrid.Instance.gridSystem.GetHeight());
        }

        for (int i = 0; i < translatedPoints.Count; i++){
            Vector3 tempPoint = new Vector3(translatedPoints[i].x + x, translatedPoints[i].y + y, 0);
            points[i] = LevelGrid.Instance.gridSystem.TransposeWorldPositionToGridPosition(tempPoint);
        }
        return points;
    }

    private bool TryMoveShape(List<Vector3> points, float distanceToMoveX, float distanceToMoveY){
        Vector3 translatedPoint = new Vector3(0, 0, 0);
        foreach(Vector3 point in points){
            translatedPoint = new Vector3(point.x + distanceToMoveX, point.y + distanceToMoveY, 0);
            if(translatedPoint.x > LevelGrid.Instance.gridSystem.GetWidth() - 1 || translatedPoint.x < 0 || 
                translatedPoint.y > LevelGrid.Instance.gridSystem.GetHeight() - 1 || translatedPoint.y < 0) return false;

        }
        return true;
    }
}
