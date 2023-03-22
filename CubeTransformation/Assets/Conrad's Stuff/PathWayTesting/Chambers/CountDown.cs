using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CountDown : MonoBehaviour
{
    [SerializeField] private int ThisChamber;
    [SerializeField] private TextMeshProUGUI CountDowntxt;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            CurrentPosition.CurrentChamber = ThisChamber;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            CurrentPosition.CurrentChamber = 0;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            LevelTimer.LevelTimersTra[ThisChamber] -= Time.deltaTime;
            CountDowntxt.text = LevelTimer.LevelTimersTra[ThisChamber].ToString();
        }
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            LevelTimer.LevelTimersRot[ThisChamber] -= Time.deltaTime;
            CountDowntxt.text = LevelTimer.LevelTimersRot[ThisChamber].ToString();
        }
        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            LevelTimer.LevelTimersRef[ThisChamber] -= Time.deltaTime;
            CountDowntxt.text = LevelTimer.LevelTimersRef[ThisChamber].ToString();
        }
        
    }

}
