using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class Controlablecart : MonoBehaviour
{
    [SerializeField] private float KartProgress;
    [SerializeField] private EndOfPathInstruction endOfPathInstruction;
    public PathCreator[] pathCreators;
    [SerializeField] private int chosenPath = -1;
    public bool InMovement;
    public AnimationCurve curve;
    public int ChosenPath
    {
        get
        {
            return chosenPath;
        }
        set
        {
            chosenPath = value;
        }
    }
    [SerializeField]private float[] pathLengths;

    public float[] PathLengths
    {
        get
        {
            return pathLengths;
        }
        set
        {
            pathLengths = value;
        }
    }
    public int branchSelected;
    private void Update()
    {
        if (ChosenPath != -1)
        {
            KartProgress = Mathf.Clamp(KartProgress, 0, PathLengths[ChosenPath]);
            
            
        }
        if (InMovement == true)
        {
            transform.position = pathCreators[ChosenPath].path.GetPointAtDistance(KartProgress, endOfPathInstruction);
            transform.rotation = pathCreators[ChosenPath].path.GetRotationAtDistance(KartProgress, endOfPathInstruction);
        }

        
    }

    private void Awake()
    {
        //StartCoroutine(BackAndForth());
    }
    public void AllAboard(int chosenTrack,float StartValue, float EndValue)
    {
        //Debug.Log("All Aboard!");
        if (InMovement == false)
        {
            ChosenPath = chosenTrack;
            StartCoroutine(Trainsition(StartValue, EndValue));
        }
        
    }

    IEnumerator Trainsition(float StartValue, float EndValue)
    {
        InMovement = true;
        for (float t = 0; t < 1; t += 0.005f)
        {
            var easing = curve.Evaluate(t);
            KartProgress = Mathf.Lerp(StartValue, EndValue, easing);
            yield return new WaitForSeconds(0.01f);
        }
        InMovement = false;
        if (EndValue == 0)
        {
            ChosenPath = -1;
        }
    }

}
