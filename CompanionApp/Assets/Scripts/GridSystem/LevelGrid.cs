using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGrid : MonoBehaviour
{
    public static LevelGrid Instance { get; private set; }
    [SerializeField] private Transform gridDebugObject;
    public GridSystem gridSystem; //!Change back to private

    [Header("Testing")]
    public GameObject testCube;
    [SerializeField] private Transform marker;
    public Transform Marker {get {return marker;}}

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        //CreatePath();
        gridSystem = new GridSystem(10, 10, 0.5f, marker);
        gridSystem.CreateDebugObjects(gridDebugObject);
        marker.transform.position = new Vector3(-2.2f, -5, 0); //! Testing
        
    }

    private void CreatePath(){
        for(int x = 0; x < 10; x++){
            for(int y = 0; y < 10; y++){
                Instantiate(gridDebugObject, new Vector3(x, y, 0), Quaternion.identity);
            }
        }
    }
}
