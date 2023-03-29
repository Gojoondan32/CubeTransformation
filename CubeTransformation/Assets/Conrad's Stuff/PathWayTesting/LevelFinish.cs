using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFinish : MonoBehaviour
{
    [SerializeField]
    private WorldTransition worldTransition;
    [SerializeField]
    private int CurrentWorld;
    [SerializeField]
    private GameObject TeleParticle;

    private void Awake()
    {
        TeleParticle.SetActive(false);
    }

    private void FixedUpdate()
    {
        LevelComplete();
    }

    private void LevelComplete()
    {
        
        if (CurrentPosition.CurrentChamber == 3 && LevelTimer.ChambersComplete[CurrentWorld] == true)
        {
            CurrentPosition.CurrentChamber = 0;
            HandleSerialisation.Instance.RoomComplete();
            StartCoroutine(Teleport());
            
        }
        
        
    }

    private IEnumerator Teleport()
    {
        if (TeleParticle != null)
        {
            TeleParticle.SetActive(true);
            yield return new WaitForSeconds(2f);
            TeleParticle.SetActive(false);
        }
       worldTransition.Loadscene(0);

    }
}
