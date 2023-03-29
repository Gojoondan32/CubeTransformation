using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestionManager : MonoBehaviour
{
    public static QuestionManager Instance { get; private set; }
    [SerializeField] private ShapeManager shapeManager;
    [SerializeField] private PlayerInteraction playerInteraction;
    [SerializeField] private ChamberUIBtn chamberUIBtn;
    [SerializeField] private Transform[] questionMarkers;

    public void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start() {
        chamberUIBtn.GetForwardButton().interactable = true; // Allow the player to move to the next question
        MoveGridToNextPosition();
    }

    public void MoveGridToNextPosition()
    {
        if(shapeManager.CheckIfPlayerHasWon()){
            Debug.Log("Player has won");
            // TODO: This is where we would load the next scene 
            LevelTimer.ChambersComplete[SceneManager.GetActiveScene().buildIndex - 1] = true; // Setting the current chamber to true
            return;
        }
        //LevelGrid.Instance.MoveGrid(questionMarkers[2].position); //! Testing
        LevelGrid.Instance.MoveGrid(questionMarkers[CurrentPosition.CurrentChamber].position);
        playerInteraction.DestroyPlayerPoints(); // Allow the player to move the shape again
        CreateQuestion();
        chamberUIBtn.GetForwardButton().interactable = true; // Allow the player to move to the next question
    }

    private void CreateQuestion(){
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        Debug.Log(sceneIndex);
        switch(sceneIndex){
            case 1:
                Debug.Log("Creating Translation Question");
                shapeManager.CreateTranslationQuestion();
                break;
            case 2:
                Debug.Log("Creating Rotation Question");
                shapeManager.CreateRotationQuestion();
                break;
            case 3:
                Debug.Log("Creating Reflection Question");
                shapeManager.CreateReflectionQuestion();
                break;
            default:
                Debug.Log("No question created");
                break;
        }
    }


}