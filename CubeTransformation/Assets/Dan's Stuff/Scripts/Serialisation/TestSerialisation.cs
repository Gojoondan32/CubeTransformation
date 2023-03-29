using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSerialisation : MonoBehaviour
{
    public static TestSerialisation Instance;

    private void Awake() {
        if(Instance == null) Instance = this;
        else Destroy(this.gameObject);
    }
    [SerializeField] private GameObject point;
    private List<Transform> playerPoints = new List<Transform>();    
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 4; i++){
            GameObject temp = Instantiate(point, new Vector3(0, 0, LevelGrid.Instance.Marker.position.z), Quaternion.identity);
            playerPoints.Add(temp.transform);
        }
    }

    public List<Vector3> GetPlayerPoints(){
        List<Vector3> temp = new List<Vector3>();
        foreach(Transform t in playerPoints){
            temp.Add(t.position);
        }
        return temp;
    }

}
