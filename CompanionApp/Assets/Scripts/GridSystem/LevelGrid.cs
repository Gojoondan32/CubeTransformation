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
        transformationData.translationData = new TranslationData[]{
            CreateTranslationData()
        };
        transformationData.reflectionData = new ReflectionData[]{
            CreateReflectionData()
        };
        transformationData.rotationData = new RotationData[]{
            CreateRotationData()
        };
        return transformationData;
    }

    private TranslationData CreateTranslationData(){
        TranslationData translationData = new TranslationData();
        Vector3[] shapePoints = new Vector3[4]{
            new Vector3(1, 2, 0),
            new Vector3(1, 3, 0),
            new Vector3(2, 3, 0),
            new Vector3(2, 2, 0)
        };
        Vector3[] playerPoints = new Vector3[4]{
            new Vector3(3, 4, 0),
            new Vector3(3, 5, 0),
            new Vector3(4, 5, 0),
            new Vector3(4, 4, 0)
        };
        translationData.shapePoints = shapePoints;
        translationData.playerPoints = playerPoints;
        translationData.translation = new Vector3(2, 2, 0);

        return translationData;
    }
    private ReflectionData CreateReflectionData(){
        ReflectionData reflectionData = new ReflectionData();
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

        reflectionData.shapePoints = shapePoints;
        reflectionData.playerPoints = playerPoints;
        reflectionData.reflectionPoints = reflectionPoints;

        return reflectionData;
    }
    private RotationData CreateRotationData(){
        RotationData rotationData = new RotationData();
        Vector3[] shapePoints = new Vector3[4]{
            new Vector3(7, 4, 0),
            new Vector3(9, 4, 0),
            new Vector3(9, 2, 0),
            new Vector3(8, 3, 0)
        };
        Vector3[] playerPoints = new Vector3[4]{
            new Vector3(4, 5, 0),
            new Vector3(4, 3, 0),
            new Vector3(2, 3, 0),
            new Vector3(3, 4, 0)
        };
        
        rotationData.shapePoints = shapePoints;
        rotationData.playerPoints = playerPoints;
        rotationData.rotationPoint = new Vector3(6, 6, 0);

        return rotationData;
    }

    private void CreatePath(){
        for(int x = 0; x < 10; x++){
            for(int y = 0; y < 10; y++){
                Instantiate(gridDebugObject, new Vector3(x, y, 0), Quaternion.identity);
            }
        }
    }
}
