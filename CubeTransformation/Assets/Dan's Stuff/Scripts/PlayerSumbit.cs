using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSumbit : MonoBehaviour
{
    [SerializeField] private ShapeManager shapeManager;
    private float cooldown = 0.25f;
    private bool canSubmit = true;
    public void SubmitAnswer(){
        if(!canSubmit || shapeManager == null) return; // Stop the player from spamming the button
        return; //! Testing 
        StartCoroutine(Cooldown());

        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        switch(sceneIndex){
            case 1:
                shapeManager.SumbitTranslationQuestion();
                break;
            case 2:
                shapeManager.SubmitPlayerReflection();
                break;
            case 3:
                shapeManager.SumbitRotationQuestion();
                break;
            case 4:
                shapeManager.SumbitRotationQuestion(); //! This is my demo scene for the reflection test 
                break;
        }
    }

    private IEnumerator Cooldown(){
        canSubmit = false;
        yield return new WaitForSeconds(cooldown);
        canSubmit = true;
    }
}
