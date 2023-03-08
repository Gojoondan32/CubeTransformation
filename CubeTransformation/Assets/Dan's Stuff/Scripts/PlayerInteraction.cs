using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private Transform indicator;
    [SerializeField] private Transform furtherIndicator;
    [SerializeField] private Transform testObject;
    [SerializeField] private Transform playerPoint;
    [SerializeField] private bool pointSelected;

    private Dictionary<Vector3, Transform> playerPositionGFX;

    private List<Vector3> playerPositions;
    public List<Vector3> PlayerPositions { get{return playerPositions;}}

    private void Awake() {
        playerPositions = new List<Vector3>();
        playerPositionGFX = new Dictionary<Vector3, Transform>();
        pointSelected = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = furtherIndicator.position - indicator.position;
        Debug.DrawRay(furtherIndicator.position, dir, Color.blue, 1f);
        //Fire a raycast to the indicator
        if(Physics.Raycast(transform.position, dir, out RaycastHit hit, float.MaxValue, LayerMask.GetMask("GridObject"))){
            Debug.Log("Hit Object");
            SnapToGrid(hit.point);
        }
    }

    private void SnapToGrid(Vector3 hitPoint){
        GridPosition gridPosition = LevelGrid.Instance.gridSystem.GetGridPosition(hitPoint); // Should convert world position into ints
        Vector3 position = new Vector3(
            LevelGrid.Instance.gridSystem.GetWorldPosition(gridPosition).x,
            LevelGrid.Instance.gridSystem.GetWorldPosition(gridPosition).y,
            LevelGrid.Instance.Marker.position.z
        );
        testObject.position = position;
    }

    public void PlayerSelected(){
        Debug.Log(testObject.position);
        pointSelected = true;
        if(!CheckIfPointExits()){
            Transform temp = Instantiate(playerPoint, testObject.position, Quaternion.identity);
            playerPositionGFX.Add(testObject.position, temp); // Store the vector as the key to access it easier
        }
        else{
            Debug.Log("A point is already in position");
            StartCoroutine(MovePlayerPoint(testObject.position));
        }
    }

    private bool CheckIfPointExits(){
        foreach(Vector3 position in playerPositions){
            if(testObject.position == position) return true;
        }
        playerPositions.Add(testObject.position);
        return false;

    }

    private IEnumerator MovePlayerPoint(Vector3 position){
    
        while(pointSelected == true){
            playerPositionGFX[position].position = testObject.position; // This method is only called once 
            yield return null;
        }
        Transform tempGFX = playerPositionGFX[position];
        playerPositionGFX.Remove(position);
        playerPositionGFX.Add(testObject.position, tempGFX);
    }

    public void PlayerUnselected(){
        pointSelected = false;
    }
}
