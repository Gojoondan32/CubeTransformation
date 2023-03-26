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
        LoadingScreen.SetActive(false);
    }

    public void Loadscene(int sceneId)
    {
        LoadingScreen.SetActive(true);
        StartCoroutine(LoadSceneAsync(sceneId));
    }

    IEnumerator LoadSceneAsync(int sceneID)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneID);
        while (!operation.isDone)
        {
            //float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
            //LoadingBarFill.text = progressValue.ToString() + "%";
            yield return null;
        }
    }
}
