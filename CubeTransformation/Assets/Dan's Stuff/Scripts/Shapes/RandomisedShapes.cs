using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RandomisedShapes : MonoBehaviour
{
    //[SerializeField] private Transform[] pointMarkers;
    private List<Vector3> chosenPoints;
    private List<Vector3> points;
    [SerializeField] private float totalAngles;

    private void Awake() {
        chosenPoints = new List<Vector3>();
        points = new List<Vector3>();
    }
    private void Start() {

    }

    public List<Vector3> StartDrawingShape(){
        totalAngles = 0;
        while(totalAngles != 360){
            totalAngles = 0;
            PickRandomPoints();
            
        }
        return points;
    }


    //Pick a random point
    private void PickRandomPoints(){
        chosenPoints.Clear();
        points.Clear();
        float cellSize = LevelGrid.Instance.gridSystem.GetCellSize(); 
        Vector3 parentPosition = LevelGrid.Instance.gridSystem.GetStartingPosition();

        for(int i = 0; i < 4; i++){
            //Needs to be 0 + starting position 
            int x = Random.Range(0, LevelGrid.Instance.gridSystem.GetWidth() - 7); //! Magic number - change later
            int y = Random.Range(0, LevelGrid.Instance.gridSystem.GetHeight() - 7);
            //Vector3 pointPosition = new Vector3((x * cellSize) + parentPosition.x, (y * cellSize) + parentPosition.y, 0);
            Vector3 pointPosition = new Vector3(x, y, 0);

            while(chosenPoints.Contains(pointPosition)){
                pointPosition.x = Random.Range(0, LevelGrid.Instance.gridSystem.GetWidth() - 7);
                pointPosition.y = Random.Range(0, LevelGrid.Instance.gridSystem.GetHeight() - 7);
            }
            
            //! Destroy these
            chosenPoints.Add(pointPosition);
            //pointMarkers[i].position = pointPosition;
            //points.Add(pointMarkers[i].position); //! Use for testing purposes

            points.Add(LevelGrid.Instance.gridSystem.TransposeWorldPositionToGridPosition(pointPosition));
            //points.Add(new Vector3((pointPosition.x * cellSize) + parentPosition.x, (pointPosition.y * cellSize) + parentPosition.y));
        }

        //! This is pretty bad
        FindAnglesBetweenAllPoints(0, 1, 3);
        FindAnglesBetweenAllPoints(1, 2, 0);
        FindAnglesBetweenAllPoints(2, 1, 3);
        FindAnglesBetweenAllPoints(3, 2, 0);
    }

    private void FindAnglesBetweenAllPoints(int startingPointPosition, int point1Position, int point2Position){
        Vector3 dir1 = points[point1Position] - points[startingPointPosition];
        Debug.DrawRay(points[startingPointPosition], dir1, Color.green, 1f); //! Debugging purposes

        Vector3 dir2 = points[point2Position] - points[startingPointPosition];
        Debug.DrawRay(points[startingPointPosition], dir2, Color.red, 1f); //! Debugging purposes

        // Find angle
        float dotProduct = Vector3.Dot(dir1, dir2);
        float stepOne = dotProduct / dir1.magnitude;
        float stepTwo = stepOne / dir2.magnitude;
        float angle = Mathf.Acos(stepTwo) * Mathf.Rad2Deg;

        totalAngles += angle;
    }

    
}
