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
    private List<Vector3> reflectionPoints; // This is the reflection line points
    public List<Vector3> ReflectionPoints { get => reflectionPoints; }
    
    private int randomValue; // This is the reflection line value
    private bool reflectInX; 

    /// <Summary>
    /// Returns two points 
    /// </Summary>
    public void CreateReflectionQuestion(List<Vector3> points){
        List<Vector3> nonTransposedPoints = new List<Vector3>();

        //Convert the points coming in back into world space
        foreach(Vector3 point in points){
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
            reflectionPoints = GetXReflectionLine(xUpperBound, xLowerBound);
            ShapeManager.GenerateLines(lineRenderer, reflectionPoints);
        }
        else{
            reflectInX = false;
            reflectionPoints = GetYReflectionLine(yUpperBound, yLowerBound);
            ShapeManager.GenerateLines(lineRenderer, reflectionPoints);
        }
            
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
        Random.InitState(System.DateTime.Now.Millisecond); // Ensure randomness
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
        Random.InitState(System.DateTime.Now.Millisecond); // Ensure randomness
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

    public bool EvaluateReflection(List<Vector3> playerPoints, List<Vector3> shapePoints){
        List<Vector3> worldSpacePlayerPoints = new List<Vector3>();
        List<Vector3> worldSpaceShapePoints = new List<Vector3>();
        int correctPointsFound = 0;

        
        //Convert the points coming in back into world space
        foreach(Vector3 point in playerPoints){
            Vector3 worldSpacePoint = LevelGrid.Instance.gridSystem.TransposeGridPositionToWorldPosition(point);
            worldSpacePlayerPoints.Add(worldSpacePoint);
            //Debug.Log($"Player point: {point}");
            //Debug.Log($"Player point X: {worldSpacePoint.x}, Player Point Y: {worldSpacePoint.y}");
        }
        foreach(Vector3 point in shapePoints){
            Vector3 worldSpacePoint = LevelGrid.Instance.gridSystem.TransposeGridPositionToWorldPosition(point);
            worldSpaceShapePoints.Add(worldSpacePoint);
            //Debug.Log($"Shape point: {point}");
            //Debug.Log($"Shape point X: {worldSpacePoint.x}, Shape Point Y: {worldSpacePoint.y}");
        }
        

        foreach(Vector3 playerPoint in worldSpacePlayerPoints){
            int distanceToReflectedShapeX = Mathf.RoundToInt(Mathf.Abs(randomValue - (playerPoint.x - randomValue)));
            int distanceToReflectedShapeY = Mathf.RoundToInt(Mathf.Abs(randomValue - (playerPoint.y - randomValue)));
            Debug.Log($"X: {distanceToReflectedShapeX}, Y: {playerPoint.y}" );

            //Debug.Log($"Distance to reflected Y shape: {distanceToReflectedShapeY}");
            
            foreach(Vector3 shapePoint in worldSpaceShapePoints){
                if(reflectInX){ // Y values should be the same
                    
                    
                    

                    if(Mathf.RoundToInt(shapePoint.x) == distanceToReflectedShapeX && Mathf.RoundToInt(shapePoint.y) == Mathf.RoundToInt(playerPoint.y)){
                        // Found correct point
                        correctPointsFound++;
                        Debug.Log($"Shape point X: {shapePoint.x}, Shape Point Y: {shapePoint.y}" );
                        break;
                    }
                }
                else{
                    if (Mathf.RoundToInt(shapePoint.y) == distanceToReflectedShapeY && Mathf.RoundToInt(shapePoint.x) == Mathf.RoundToInt(playerPoint.x)){
                        // Found correct point
                        correctPointsFound++;
                        break;
                    }
                }
            }
        }
        
        Debug.Log("Correct points: " + correctPointsFound);
        return correctPointsFound == 4 ? true : false;
    }


    public void RemoveReflectionLines(){
        List<Vector3> points = new List<Vector3>() {
            new Vector3(0, 0, 0),
            new Vector3(0, 0, 0)
        };
        ShapeManager.GenerateLines(lineRenderer, points);
    }
}
