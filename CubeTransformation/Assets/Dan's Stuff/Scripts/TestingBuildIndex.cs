using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestingBuildIndex : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        Debug.Log(sceneIndex);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
