using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using UnityEngine.UI;

public class ChamberCartMovement : MonoBehaviour
{
    public GameObject ForwardButton;
    [SerializeField] PathCreator path;
    [SerializeField] GameObject Kart;
    [SerializeField] float KartProgress = 0.1f;
    [SerializeField] EndOfPathInstruction EndOfPathInstruction;
    public AnimationCurve curve;
    private void FixedUpdate()
    {
        Kart.transform.position = path.path.GetPointAtDistance(KartProgress, EndOfPathInstruction);
        Kart.transform.rotation = path.path.GetRotationAtDistance(KartProgress, EndOfPathInstruction);
    }



    public IEnumerator Transition(float NewPosition)
    {
        ForwardButton.GetComponent<Button>().interactable = false;
        float StartValue = KartProgress;
        for (float t = 0.005f; t < 1; t += 0.005f)
        {
            var easing = curve.Evaluate(t);
            KartProgress = Mathf.Lerp(StartValue, NewPosition, easing);
            yield return new WaitForSeconds(0.01f);
        }
        ForwardButton.GetComponent<Button>().interactable = true;
    }
}
