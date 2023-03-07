using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectionTest : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private float highestX;
    [SerializeField] private float highestY;
    [SerializeField] private float lowestX;
    [SerializeField] private float lowestY;
    
    private int randomValue; // This is the reflection line value
    private bool reflectInX; 

    /// <Summary>
    /// Returns two points 
    /// </Summary>
    public void CreateReflectionQuestion(List<Vector3> points){
        List<Vector3> nonTransposedPoints = new List<Vector3>();

        //Convert the points coming in back into world space
        foreach(Vector3 point in points){
            Debug.Log(point);
            //nonTransposedPoints.Add(new Vector3((point.x / cellSize) - parentPosition.x, (point.y / cellSize) - parentPosition.y));
            nonTransposedPoints.Add(LevelGrid.Instance.gridSystem.TransposeGridPositionToWorldPosition(point));
        }

        // GetHeighstAndLowest should be called here
        GetHeighestAndLowest(nonTransposedPoints);
        //Needs to return two points to the shape manager so it can draw that line with the line renderer
        CreateReflectionLine(nonTransposedPoints); //! Debugging

    }

    //! Returning as a list here is for testing purposes only
    private void CreateReflectionLine(List<Vector3> points){
        //Find the distance to the highest point on the grid from the lowest point on the shape
        //Divide that length by 2 and add it to the lowest point on the shape (in example, 8 / 2 = 4, 1 + 4 = 5)
        
        // Reflect to the right of the shape
        float xUpperBound = ((LevelGrid.Instance.gridSystem.GetWidth() - 1 - lowestX) / 2) + lowestX;
        //Reflect to the left of the shape
        //The 0 here is the lowest value of the grid but it should always be 0
        float xLowerBound = (highestX - 0) / 2;

        // Reflect above shape
        float yUpperBound = ((LevelGrid.Instance.gridSystem.GetHeight() - 1 - lowestY) / 2) + lowestY;
        //Reflect below shape
        float yLowerBound = (highestY - 0) / 2;
        //Debug.Log($"Maximum bound: {yUpperBound}");
        
        int randomValue = Random.Range(0, 2);
        if(randomValue == 0){
            reflectInX = true;
            GenerateLines(GetXReflectionLine(xUpperBound, xLowerBound));
        }
        else{
            reflectInX = false;
            GenerateLines(GetYReflectionLine(yUpperBound, yLowerBound));
        }
            
    }
    private void FindLengthAndHeight(){

    }
    private void GetHeighestAndLowest(List<Vector3> points){
        highestX = float.MinValue;
        highestY = float.MinValue;
        lowestX = float.MaxValue;
        lowestY = float.MaxValue;

        for (int i = 0; i < points.Count; i++){
            //Debug.Log(points[i].x);
            //Debug.Log(points[i].y);
            if (points[i].x > highestX) highestX = points[i].x;
            else if (points[i].x < lowestX) lowestX = points[i].x;

            if (points[i].y > highestY) highestY = points[i].y;
            else if (points[i].y < lowestY) lowestY = points[i].y;
        }
    }

    //Create a list which stores all the valid reflection lines and then picks one at random
    private List<Vector3> GetXReflectionLine(float xUpperBound, float xLowerBound){
        int upperBound = Mathf.RoundToInt(Mathf.Floor(xUpperBound)); //! This should not need to be here if this is carried out above
        int lowerBound = Mathf.RoundToInt(Mathf.Floor(xLowerBound + 0.5f));
        randomValue = Random.Range(lowerBound, upperBound + 1); //! +1 is needed here because the method is exclusive
        Debug.Log($"UpperBound: {upperBound}");
        Debug.Log($"lowerBound: {lowerBound}");

        float cellSize = LevelGrid.Instance.gridSystem.GetCellSize();
        Vector3 parentPosition = LevelGrid.Instance.gridSystem.GetStartingPosition();

        return new List<Vector3>() { //! Needs to be reworked slightly to work with the new method in the grid system
            new Vector3((randomValue * cellSize) + parentPosition.x, parentPosition.y),
            new Vector3((randomValue * cellSize) + parentPosition.x, ((LevelGrid.Instance.gridSystem.GetHeight() - 1) * cellSize) + parentPosition.y)
        };
        
    }

    //Create a list which stores all the valid reflection lines and then picks one at random
    private List<Vector3> GetYReflectionLine(float yUpperBound, float yLowerBound){
        int upperBound = Mathf.RoundToInt(Mathf.Floor(yUpperBound)); //! This should not need to be here if this is carried out above
        int lowerBound = Mathf.RoundToInt(Mathf.Floor(yLowerBound + 0.5f));
        randomValue = Random.Range(lowerBound, upperBound + 1); //! +1 is needed here because the method is exclusive
        Debug.Log($"UpperBound: {upperBound}");
        Debug.Log($"lowerBound: {lowerBound}");

        float cellSize = LevelGrid.Instance.gridSystem.GetCellSize();
        Vector3 parentPosition = LevelGrid.Instance.gridSystem.GetStartingPosition();

        return new List<Vector3>() { //! Needs to be reworked slightly to work with the new method in the grid system
            new Vector3(parentPosition.x, (randomValue * cellSize) + parentPosition.y),
            new Vector3(((LevelGrid.Instance.gridSystem.GetWidth() - 1) * cellSize) + parentPosition.x, (randomValue * cellSize) + parentPosition.y)
        };
        
    }

    //! Testing - this is alreayd on the shape manager
    private void GenerateLines(List<Vector3> points, bool connectFinalToFirst = false)
    {
        for (int i = 0; i < points.Count; i++)
        {
            lineRenderer.SetPosition(i, new Vector3(points[i].x, points[i].y, 5));
        }

        if (connectFinalToFirst)
        {
            lineRenderer.positionCount = points.Count + 1; // Don't know if this is needed
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, new Vector3(points[0].x, points[0].y, 5));
        }

    }

    public bool EvaluateReflection(List<Vector3> playerPoints, List<Vector3> shapePoints){
        List<Vector3> nonTransposedPlayerPoints = playerPoints; //! TESTING
        List<Vector3> nonTransposedShapePoints = new List<Vector3>();
        int correctPointsFound = 0;

        //Convert the points coming in back into world space
        foreach(Vector3 point in playerPoints){
            //nonTransposedPlayerPoints.Add(LevelGrid.Instance.gridSystem.TransposeGridPositionToWorldPosition(point));
        }
        foreach(Vector3 point in shapePoints){
            nonTransposedShapePoints.Add(LevelGrid.Instance.gridSystem.TransposeGridPositionToWorldPosition(point));
        }

        foreach(Vector3 playerPoint in nonTransposedPlayerPoints){
            float distanceToReflectedShapeX = Mathf.Abs(randomValue - (playerPoint.x - randomValue));
            float distanceToReflectedShapeY = Mathf.Abs(randomValue - (playerPoint.y - randomValue));
            
            foreach(Vector3 shapePoint in nonTransposedShapePoints){
                if(reflectInX){ // Y values should be the same
                    if(shapePoint.x == distanceToReflectedShapeX && shapePoint.y == playerPoint.y){
                        // Found correct point
                        correctPointsFound++;
                        break;
                    }
                }
                else{
                    if (shapePoint.y == distanceToReflectedShapeY && shapePoint.x == playerPoint.x){
                        // Found correct point
                        correctPointsFound++;
                        break;
                    }
                }
            }
        }

        return correctPointsFound == 4 ? true : false;
    }
}
