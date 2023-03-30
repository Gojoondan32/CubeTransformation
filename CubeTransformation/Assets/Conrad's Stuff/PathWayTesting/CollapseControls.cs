using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using OVR;

public class CollapseControls : MonoBehaviour
{
    [SerializeField]
    GameObject CollapseObj;
    [SerializeField]
    GameObject Explosion;
    [SerializeField]
    Transform TargetPoint;
    [SerializeField]
    WorldTransition worldTransition;
    private void FixedUpdate()
    {
        //RemainingTime = TimeText.

        // Check Player isn't in hub world
        if (CurrentPosition.CurrentChamber == gameObject.GetComponent<CountDown>().ThisChamber - 1)
        {
            // If CurrentRoom.TimeAmount is one of the following numbers 100, 70, 40 ,10 ,0 Then Activate Haptics and, Deadly Enviroments for those rooms
            switch (CurrentPosition.CurrentDimension)
            {
                case 1:
                    switch (LevelTimer.LevelTimersTra[CurrentPosition.CurrentChamber - 1])
                    {
                        case 250:
                            Instantiate(Explosion, TargetPoint.position,Quaternion.identity);
                            OVRInput.SetControllerVibration(70, 30);
                            break;
                        case 200:
                            Instantiate(Explosion, TargetPoint.position,Quaternion.identity);
                            OVRInput.SetControllerVibration(70, 30);
                            break;
                        case 100:
                            Instantiate(Explosion, TargetPoint.position,Quaternion.identity);
                            OVRInput.SetControllerVibration(70, 30);
                            break;
                        case 70:
                            Instantiate(Explosion, TargetPoint.position, Quaternion.identity);
                            OVRInput.SetControllerVibration(70, 30);
                            StartCoroutine(StopVibration());
                            break;
                        case 40:
                            Instantiate(Explosion, TargetPoint.position, Quaternion.identity);
                            OVRInput.SetControllerVibration(70, 70);
                            StartCoroutine(StopVibration());
                            break;
                        case 10:
                            Instantiate(Explosion, TargetPoint.position, Quaternion.identity);
                            OVRInput.SetControllerVibration(70, 70);
                            StartCoroutine(StopVibration());
                            break;
                        case 0:
                            Instantiate(Explosion, TargetPoint.position, Quaternion.identity);
                            OVRInput.SetControllerVibration(70, 70);
                            StartCoroutine(StopVibration());
                            StartCoroutine(GameOver());
                            break;
                    }
                    break;
                case 2:
                    switch (LevelTimer.LevelTimersRot[CurrentPosition.CurrentChamber - 1])
                    {
                        case 250:
                            Instantiate(Explosion, TargetPoint.position,Quaternion.identity);
                            OVRInput.SetControllerVibration(70, 30);
                            break;
                        case 200:
                            Instantiate(Explosion, TargetPoint.position,Quaternion.identity);
                            OVRInput.SetControllerVibration(70, 30);
                            break;
                        case 100:
                            Instantiate(Explosion, TargetPoint.position, Quaternion.identity);
                            OVRInput.SetControllerVibration(70, 30);
                            StartCoroutine(StopVibration());
                            break;
                        case 70:
                            Instantiate(Explosion, TargetPoint.position, Quaternion.identity);
                            OVRInput.SetControllerVibration(70, 30);
                            StartCoroutine(StopVibration());
                            break;
                        case 40:
                            Instantiate(Explosion, TargetPoint.position, Quaternion.identity);
                            OVRInput.SetControllerVibration(70, 70);
                            StartCoroutine(StopVibration());
                            break;
                        case 10:
                            Instantiate(Explosion, TargetPoint.position, Quaternion.identity);
                            OVRInput.SetControllerVibration(70, 70);
                            StartCoroutine(StopVibration());
                            break;
                        case 0:
                            Instantiate(Explosion, TargetPoint.position, Quaternion.identity);
                            OVRInput.SetControllerVibration(70, 120);
                            StartCoroutine(StopVibration());
                            StartCoroutine(GameOver());
                            break;
                    }
                    break;
                case 3:
                    switch (LevelTimer.LevelTimersRef[CurrentPosition.CurrentChamber - 1])
                    {
                        case 250:
                            Instantiate(Explosion, TargetPoint.position,Quaternion.identity);
                            OVRInput.SetControllerVibration(70, 30);
                            break;
                        case 200:
                            Instantiate(Explosion, TargetPoint.position,Quaternion.identity);
                            OVRInput.SetControllerVibration(70, 30);
                            break;
                        case 100:
                            Instantiate(CollapseObj, TargetPoint.position, Quaternion.identity);
                            OVRInput.SetControllerVibration(70, 30);
                            StartCoroutine(StopVibration());

                            break;
                        case 70:
                            Instantiate(CollapseObj, TargetPoint.position, Quaternion.identity);
                            OVRInput.SetControllerVibration(70, 30);
                            StartCoroutine(StopVibration());
                            break;
                        case 40:
                            Instantiate(CollapseObj, TargetPoint.position, Quaternion.identity);
                            OVRInput.SetControllerVibration(70, 70);
                            StartCoroutine(StopVibration());
                            break;
                        case 10:
                            Instantiate(CollapseObj, TargetPoint.position, Quaternion.identity);
                            OVRInput.SetControllerVibration(70, 70);
                            StartCoroutine(StopVibration());
                            break;
                        case 0:
                            Instantiate(CollapseObj, TargetPoint.position, Quaternion.identity);
                            OVRInput.SetControllerVibration(70, 120);
                            StartCoroutine(StopVibration());
                            StartCoroutine(GameOver());
                            break;
                    }

                    break;
                default:
                    Debug.Log(CurrentPosition.CurrentDimension);
                    break;
            }

        }
        // Check Players
         

    }

    IEnumerator StopVibration(float waitIme = 1f)
    {
        yield return new WaitForSeconds(waitIme);
        OVRInput.SetControllerVibration(0,0);
    }
    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(3f);
        worldTransition.Loadscene(0);
    }
}
