using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslateShape : MonoBehaviour
{
    private int x;
    private int y;

    /// <Summary>
    /// Returns a list of Vector3s in grid position
    /// </Summary>
    public List<Vector3> RandomlyMoveShape(List<Vector3> points){
        //translatedPoints.Clear(); // Prevent argument out of range exception
        //List<Vector3> translatedPoints = LevelGrid.Instance.gridSystem.TransposeGridPositionListToWorldPosition(points);
        List<Vector3> translatedPoints = new List<Vector3>();
        foreach(Vector3 point in points){
            translatedPoints.Add(LevelGrid.Instance.gridSystem.TransposeGridPositionToWorldPosition(point));
        }
        x = 0;
        y = 0;

        do{
            x = Random.Range(-LevelGrid.Instance.gridSystem.GetWidth() - 1, LevelGrid.Instance.gridSystem.GetWidth());
            y = Random.Range(-LevelGrid.Instance.gridSystem.GetHeight() - 1, LevelGrid.Instance.gridSystem.GetHeight());
        }
        while(!TryMoveShape(translatedPoints, x, y));

        // Convert points found into grid position
        for (int i = 0; i < translatedPoints.Count; i++){
            Vector3 tempPoint = new Vector3(translatedPoints[i].x + x, translatedPoints[i].y + y, 0);
            points[i] = LevelGrid.Instance.gridSystem.TransposeWorldPositionToGridPosition(tempPoint);
        }
        return points;
    }

    public (int x, int y) CreateTranslationQuestion(List<Vector3> points){
        List<Vector3> translatedPoints = RandomlyMoveShape(points); //! This can create 0, 0 which should not be allowed
        return (x, y);

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

    public bool EvalutateTranslation(List<Vector3> playerPoints, List<Vector3> shapePoints){
        List<Vector3> worldSpacePlayerPoints = new List<Vector3>();
        List<Vector3> worldSpaceShapePoints = new List<Vector3>();
        int correctPointsFound = 0;

        
        //Convert the points coming in back into world space
        foreach(Vector3 point in playerPoints){
            worldSpacePlayerPoints.Add(LevelGrid.Instance.gridSystem.TransposeGridPositionToWorldPosition(point));
        }
        foreach(Vector3 point in shapePoints){
            worldSpaceShapePoints.Add(LevelGrid.Instance.gridSystem.TransposeGridPositionToWorldPosition(point));
        }

        foreach(Vector3 playerPoint in worldSpacePlayerPoints){
            foreach(Vector3 shapePoint in worldSpaceShapePoints){
                Debug.Log($"X: {shapePoint.x + x} Y: {shapePoint.y + y}");

                if(Mathf.RoundToInt(playerPoint.x) == Mathf.RoundToInt(shapePoint.x + x) && Mathf.RoundToInt(playerPoint.y) == Mathf.RoundToInt(shapePoint.y + y)){
                    correctPointsFound++;
                    break;
                }

                if(playerPoint == shapePoint){
                    //correctPointsFound++;
                    break;
                }
            }
        }
        Debug.Log("Correct points found: " + correctPointsFound);
        return correctPointsFound == 4 ? true : false;
    }
}
