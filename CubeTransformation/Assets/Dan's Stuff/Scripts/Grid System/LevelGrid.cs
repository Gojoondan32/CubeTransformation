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

        gridSystem = new GridSystem(10, 10, 0.25f, marker);
        gridSystem.CreateDebugObjects(gridDebugObject);
        marker.transform.position = new Vector3(0, 2f, 4.9f); //! Testing for my scene
        
    }

    public void MoveGrid(Vector3 position){
        Debug.Log("Moving Grid");
        marker.transform.position = position;
    }

    private void CreatePath(){
        int width = Random.Range(0, gridSystem.GetWidth());
        int height = Random.Range(0, gridSystem.GetHeight());

        GridPosition gridPosition = gridSystem.GetGridPosition(new Vector3(width, height, 0));


        //marker.position = gridSystem.GetWorldPosition()
    }
}
