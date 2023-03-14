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
    [SerializeField] private LineRenderer lineRenderer;
    private float amountOfPlayerPoints;

    [SerializeField] private List<Transform> playerPoints;

    

    private void Awake() {
        playerPoints = new List<Transform>();
        pointSelected = false;
        amountOfPlayerPoints = 0;
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
        if(playerPoints.Count < 2) return;
        GenerateLines();
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
        Transform playerPoint = CheckIfPointExits(testObject);
        if(playerPoint == null){
            // A point does not currently exist on this point so we need to place one
            PlacePoint(testObject.position);
        }
        else{
            // We have selected a point which does currently exists so we need to move it
            StartCoroutine(MovePlayerPoint(playerPoint));
        }
    }

    private Transform CheckIfPointExits(Transform position){
        foreach(Transform pos in playerPoints){
            if(pos.position == position.position){
                playerPoints.Remove(pos); // Remove from the list while we are looping here so we don't need another loop to remove it later
                return pos;
            }
        }
        return null;
    }

    private void PlacePoint(Vector3 position){
        if(amountOfPlayerPoints >= 4) return;
        Transform point = Instantiate(playerPointTestPrefab, position, Quaternion.identity);
        playerPoints.Add(point);
        amountOfPlayerPoints++;
    }

    private IEnumerator MovePlayerPoint(Transform point){
        while(pointSelected == true){
            point.position = testObject.position;
            yield return null;
        }
        playerPoints.Add(point);
    }

    public void PlayerUnselected(){
        pointSelected = false;
    }

    public List<Vector3> GetPlayerPoints(){
        List<Vector3> points = new List<Vector3>();
        foreach(Transform point in playerPoints){
            points.Add(point.position);
        }
        return points;
    }
    private void GenerateLines(){
        for (int i = 0; i < playerPoints.Count; i++){
            lineRenderer.SetPosition(i, new Vector3(playerPoints[i].position.x, playerPoints[i].position.y, 5));
        }

        lineRenderer.positionCount = playerPoints.Count + 1; // Don't know if this is needed
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, new Vector3(playerPoints[0].position.x, playerPoints[0].position.y, 5));
        
    }
}
