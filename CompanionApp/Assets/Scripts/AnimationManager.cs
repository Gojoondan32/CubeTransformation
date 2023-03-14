using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ModularMotion;

public class AnimationManager : MonoBehaviour
{
    public static AnimationManager Instance;
    [SerializeField] private UIMotion mainScreen;
    [SerializeField] private UIMotion loadingScreen;
    [SerializeField] private UIMotion studentScreen;
    private void Awake() {
        if(Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void StartMoving(){
        PlayMotion();
    }
    private IEnumerator WaitTime(){
        yield return new WaitForSeconds(1f);
        PlayMotion();
    }

    private void PlayMotion(){

        mainScreen.Play();
        loadingScreen.Play();
        studentScreen.Play();
    }
    public void PlayInReverse(){
        mainScreen.PlayAllBackward();
        loadingScreen.PlayAllBackward();
        studentScreen.PlayAllBackward();
    }
}
