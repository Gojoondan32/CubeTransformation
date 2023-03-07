using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class SerialisationHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Download the data 
    private IEnumerator GetDataRequest(string url){
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url)){
            webRequest.SetRequestHeader("Content", "application/json");
            yield return webRequest.SendWebRequest();

            string[] pages = url.Split('/');
            int page = pages.Length - 1;

            if(webRequest.result == UnityWebRequest.Result.ConnectionError){
                Debug.LogWarning("Web request error");
                yield break;
            }

            string textToParse = webRequest.downloadHandler.text;
            TransformationData transformationData = JsonUtility.FromJson<TransformationData>(textToParse);

            Debug.Log(transformationData.score);
            Debug.Log(transformationData.time);

        }
    }

    private IEnumerator SetDataRequest(string url){
        TransformationData transformationData = new TransformationData(4, 50);

        string json = JsonUtility.ToJson(transformationData);

        UnityWebRequest webRequest = UnityWebRequest.Post(url, json);
        webRequest.SetRequestHeader("Content", "application/json");

        var jsonByts = Encoding.UTF8.GetBytes(json);
        webRequest.uploadHandler = new UploadHandlerRaw(jsonByts);
        webRequest.downloadHandler = new DownloadHandlerBuffer();
        yield return webRequest.SendWebRequest();

        if(webRequest.result == UnityWebRequest.Result.ConnectionError){
            Debug.Log("Web request error");
        }
        else{
            Debug.Log(webRequest.downloadHandler.text);
            Debug.Log("Data was submitted");
        }
    }
}
