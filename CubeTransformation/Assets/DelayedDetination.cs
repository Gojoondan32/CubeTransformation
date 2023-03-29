using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedDetination : MonoBehaviour
{
    [SerializeField] GameObject[] ListofChildren;

    private void Awake()
    {
        StartCoroutine(ChainExplosion());
    }

    IEnumerator ChainExplosion()
    {
        foreach (GameObject child in ListofChildren)
        {
            child.SetActive(true);
            
            yield return new WaitForSeconds(1.2f);
        }
    }
}


