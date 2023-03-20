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

    [SerializeField] private List<Vector3> gridSpacePoints;
    
    [SerializeField] private PlayerInteraction playerInteraction;

    [SerializeField] private List<Vector3> testPlayerPoints;
    private void Awake() {
        gridSpacePoints = new List<Vector3>();
    }

    public void CreateShape(){
        gridSpacePoints = randomisedShapes.StartDrawingShape();
        GenerateLines(lineRenderer, gridSpacePoints, true);
    }
    public void MoveShape(){
        gridSpacePoints = translateShape.RandomlyMoveShape(gridSpacePoints);
        GenerateLines(lineRenderer, gridSpacePoints, true);
    }
    public void CreateReflectionTest(){
        if(gridSpacePoints.Count <= 0){
            Debug.LogError("Shape points is empty, Call CreateShape before this method");
            return;
        }
        reflectionTest.CreateReflectionQuestion(gridSpacePoints);
    }

    [ContextMenu("CreateReflectionQuestion")]
    public void CreateReflectionQuestion(){
        CreateShape();
        MoveShape();
        CreateReflectionTest();
    }

    [ContextMenu("SumbitPlayerReflection")]
    public void SubmitPlayerReflection(){
        if(reflectionTest.EvaluateReflection(playerInteraction.GetPlayerPoints(), gridSpacePoints)){
            Debug.Log("PLAYER HAS WON");
        }
    }

    [ContextMenu("Create Translation Question")]
    public void CreateTranslationQuestion(){
        reflectionTest.RemoveReflectionLines();
        CreateShape();
        MoveShape();
        (int x, int y) = translateShape.CreateTranslationQuestion(gridSpacePoints);
        Debug.Log(x);
        Debug.Log(y);
    }
    
    [ContextMenu("Sumbit Player Translation")]
    public void SumbitTranslationQuestion(){
        if(translateShape.EvalutateTranslation(playerInteraction.GetPlayerPoints(), gridSpacePoints)){
            Debug.Log("PLAYER HAS WON WITH TRANSLATION");
        }
    }

    public void CreateRotationQuestion(){
        CreateShape();
        MoveShape();
    }

    public void TestingInteractable(){
        Debug.Log("INTERACTABLE BUTTON IS WORKING");
    }

    public static void GenerateLines(LineRenderer lineRenderer, List<Vector3> points, bool connectFinalToFirst = false){
        for (int i = 0; i < points.Count; i++){
            lineRenderer.SetPosition(i, new Vector3(points[i].x, points[i].y, LevelGrid.Instance.Marker.position.z - 0.01f));
        }

        if(connectFinalToFirst){
            lineRenderer.positionCount = points.Count + 1; // Don't know if this is needed
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, new Vector3(points[0].x, points[0].y, LevelGrid.Instance.Marker.position.z - 0.01f));
        }
        
    }
}
