using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGrid : MonoBehaviour
{
    public static LevelGrid Instance { get; private set; }
    [SerializeField] private Transform gridDebugObject;
    private GridSystem gridSystem;

    [Header("Testing")]
    public GameObject testCube;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);


        gridSystem = new GridSystem(10, 10, 1f);
        gridSystem.CreateDebugObjects(gridDebugObject);
    }

    private void Start(){
        for (int i = 0; i < 4; i++)
        {
            CreateObjectAtRandomPoint();
        }
        
    }

    private void CreateObjectAtRandomPoint(){
        int height = Random.Range(0, gridSystem.GetHeight());
        int width = Random.Range(0, gridSystem.GetWidth());
        GridPosition gridPosition = gridSystem.GetGridPosition(new Vector3(width, height, 0));

        if(gridSystem.IsValidGridPosition(gridPosition))
            Instantiate(testCube, gridSystem.GetWorldPosition(gridPosition), Quaternion.identity);

    }
}
