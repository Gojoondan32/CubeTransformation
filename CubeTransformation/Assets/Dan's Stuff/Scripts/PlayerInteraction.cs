using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private Transform indicator;
    [SerializeField] private Transform furtherIndicator;
    [SerializeField] private Transform testObject; // This is the visual object seen on the grid
    [SerializeField] private Transform playerPointTestPrefab;
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
        pointSelected = true;
        if(!CheckIfPointExits(testObject.position)){
            // Point does not exist so we need to create a new one
            Transform tempPoint = Instantiate(playerPointTestPrefab, testObject.position, Quaternion.identity);
            playerPositionGFX.Add(testObject.position, tempPoint);
        }
        else{
            // We want to move the point
            StartCoroutine(MovePlayerPoint(testObject.position));
        }
    }

    private bool CheckIfPointExits(Vector3 position){
        if(playerPositionGFX.ContainsKey(position)) return true;
        else return false;

    }

    private IEnumerator MovePlayerPoint(Vector3 position){
        while(pointSelected == true){
            playerPositionGFX[position].position = testObject.position; // Move the point
            yield return null;
        }
        Transform point = playerPositionGFX[position];
        playerPositionGFX.Remove(position); // Remove the old position as the key
        playerPositionGFX.Add(testObject.position, point);
    }

    public void PlayerUnselected(){
        pointSelected = false;
    }
}
