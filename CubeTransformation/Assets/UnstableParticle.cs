using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnstableParticle : MonoBehaviour
{
    [SerializeField]
    private float TimeTillDestruction;
    [SerializeField]
    GameObject Target;
    private void Awake()
    {
        StartCoroutine(Destruction());
    }

    private IEnumerator Destruction()
    {
        yield return new WaitForSeconds(TimeTillDestruction);
        Destroy(Target);
    }
}
