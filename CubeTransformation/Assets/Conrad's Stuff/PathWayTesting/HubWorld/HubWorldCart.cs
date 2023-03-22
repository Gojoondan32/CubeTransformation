using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class HubWorldCart : MonoBehaviour
{
    [SerializeField] private int chosenPath = -1;
    [SerializeField]private float KartProgress;
    [SerializeField] private EndOfPathInstruction endOfPathInstruction;
    public PathCreator[] pathCreators;
    public bool InMovement;
    public AnimationCurve curve;
    private float x;
    [SerializeField] WorldTransition worldTransition;

    public void GoToRot()
    {
        Vector3 StartRot = new Vector3(0, gameObject.transform.rotation.eulerAngles.y + 180, 0);
        Vector3 FinRot = new Vector3(gameObject.transform.rotation.eulerAngles.y, (pathCreators[1].gameObject.transform.rotation.eulerAngles.y), 1);
        //Go To Rotation Dimension
        StartCoroutine(MineCartMove(StartRot, FinRot, 1));
    }
    public void GoToTra()
    {
        Vector3 StartRot = new Vector3(0, gameObject.transform.rotation.eulerAngles.y + 180, 0);
        Vector3 FinRot = new Vector3(gameObject.transform.rotation.eulerAngles.y, (pathCreators[0].gameObject.transform.rotation.eulerAngles.y), 0);
        //Go To Translation Dimension
        StartCoroutine(MineCartMove(StartRot, FinRot, 0));
    }
    public void GoToRef()
    {
        Vector3 StartRot = new Vector3(0, gameObject.transform.rotation.eulerAngles.y + 180, 0);
        Vector3 FinRot = new Vector3(gameObject.transform.rotation.eulerAngles.y, (pathCreators[2].gameObject.transform.rotation.eulerAngles.y), 2);
        //Go To Reflection Dimension
        StartCoroutine(MineCartMove(StartRot, FinRot, 2));

    }

    IEnumerator MineCartMove(Vector3 StartRotation, Vector3 FinishRotation, int RailofChoice)
    {
        InMovement = true;
        if (RailofChoice <= 3 && RailofChoice >= 0)
        {
            // Phase 1 Turning
            x = 0;
            while ((gameObject.transform.rotation.eulerAngles.y + 180) % 360 != FinishRotation.y)
            {
                x += Time.deltaTime / 10;
                x %= 360;
                float y = Mathf.Lerp(StartRotation.y, FinishRotation.y, x);
                float z = (y + 180) % 360;
                gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, z, 0));
                yield return null;
                Debug.Log("Turning");
                Debug.Log(FinishRotation.y);
                Debug.Log((gameObject.transform.rotation.eulerAngles.y + 180) % 360);
                Debug.Log((gameObject.transform.rotation.eulerAngles.y + 180) % 360 != FinishRotation.y);
            }
            Debug.Log("Turn Complete");
            // Phase 2 Coupling

            chosenPath = RailofChoice;
            KartProgress = 0.01f;
            transform.position = pathCreators[chosenPath].path.GetPointAtDistance(KartProgress, endOfPathInstruction);
            transform.rotation = pathCreators[chosenPath].path.GetRotationAtDistance(KartProgress, endOfPathInstruction);
            Debug.Log("RailofChoice Selected");

            yield return new WaitForSeconds(1f);

            // Phase 3
            if (chosenPath != -1)
            {
                AllAboard(0, 25);
                
            }

        }
        else
        {
            chosenPath = -1;
            Debug.Log("Non Valid Path");
        }
        InMovement = false;
    }

    private void FixedUpdate()
    {
        if (chosenPath != -1 && chosenPath < pathCreators.Length)
        {
            transform.position = pathCreators[chosenPath].path.GetPointAtDistance(KartProgress, endOfPathInstruction);
            transform.rotation = pathCreators[chosenPath].path.GetRotationAtDistance(KartProgress, endOfPathInstruction);
        }
    }

    public void AllAboard(float StartValue, float EndValue)
    {
        Debug.Log("All Aboard!");
        StartCoroutine(Trainsition(StartValue, EndValue));
        

    }

    IEnumerator Trainsition(float StartValue, float EndValue)
    {
        for (float t = 0.005f; t < 1; t += 0.005f)
        {
            var easing = curve.Evaluate(t);
            KartProgress = Mathf.Lerp(StartValue, EndValue, easing);
            yield return new WaitForSeconds(0.01f);
        }
        EnterChamber();
    }

    public void EnterChamber()
    {
        StartCoroutine(TransitionEffect());

    }

    IEnumerator TransitionEffect()
    {
        yield return null;
        worldTransition.Loadscene(chosenPath + 1);
        
    }
}
