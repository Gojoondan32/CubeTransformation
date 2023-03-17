using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowHead : MonoBehaviour
{
    [SerializeField] GameObject VrHead;


    private void Update()
    {
        if (VrHead != null)
        {
            gameObject.transform.position = VrHead.transform.position;
        }
    }
}
