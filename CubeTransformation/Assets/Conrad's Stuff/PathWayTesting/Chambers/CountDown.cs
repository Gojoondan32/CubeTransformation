using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountDown : MonoBehaviour
{
    [SerializeField] private int Path;
    [SerializeField] private int Stage;
    [SerializeField] private TextMeshProUGUI CountDowntxt;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Player Located");
            int SearchCode = (Path + 1) * (Stage + 1);
            if (LevelTimer.LevelTimers[SearchCode] != 0)
            {
                LevelTimer.LevelTimers[SearchCode] -= Time.deltaTime;
                CountDowntxt.text = LevelTimer.LevelTimers[SearchCode].ToString();
                Debug.Log("TickTock");
            }
        }
    }
    
}
