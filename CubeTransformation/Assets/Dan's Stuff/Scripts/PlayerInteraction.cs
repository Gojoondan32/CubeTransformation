using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private Transform indicator;
    [SerializeField] private Transform testObject;

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = indicator.position - transform.position;
        if(Physics.Raycast(transform.position, dir, out RaycastHit hit, float.MaxValue, LayerMask.GetMask("GridObject"))){
            Debug.Log("Hit Object");
            testObject.position = hit.point;
        }
    }
}
