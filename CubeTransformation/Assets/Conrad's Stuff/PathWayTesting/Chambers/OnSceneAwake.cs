using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnSceneAwake : MonoBehaviour
{
    private void Awake()
    {
        CurrentPosition.CurrentDimension = SceneManager.GetActiveScene().buildIndex;
    }
}
