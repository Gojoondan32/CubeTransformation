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
    private IEnumerator coroutineRunning;
    private void FixedUpdate()
    {
        //RemainingTime = TimeText.

        // Check Player isn't in hub world
        
        if (CurrentPosition.CurrentChamber == gameObject.GetComponent<CountDown>().ThisChamber - 1)
        {
            Debug.Log(CurrentPosition.CurrentChamber);
            Debug.Log(gameObject.GetComponent<CountDown>().ThisChamber - 1);
            //Debug.Log(LevelTimer.LevelTimersTra[CurrentPosition.CurrentChamber - 1]);
            //Debug.Log(LevelTimer.LevelTimersRot[CurrentPosition.CurrentChamber - 1]);
            //Debug.Log(LevelTimer.LevelTimersRef[CurrentPosition.CurrentChamber - 1]);
            // If CurrentRoom.TimeAmount is one of the following numbers 100, 70, 40 ,10 ,0 Then Activate Haptics and, Deadly Enviroments for those rooms
            switch (CurrentPosition.CurrentDimension)
            {
                case 1:
                    switch (Mathf.Round(LevelTimer.LevelTimersTra[CurrentPosition.CurrentChamber - 1]))
                    {
                        case 290:
                            Debug.Log("Institating 1");
                            if(coroutineRunning == null){
                                StartCoroutine(SpawnRock());
                            }
                            
                            OVRInput.SetControllerVibration(0, 30);
                            StartCoroutine(StopVibration());
                            break;
                        case 200:
                            if(coroutineRunning == null){
                                StartCoroutine(SpawnRock());
                            }
                            OVRInput.SetControllerVibration(0, 30);
                            StartCoroutine(StopVibration());
                            break;
                        case 100:
                            if(coroutineRunning == null){
                                StartCoroutine(SpawnRock());
                            }
                            OVRInput.SetControllerVibration(0, 30);
                            StartCoroutine(StopVibration());
                            break;
                        case 70:
                            if(coroutineRunning == null){
                                StartCoroutine(SpawnRock());
                            }
                            OVRInput.SetControllerVibration(0, 30);
                            StartCoroutine(StopVibration());
                            break;
                        case 40:
                            if(coroutineRunning == null){
                                StartCoroutine(SpawnRock());
                            }
                            OVRInput.SetControllerVibration(0, 70);
                            StartCoroutine(StopVibration());
                            break;
                        case 10:
                            if(coroutineRunning == null){
                                StartCoroutine(SpawnRock());
                            }
                            OVRInput.SetControllerVibration(0, 70);
                            StartCoroutine(StopVibration());
                            break;
                        case 0:
                            if(coroutineRunning == null){
                                StartCoroutine(SpawnRock());
                            }
                            OVRInput.SetControllerVibration(0, 70);
                            StartCoroutine(StopVibration());
                            StartCoroutine(GameOver());
                            break;
                        default:
                            //Debug.Log(LevelTimer.LevelTimersTra[CurrentPosition.CurrentChamber - 1]);
                            break;
                    }
                    break;
                case 2:
                    switch (Mathf.Round(LevelTimer.LevelTimersRot[CurrentPosition.CurrentChamber - 1]))
                    {
                        case 290:
                            Debug.Log("Institating 2");
                            if(coroutineRunning == null){
                                StartCoroutine(SpawnRock());
                            }
                            OVRInput.SetControllerVibration(0, 30);
                            StartCoroutine(StopVibration());
                            break;
                        case 200:
                            if(coroutineRunning == null){
                                StartCoroutine(SpawnRock());
                            }
                            OVRInput.SetControllerVibration(0, 30);
                            StartCoroutine(StopVibration());
                            break;
                        case 100:
                            if(coroutineRunning == null){
                                StartCoroutine(SpawnRock());
                            }
                            OVRInput.SetControllerVibration(0, 30);
                            StartCoroutine(StopVibration());
                            break;
                        case 70:
                            if(coroutineRunning == null){
                                StartCoroutine(SpawnRock());
                            }
                            OVRInput.SetControllerVibration(0, 30);
                            StartCoroutine(StopVibration());
                            break;
                        case 40:
                            if(coroutineRunning == null){
                                StartCoroutine(SpawnRock());
                            }
                            OVRInput.SetControllerVibration(0, 70);
                            StartCoroutine(StopVibration());
                            break;
                        case 10:
                            if(coroutineRunning == null){
                                StartCoroutine(SpawnRock());
                            }
                            OVRInput.SetControllerVibration(0, 70);
                            StartCoroutine(StopVibration());
                            break;
                        case 0:
                            if(coroutineRunning == null){
                                StartCoroutine(SpawnRock());
                            }
                            OVRInput.SetControllerVibration(0, 120);
                            StartCoroutine(StopVibration());
                            StartCoroutine(GameOver());
                            break;
                        default:
                            Debug.Log(LevelTimer.LevelTimersRot[CurrentPosition.CurrentChamber - 1]);
                            break;
                    }
                    break;
                case 3:
                    switch (Mathf.Round(LevelTimer.LevelTimersRef[CurrentPosition.CurrentChamber - 1]))
                    {
                        case 290:
                            Debug.Log("Institating 3");
                            if(coroutineRunning == null){
                                StartCoroutine(ChangeObject());
                            }
                            OVRInput.SetControllerVibration(70, 30);
                            StartCoroutine(StopVibration());
                            break;
                        case 200:
                            if(coroutineRunning == null){
                                StartCoroutine(ChangeObject());
                            }
                            OVRInput.SetControllerVibration(70, 30);
                            StartCoroutine(StopVibration());
                            break;
                        case 100:
                            if(coroutineRunning == null){
                                StartCoroutine(ChangeObject());
                            }
                            OVRInput.SetControllerVibration(70, 30);
                            StartCoroutine(StopVibration());

                            break;
                        case 70:
                            if(coroutineRunning == null){
                                StartCoroutine(ChangeObject());
                            }
                            OVRInput.SetControllerVibration(70, 30);
                            StartCoroutine(StopVibration());
                            break;
                        case 40:
                            if(coroutineRunning == null){
                                StartCoroutine(ChangeObject());
                            }
                            OVRInput.SetControllerVibration(70, 70);
                            StartCoroutine(StopVibration());
                            break;
                        case 10:
                            if(coroutineRunning == null){
                                StartCoroutine(ChangeObject());
                            }
                            OVRInput.SetControllerVibration(70, 70);
                            StartCoroutine(StopVibration());
                            break;
                        case 0:
                            if(coroutineRunning == null){
                                StartCoroutine(ChangeObject());
                            }
                            OVRInput.SetControllerVibration(70, 120);
                            StartCoroutine(StopVibration());
                            StartCoroutine(GameOver());
                            break;
                    }

                    break;
                default:
                    Debug.Log(Mathf.Round(LevelTimer.LevelTimersRef[CurrentPosition.CurrentChamber - 1]));
                    break;
            }

        }
        // Check Players
         

    }

    private IEnumerator SpawnRock(){
        coroutineRunning = SpawnRock();
        yield return new WaitForSeconds(1f);
        Instantiate(Explosion, TargetPoint.position,Quaternion.identity);
        coroutineRunning = null;
    }

    private IEnumerator ChangeObject(){
    coroutineRunning = ChangeObject();
    yield return new WaitForSeconds(1f);
    Instantiate(CollapseObj, TargetPoint.position,Quaternion.identity);
    coroutineRunning = null;
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
