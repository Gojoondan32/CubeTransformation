using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RubbleNPathWay : MonoBehaviour
{
    [SerializeField]
    private GameObject[] RubblePiles;
    [SerializeField]
    private GameObject[] Buttons;

    private void Awake()
    {
        foreach(GameObject piles in RubblePiles)
        {
            piles.SetActive(false);
        }
        
            for (int x = 0; x < RubblePiles.Length;x++)
            {
                if (LevelTimer.ChambersComplete[x] == false)
                {
                    RubblePiles[x].SetActive(true);
                    Buttons[x].GetComponent<Button>().interactable = false;
                
                }
            }
        
    }

    
}
