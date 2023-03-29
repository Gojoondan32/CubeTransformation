using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using OVR;

public class CollapseControls : MonoBehaviour
{
    GameObject CollapseObj;
    GameObject Explosion;
    private float RemainingTime;
    private TextMeshProUGUI TimeText;
    private void FixedUpdate()
    {
        //RemainingTime = TimeText.

        // Check Player isn't in hub world
        if (CurrentPosition.CurrentChamber != 0)
        {
            // If CurrentRoom.TimeAmount is one of the following numbers 100, 70, 40 ,10 ,0 Then Activate Haptics and, Deadly Enviroments for those rooms
            switch (CurrentPosition.CurrentDimension)
            {
                case 1:
                    switch (LevelTimer.LevelTimersTra[CurrentPosition.CurrentChamber])
                    {
                        case 100:
                            Instantiate(Explosion);
                            OVRInput.SetControllerVibration(70, 30);
                            break;
                        case 70:
                            Instantiate(Explosion);
                            OVRInput.SetControllerVibration(70, 30);
                            StartCoroutine(StopVibration());
                            break;
                        case 40:
                            Instantiate(Explosion);
                            OVRInput.SetControllerVibration(70, 70);
                            StartCoroutine(StopVibration());
                            break;
                        case 10:
                            Instantiate(Explosion);
                            OVRInput.SetControllerVibration(70, 70);
                            StartCoroutine(StopVibration());
                            break;
                        case 0:
                            Instantiate(Explosion);
                            OVRInput.SetControllerVibration(70, 70);
                            StartCoroutine(StopVibration());
                            break;
                    }
                    break;
                case 2:
                    switch (LevelTimer.LevelTimersRot[CurrentPosition.CurrentChamber])
                    {
                        case 100:
                            Instantiate(Explosion);
                            OVRInput.SetControllerVibration(70, 30);
                            StartCoroutine(StopVibration());
                            break;
                        case 70:
                            Instantiate(Explosion);
                            OVRInput.SetControllerVibration(70, 30);
                            StartCoroutine(StopVibration());
                            break;
                        case 40:
                            Instantiate(Explosion);
                            OVRInput.SetControllerVibration(70, 70);
                            StartCoroutine(StopVibration());
                            break;
                        case 10:
                            Instantiate(Explosion);
                            OVRInput.SetControllerVibration(70, 70);
                            StartCoroutine(StopVibration());
                            break;
                        case 0:
                            Instantiate(Explosion);
                            OVRInput.SetControllerVibration(70, 120);
                            StartCoroutine(StopVibration());
                            break;
                    }
                    break;
                case 3:
                    switch (LevelTimer.LevelTimersRef[CurrentPosition.CurrentChamber])
                    {
                        case 100:
                            Instantiate(CollapseObj);
                            OVRInput.SetControllerVibration(70, 30);
                            StartCoroutine(StopVibration());

                            break;
                        case 70:
                            Instantiate(CollapseObj);
                            OVRInput.SetControllerVibration(70, 30);
                            StartCoroutine(StopVibration());
                            break;
                        case 40:
                            Instantiate(CollapseObj);
                            OVRInput.SetControllerVibration(70, 70);
                            StartCoroutine(StopVibration());
                            break;
                        case 10:
                            Instantiate(CollapseObj);
                            OVRInput.SetControllerVibration(70, 70);
                            StartCoroutine(StopVibration());
                            break;
                        case 0:
                            Instantiate(CollapseObj);
                            OVRInput.SetControllerVibration(70, 120);
                            StartCoroutine(StopVibration());
                            break;
                    }

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
}
