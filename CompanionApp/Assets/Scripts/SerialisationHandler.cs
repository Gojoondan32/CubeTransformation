using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class SerialisationHandler : MonoBehaviour
{
    [SerializeField] private DisplayTransformationData displayTransformationData;
    // Start is called before the first frame update
    void Start()
    {
        // This should be in the main program
        //StartCoroutine(TrialSetDataRequest("https://getpantry.cloud/apiv1/pantry/fa8f4194-314d-4ece-8d08-8fe8f5592358/basket/StudentScore1"));
    }
    private void Update() {
        if(Input.GetMouseButtonDown(0)){
            //StartCoroutine(GetDataRequest("https://getpantry.cloud/apiv1/pantry/fa8f4194-314d-4ece-8d08-8fe8f5592358/basket/StudentScore1"));
        }
    }
    public void StartDataDownload(){
        AnimationManager.Instance.StartMoving();
        StartCoroutine(GetDataRequest("https://getpantry.cloud/apiv1/pantry/fa8f4194-314d-4ece-8d08-8fe8f5592358/basket/StudentScore1"));
    }

    // Download the data 
    private IEnumerator GetDataRequest(string url){
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url)){
            webRequest.SetRequestHeader("Content-Type", "application/json");
            yield return webRequest.SendWebRequest();

            string[] pages = url.Split('/');
            int page = pages.Length - 1;

            if(webRequest.result == UnityWebRequest.Result.ConnectionError){
                Debug.LogWarning("Web request error");
                yield break;
            }

            string textToParse = webRequest.downloadHandler.text;
            TransformationData transformationData = JsonUtility.FromJson<TransformationData>(textToParse);

            //Debug.Log(transformationData.score);
            //Debug.Log(transformationData.time);
            displayTransformationData.PassInTransformationData(transformationData);
            AnimationManager.Instance.StartMoving();
        }
    }

    private IEnumerator TrialSetDataRequest(string url){
        TransformationData transformationData = LevelGrid.Instance.TestTransformData();
        //transformationData.score = 420;
        //transformationData.time = 69;

        string json = JsonUtility.ToJson(transformationData);
        using (UnityWebRequest webRequest = UnityWebRequest.Post(url, json)){
            webRequest.SetRequestHeader("Content-Type", "application/json");

            var jsonByts = Encoding.UTF8.GetBytes(json);
            webRequest.uploadHandler = new UploadHandlerRaw(jsonByts);
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log("Web request error");
                yield break;
            }

            Debug.Log(webRequest.downloadHandler.text);
            Debug.Log("Data was submitted");

        }
    }

    private IEnumerator SetDataRequest(string url){
        TransformationData transformationData = new TransformationData();
        //transformationData.score = 50;
        //transformationData.time = 22;

        string json = JsonUtility.ToJson(transformationData);

        UnityWebRequest webRequest = UnityWebRequest.Post(url, json);
        webRequest.SetRequestHeader("Content-Type", "application/json");

        var jsonByts = Encoding.UTF8.GetBytes(json);
        webRequest.uploadHandler = new UploadHandlerRaw(jsonByts);
        webRequest.downloadHandler = new DownloadHandlerBuffer();
        yield return webRequest.SendWebRequest();

        webRequest.disposeUploadHandlerOnDispose = true;
        webRequest.disposeDownloadHandlerOnDispose = true;

        if(webRequest.result == UnityWebRequest.Result.ConnectionError){
            Debug.Log("Web request error");
            yield break;
        }

        Debug.Log(webRequest.downloadHandler.text);
        Debug.Log("Data was submitted");

        webRequest.Dispose();
    }
}
