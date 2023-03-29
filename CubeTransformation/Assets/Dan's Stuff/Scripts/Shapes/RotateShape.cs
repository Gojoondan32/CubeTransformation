using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RotateShape : MonoBehaviour
{
    [SerializeField] private GameObject testobject;
    private Dictionary<string, Vector3> directions;
    private bool arePointsOnTheGrid;
    private int rotationAmount;
    private Vector2 rotationPoint;

    private int searchRadiusX;

    private void Awake() {
        directions = new Dictionary<string, Vector3>();

        directions.Add("north", new Vector3 (0, 1, 0));
        directions.Add("south", new Vector3 (0, -1, 0));
        directions.Add("east", new Vector3 (1, 0, 0));
        directions.Add("west", new Vector3 (-1, 0, 0));
        directions.Add("northeast", new Vector3 (1, 1, 0));
        directions.Add("northwest", new Vector3 (-1, 1, 0));
        directions.Add("southeast", new Vector3 (1, -1, 0));
        directions.Add("southwest", new Vector3 (-1, -1, 0));

        directions.Add("north2", new Vector3 (0, 2, 0));
        directions.Add("south2", new Vector3 (0, -2, 0));
        directions.Add("east2", new Vector3 (2, 0, 0));
        directions.Add("west2", new Vector3 (-2, 0, 0));
        directions.Add("northeast2", new Vector3 (2, 2, 0));
        directions.Add("northwest2", new Vector3 (-2, 2, 0));
        directions.Add("southeast2", new Vector3 (2, -2, 0));
        directions.Add("southwest2", new Vector3 (-2, -2, 0));
    }
    
    public (Vector2 rotationPoint, int rotationAmount) CreateRotationQuestion(List<Vector3> points){
        arePointsOnTheGrid = false;
        rotationAmount = 0;
        // Convert grid space points to world space
        List<Vector3> translatedPoints = new List<Vector3>();
        foreach (Vector3 point in points){
            translatedPoints.Add(LevelGrid.Instance.gridSystem.TransposeGridPositionToWorldPosition(point));
        }

        Debug.Log("Got here");
        // Take the components of the rotation point away from each componenet of the points 
        // Flip those values 
        // Invert the Y sign

        return FindRotation(translatedPoints);
        //return TryAllRotations(translatedPoints);
        //return FindRotationPoint(translatedPoints);
        
    }
    // Create me a method which takes a list of points and finds all the points around them in a radius of 2

    private (Vector2 rotationPoint, int rotationAmount) FindRotation(List<Vector3> points){
        List<Vector3> rotatedPoints = points;
        List<Vector2> pointsAround = FindAllPointsAround(points);
        rotationPoint = new Vector2();
        while(arePointsOnTheGrid == false){
            Random.InitState(System.DateTime.Now.Millisecond); // Ensure randomness
            int randomValue = Random.Range(0, pointsAround.Count);
            rotationPoint = pointsAround[randomValue];
            rotationAmount = 0;

            for(int i = 0; i < 3; i++){
                rotationAmount++;
                rotatedPoints = DoRotation(rotatedPoints, rotationPoint);
                if(ArePointsOnTheGrid(rotatedPoints) == true) break;
            }   
        }

        /*
        for (int i = 0; i < rotatedPoints.Count; i++){
            Vector3 pos = LevelGrid.Instance.gridSystem.TransposeWorldPositionToGridPosition(rotatedPoints[i]);
            pos.z = LevelGrid.Instance.Marker.position.z;
            Destroy(Instantiate(testobject, pos, Quaternion.identity), 60f);
        }
        */
        Debug.Log(rotationAmount);
        
        return (rotationPoint, rotationAmount * 90);
    }

    private List<Vector2> FindAllPointsAround(List<Vector3> points){
        List<Vector2> pointsAround = new List<Vector2>();
        foreach(Vector3 point in points){
            for(int i = 0; i < 16; i++){
                Vector2 pointAround = new Vector2(point.x + directions[directions.Keys.ElementAt(i)].x, point.y + directions[directions.Keys.ElementAt(i)].y);
                if(!pointsAround.Contains(pointAround) && IsPointOnTheGrid(pointAround)){
                    pointsAround.Add(pointAround);
                }
                
            }
        }
        return pointsAround;
    }
    private void TestAllPointsAround(List<Vector2> points){
        foreach(Vector2 point in points){
            Vector3 pos = LevelGrid.Instance.gridSystem.TransposeWorldPositionToGridPosition(point);
            pos.z = LevelGrid.Instance.Marker.position.z;
            Instantiate(testobject, pos, Quaternion.identity);
        }
    }

    public bool EvaluateRotation(List<Vector3> playerPoints, List<Vector3> shapePoints){
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
        
        List<Vector3> rotatedPoints = worldSpaceShapePoints;
        for(int i = 0; i < rotationAmount; i++){
            rotatedPoints = DoRotation(rotatedPoints, rotationPoint);
        }

        for(int i = 0; i < rotatedPoints.Count; i++){
            for(int j = 0; j < worldSpacePlayerPoints.Count; j++){
                if(Mathf.RoundToInt(rotatedPoints[i].x) == Mathf.RoundToInt(worldSpacePlayerPoints[j].x) && 
                    Mathf.RoundToInt(rotatedPoints[i].y) == Mathf.RoundToInt(worldSpacePlayerPoints[j].y)){
                        correctPointsFound++;
                    }
                if(rotatedPoints[i] == worldSpacePlayerPoints[j]){
                    //correctPointsFound++;
                }
            }
        }
        Debug.Log($"Correct points: {correctPointsFound}");
        return correctPointsFound == 4 ? true : false;

    }


    private Vector2 TryAllRotations(List<Vector3> points){
        Vector2 rotationPoint = new Vector2(0, 0);
        List<Vector3> rotatedPoints = points;

        while(arePointsOnTheGrid == false){
            Random.InitState(System.DateTime.Now.Millisecond); // Ensure randomness
            int x = Random.Range(0, LevelGrid.Instance.gridSystem.GetWidth());
            int y = Random.Range(0, LevelGrid.Instance.gridSystem.GetHeight());
            rotationAmount = 0;
            rotationPoint = new Vector2(x, y);
            for(int i = 0; i < 3; i++){
                rotationAmount += 90;
                rotatedPoints = DoRotation(rotatedPoints, rotationPoint);
                if(ArePointsOnTheGrid(rotatedPoints) == true) break;
            }   
        }

        for (int i = 0; i < rotatedPoints.Count; i++){
            Vector3 pos = LevelGrid.Instance.gridSystem.TransposeWorldPositionToGridPosition(rotatedPoints[i]);
            pos.z = LevelGrid.Instance.Marker.position.z;
            Destroy(Instantiate(testobject, pos, Quaternion.identity), 10f);
        }
        Debug.Log(rotationAmount);
        return rotationPoint;
    }

    private Vector2 FindRotationPoint(List<Vector3> points){
        List<Vector3> rotatedPoints = new List<Vector3>();
        Vector2 rotationPoint; 
        do{
            Random.InitState(System.DateTime.Now.Millisecond); // Ensure randomness
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

    private bool IsPointOnTheGrid(Vector3 point){
        if(point.x > LevelGrid.Instance.gridSystem.GetWidth() - 1 || point.x < 0 || 
            point.y > LevelGrid.Instance.gridSystem.GetHeight() - 1 || point.y < 0) return false;
        else return true;
    }

    private bool ArePointsOnTheGrid(List<Vector3> points){
        for(int i = 0; i < points.Count; i++){
            if(points[i].x > LevelGrid.Instance.gridSystem.GetWidth() - 1 || points[i].x < 0 || 
                points[i].y > LevelGrid.Instance.gridSystem.GetHeight() - 1 || points[i].y < 0) return false;
        }
        Debug.Log("Correct x: " + points[0].x);
        Debug.Log("Correct y: " + points[0].y);
        arePointsOnTheGrid = true;
        return true;
    }
}
