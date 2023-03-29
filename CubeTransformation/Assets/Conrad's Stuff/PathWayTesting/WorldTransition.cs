using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class WorldTransition : MonoBehaviour
{
    [SerializeField]private GameObject LoadingScreen;
    //[SerializeField] private TextMeshProUGUI LoadingBarFill;

    private void Awake()
    {
        Debug.Log(LoadingScreen);
        LoadingScreen.SetActive(false);
    }

    public void Loadscene(int sceneId)
    {
        LoadingScreen.SetActive(true);
        StartCoroutine(LoadSceneAsync(sceneId));
    }

    IEnumerator LoadSceneAsync(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
        yield return null;
        
    }
}
