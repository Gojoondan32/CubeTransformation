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
        gridSystem = new GridSystem(10, 10, 0.9f, marker);
        gridSystem.CreateDebugObjects(gridDebugObject);
        marker.transform.position = new Vector3(21f, -4f, 0); //! Testing
        
    }

    public TransformationData TestTransformData(){
        TransformationData transformationData = new TransformationData();
        Vector3[] shapePoints = new Vector3[4]{
            new Vector3(1, 1, 0),
            new Vector3(1, 3, 0),
            new Vector3(3, 3, 0),
            new Vector3(3, 1, 0)
        };
        Vector3[] playerPoints = new Vector3[4]{
            new Vector3(5, 1, 0),
            new Vector3(5, 3, 0),
            new Vector3(7, 3, 0),
            new Vector3(7, 1, 0)
        };
        Vector3[] reflectionPoints = new Vector3[2]{
            new Vector3(4, 0, 0),
            new Vector3(4, 9, 0)
        };

        transformationData.shapePoints = shapePoints;
        transformationData.playerPoints = playerPoints;
        transformationData.reflectionPoints = reflectionPoints;

        return transformationData;
    }

    private void CreatePath(){
        for(int x = 0; x < 10; x++){
            for(int y = 0; y < 10; y++){
                Instantiate(gridDebugObject, new Vector3(x, y, 0), Quaternion.identity);
            }
        }
    }
}
