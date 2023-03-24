using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AnimationManager : MonoBehaviour
{
    public static AnimationManager Instance;
    [SerializeField] private Transform middleScreen;
    [SerializeField] private Transform offScreen;

    [SerializeField] private Transform middleScreenStudent;
    [SerializeField] private Transform offScreenStudent;

    private int currentMainScreen = 0;
    private int currentStudentScreen = 0;
    [SerializeField] private Screens[] mainScreens;
    [SerializeField] private Screens[] studentScreens;
    
    private void Awake() {
        if(Instance == null) Instance = this;
        else Destroy(gameObject);


    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void StartMoving(){
        StartCoroutine(WaitTime());
       
    }

    public void MoveFromTo(int toScreenId){
        mainScreens[currentMainScreen].MoveToOffScreen(offScreen);
        mainScreens[toScreenId].MoveToMiddleScreen(middleScreen);
        currentMainScreen = toScreenId;
    }

    public void MoveInStudentScreen(int toScreenId){
        studentScreens[currentStudentScreen].MoveToOffScreen(offScreenStudent);
        studentScreens[toScreenId].MoveToMiddleScreen(middleScreenStudent);
        currentStudentScreen = toScreenId;

    }
    private IEnumerator WaitTime(){
        yield return new WaitForSeconds(1f);
        MoveFromTo(2);
    }


}
