using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSumbit : MonoBehaviour
{
    [SerializeField] private ShapeManager shapeManager;
     
    public void SubmitAnswer(int dimensionNumber){
        switch(dimensionNumber){
            case 1:
                shapeManager.SumbitTranslationQuestion();
                break;
            case 2:
                shapeManager.SubmitPlayerReflection();
                break;

        }
    }
}
