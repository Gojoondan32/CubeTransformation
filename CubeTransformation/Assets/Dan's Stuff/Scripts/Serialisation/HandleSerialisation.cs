using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;

public class HandleSerialisation : MonoBehaviour
{
    public static HandleSerialisation Instance;
    // Keep these as lists so we can add to them
    private List<TranslationData> translationDataList = new List<TranslationData>();
    private List<ReflectionData> reflectionDataList = new List<ReflectionData>();
    private List<RotationData> rotationDataList = new List<RotationData>(); 
    private int roomsCompleted = 0;

    private void Awake() {
        if(Instance == null) Instance = this;
        else Destroy(this.gameObject);

        roomsCompleted = 0;

        DontDestroyOnLoad(this.gameObject); // Keep this object alive between scenes (so we can keep the lists)
    }

    #region CreateData

    public void CreateTranslationData(List<Vector3> playerPoints, List<Vector3> shapePoints, Vector3 translation, bool isCorrect){
        TranslationData translationData = new TranslationData();
        translationData.playerPoints = ConvertPointsToWorldSpace(playerPoints).ToArray();
        translationData.shapePoints = ConvertPointsToWorldSpace(shapePoints).ToArray();
        translationData.translation = translation;
        translationData.isCorrect = isCorrect;
        translationDataList.Add(translationData);
        Debug.Log("Added translation data");
    }

    public void CreateReflectionData(List<Vector3> playerPoints, List<Vector3> shapePoints, List<Vector3> reflectionPoints, bool isCorrect){
        ReflectionData reflectionData = new ReflectionData();
        reflectionData.playerPoints = ConvertPointsToWorldSpace(playerPoints).ToArray();
        reflectionData.shapePoints = ConvertPointsToWorldSpace(shapePoints).ToArray();
        reflectionData.reflectionPoints = ConvertPointsToWorldSpace(reflectionPoints).ToArray();
        reflectionData.isCorrect = isCorrect;
        reflectionDataList.Add(reflectionData);
        Debug.Log("Added reflection data");
    }

    public void CreateRotationData(List<Vector3> playerPoints, List<Vector3> shapePoints, Vector3 rotationPoint, bool isCorrect){
        RotationData rotationData = new RotationData();
        rotationData.playerPoints = ConvertPointsToWorldSpace(playerPoints).ToArray();
        rotationData.shapePoints = ConvertPointsToWorldSpace(shapePoints).ToArray();
        rotationData.rotationPoint = rotationPoint;
        rotationData.isCorrect = isCorrect;
        rotationDataList.Add(rotationData);
    }

    [ContextMenu("Create Transformation Data")]
    public void CreateTransformationData(){
        TransformationData transformationData = new TransformationData();
        transformationData.translationData = translationDataList.ToArray();
        transformationData.reflectionData = reflectionDataList.ToArray();
        transformationData.rotationData = rotationDataList.ToArray();
        UploadData(transformationData);
    }

    #endregion

    public void RoomComplete(){
        roomsCompleted++;
        if(roomsCompleted == 3){
            CreateTransformationData();
            roomsCompleted = 0;
        }
    }

    private List<Vector3> ConvertPointsToWorldSpace(List<Vector3> points){
        List<Vector3> worldSpacePoints = new List<Vector3>();
        for(int i = 0; i < points.Count; i++){
            Vector3 temp = LevelGrid.Instance.gridSystem.TransposeGridPositionToWorldPosition(points[i]);
            Vector3 temp2 = new Vector3(Mathf.RoundToInt(temp.x), Mathf.RoundToInt(temp.y), 0); // Test
            worldSpacePoints.Add(temp2);
            Debug.Log($"X = {temp2.x}, Y = {temp2.y}");
            
        }
        return worldSpacePoints;
    }

    public void UploadData(TransformationData transformationData){
        StartCoroutine(TrialSetDataRequest("https://getpantry.cloud/apiv1/pantry/fa8f4194-314d-4ece-8d08-8fe8f5592358/basket/StudentScore1", transformationData));
    }

    private IEnumerator TrialSetDataRequest(string url, TransformationData transformationData){
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
            webRequest.Dispose();
        }

        
    }
}
