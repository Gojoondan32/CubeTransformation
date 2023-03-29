using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CountDown : MonoBehaviour
{
    [SerializeField] private int ThisChamber;
    [SerializeField] private TextMeshProUGUI CountDowntxt;
    private bool canCount = true;

    private void Awake() {
        canCount = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            CurrentPosition.CurrentChamber = ThisChamber;
            canCount = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        canCount = false;
    }

    private void Update() {
        if(canCount){
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                LevelTimer.LevelTimersTra[ThisChamber - 1] -= Time.deltaTime;
                CountDowntxt.text = (Mathf.Round(LevelTimer.LevelTimersTra[ThisChamber - 1] * 100)/100).ToString();
            }
            if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                LevelTimer.LevelTimersRot[ThisChamber - 1] -= Time.deltaTime;
                CountDowntxt.text = (Mathf.Round(LevelTimer.LevelTimersRot[ThisChamber - 1] * 100) / 100).ToString();
            }
            if (SceneManager.GetActiveScene().buildIndex == 3)
            {
                LevelTimer.LevelTimersRef[ThisChamber - 1] -= Time.deltaTime;
                CountDowntxt.text = (Mathf.Round(LevelTimer.LevelTimersRef[ThisChamber - 1] * 100) / 100).ToString();
            }
        }
        
    }

    private void OnTriggerStay(Collider other)
    {
        
        
    }

}
