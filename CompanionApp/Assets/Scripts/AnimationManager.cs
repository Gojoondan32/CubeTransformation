using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ModularMotion;

public class AnimationManager : MonoBehaviour
{
    [SerializeField] private UIMotion uIMotion;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void PlayMotion(){
        uIMotion.Play();
    }
}
