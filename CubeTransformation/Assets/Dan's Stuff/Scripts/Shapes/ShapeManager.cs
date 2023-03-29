using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShapeManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI textXandY; 
    [SerializeField] private RandomisedShapes randomisedShapes;
    [SerializeField] private TranslateShape translateShape;
    [SerializeField] private ReflectionTest reflectionTest; //! Testing
    [SerializeField] private RotateShape rotateShape;
    [SerializeField] private LineRenderer lineRenderer;

    [SerializeField] private List<Vector3> gridSpacePoints;
    private List<Transform> gridSpaceVisuals;
    [SerializeField] private Transform gridSpacePointVisualPrefab;
    
    [SerializeField] private PlayerInteraction playerInteraction;

    [SerializeField] private List<Vector3> testPlayerPoints;
    [SerializeField] private int correctAnswers = 0;


    private void Awake() {
        gridSpacePoints = new List<Vector3>();
        gridSpaceVisuals = new List<Transform>();
        correctAnswers = 0;
        for(int i = 0; i < 4; i++){
            Transform temp = Instantiate(gridSpacePointVisualPrefab, new Vector3(0, -100f, 0), Quaternion.identity);
            gridSpaceVisuals.Add(temp);
        }
    }

    public void CreateShape(){
        gridSpacePoints = randomisedShapes.StartDrawingShape();
        GenerateLines(lineRenderer, gridSpacePoints, true);
        MoveVisuals();
    }
    public void MoveShape(){
        gridSpacePoints = translateShape.RandomlyMoveShape(gridSpacePoints);
        GenerateLines(lineRenderer, gridSpacePoints, true);
        MoveVisuals();
    }

    #region Reflection

    [ContextMenu("CreateReflectionQuestion")]
    public void CreateReflectionQuestion(){
        CreateShape();
        MoveShape();
        CreateReflectionTest();
    }
    public void CreateReflectionTest(){
        if(gridSpacePoints.Count <= 0){
            Debug.LogError("Shape points is empty, Call CreateShape before this method");
            return;
        }
        reflectionTest.CreateReflectionQuestion(gridSpacePoints);
    }

    [ContextMenu("SumbitPlayerReflection")]
    public void SubmitPlayerReflection(){
        if(reflectionTest.EvaluateReflection(playerInteraction.GetPlayerPoints(), gridSpacePoints)){
            Debug.Log("PLAYER HAS WON");
            correctAnswers++;
            //HandleSerialisation.Instance.CreateReflectionData(playerInteraction.GetPlayerPoints(), gridSpacePoints, reflectionTest.ReflectionPoints, true);
            QuestionManager.Instance.MoveGridToNextPosition();
        }
        else{
            // Upload the incorrect answer
            //HandleSerialisation.Instance.CreateReflectionData(playerInteraction.GetPlayerPoints(), gridSpacePoints, reflectionTest.ReflectionPoints, false);
        }
    }
    #endregion

    #region Translation

    private (int x, int y) translation;

    [ContextMenu("Create Translation Question")]
    public void CreateTranslationQuestion(){
        reflectionTest.RemoveReflectionLines();
        CreateShape();
        MoveShape();
        translation = translateShape.CreateTranslationQuestion(gridSpacePoints);
        textXandY.text = $"X: {translation.x}, Y: {translation.y}";
    }
    
    [ContextMenu("Sumbit Player Translation")]
    public void SumbitTranslationQuestion(){
        if(translateShape.EvalutateTranslation(playerInteraction.GetPlayerPoints(), gridSpacePoints)){
            Debug.Log("PLAYER HAS WON WITH TRANSLATION");
            correctAnswers++;
            Vector2 translationVector = new Vector2(translation.x, translation.y);
            HandleSerialisation.Instance.CreateTranslationData(playerInteraction.GetPlayerPoints(), gridSpacePoints, translationVector, true);
        }
        else{
            // Upload the incorrect answer
            Vector2 translationVector = new Vector2(translation.x, translation.y);
            HandleSerialisation.Instance.CreateTranslationData(playerInteraction.GetPlayerPoints(), gridSpacePoints, translationVector, false);
        }
    }
    #endregion

    #region Rotation

    (Vector2 rotationPoint, int rotationAmount) rotationQuestion;
    [ContextMenu("Create Rotation Question")]
    public void CreateRotationQuestion(){
        reflectionTest.RemoveReflectionLines(); // Testing
        CreateShape();
        MoveShape();
        rotationQuestion = rotateShape.CreateRotationQuestion(gridSpacePoints);
        textXandY.text = $"x: {rotationQuestion.rotationPoint.x} y: {rotationQuestion.rotationPoint.y} rotation: {rotationQuestion.rotationAmount}";

    }
    [ContextMenu("Sumbit Player Rotation")]
    public void SumbitRotationQuestion(){
        if(rotateShape.EvaluateRotation(playerInteraction.GetPlayerPoints(), gridSpacePoints)){
            Debug.Log("PLAYER HAS WON WITH ROTATION");
            correctAnswers++;
            HandleSerialisation.Instance.CreateRotationData(playerInteraction.GetPlayerPoints(), gridSpacePoints, rotationQuestion.rotationPoint, true);
        }
        else{
            // Upload the incorrect answer
            HandleSerialisation.Instance.CreateRotationData(playerInteraction.GetPlayerPoints(), gridSpacePoints, rotationQuestion.rotationPoint, false);
        }
    }

    #endregion

    public bool CheckIfPlayerHasWon(){
        if(correctAnswers >= 3) return true;
        return false;
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
    public void MoveVisuals(){
        for(int i = 0; i < gridSpaceVisuals.Count; i++){
            Vector3 pos = gridSpacePoints[i];
            pos.z = LevelGrid.Instance.Marker.position.z;
            gridSpaceVisuals[i].position = pos;
        }
    }
}
