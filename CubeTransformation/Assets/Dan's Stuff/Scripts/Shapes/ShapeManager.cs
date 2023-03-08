using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RandomisedShapes randomisedShapes;
    [SerializeField] private TranslateShape translateShape;
    [SerializeField] private ReflectionTest reflectionTest; //! Testing
    [SerializeField] private LineRenderer lineRenderer;

    [SerializeField] private List<Vector3> transposedPoints;
    
    [SerializeField] private PlayerInteraction playerInteraction;

    [SerializeField] private List<Vector3> testPlayerPoints;
    private void Awake() {
        transposedPoints = new List<Vector3>();
    }

    public void CreateShape(){
        transposedPoints = randomisedShapes.StartDrawingShape();
        GenerateLines(transposedPoints, true);
    }
    public void MoveShape(){
        transposedPoints = translateShape.RandomlyMoveShape(transposedPoints);
        GenerateLines(transposedPoints, true);
    }
    public void CreateReflectionTest(){
        if(transposedPoints.Count <= 0){
            Debug.LogError("Shape points is empty, Call CreateShape before this method");
            return;
        }
        reflectionTest.CreateReflectionQuestion(transposedPoints);
    }

    [ContextMenu("CreateReflectionQuestion")]
    public void CreateReflectionQuestion(){
        CreateShape();
        MoveShape();
        CreateReflectionTest();
    }

    [ContextMenu("SumbitPlayerReflection")]
    public void SubmitPlayerReflection(){
        if(reflectionTest.EvaluateReflection(playerInteraction.PlayerPositions, transposedPoints)){
            Debug.Log("PLAYER HAS WON");
        }
    }

    public void TestingInteractable(){
        Debug.Log("INTERACTABLE BUTTON IS WORKING");
    }

    private void GenerateLines(List<Vector3> points, bool connectFinalToFirst = false){
        for (int i = 0; i < points.Count; i++){
            lineRenderer.SetPosition(i, new Vector3(points[i].x, points[i].y, 5));
        }

        if(connectFinalToFirst){
            lineRenderer.positionCount = points.Count + 1; // Don't know if this is needed
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, new Vector3(points[0].x, points[0].y, 5));
        }
        
    }
}
