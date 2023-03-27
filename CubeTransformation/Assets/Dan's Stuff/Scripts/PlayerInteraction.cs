using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;
using UnityEngine.XR;

public class PlayerInteraction : MonoBehaviour
{
    private InputDevice rightTargetDevice; // The right controller
    [SerializeField] private Transform playerHandRight; // This is the hand that is used to select a point on the grid
    [SerializeField] private RayInteractor rayInteractor; // This is the ray that is used to select a point on the grid

    [SerializeField] private PlayerSumbit playerSumbit; // This is the script that is used to submit the player's answer

    [SerializeField] private Transform testObject; // This is the visual object seen on the grid
    [SerializeField] private Transform playerPointTestPrefab;
    [SerializeField] private bool pointSelected;
    [SerializeField] private LineRenderer lineRenderer;
    private float amountOfPlayerPoints;
    private List<Transform> playerPoints;

    private void Awake() {
        playerPoints = new List<Transform>();
        pointSelected = false;
        amountOfPlayerPoints = 0;
    }

    private void Start() {
        List<InputDevice> devices = new List<InputDevice>();
        InputDeviceCharacteristics rightControllerCharacteristics = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(rightControllerCharacteristics, devices);

        foreach (var item in devices)
        {
            Debug.Log(item.name + item.characteristics);
        }

        if(devices.Count > 0)
        {
            rightTargetDevice = devices[0]; // This should be the right controller
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(rightTargetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue) && triggerValue > 0.1f)
        {
            // This should be used to select a point on the grid and move it
            Debug.Log("Pressing Trigger Button");
            PlayerSelected();
        }
        else PlayerUnselected(); // This should be used to deselect a point on the grid   
        if(rightTargetDevice.TryGetFeatureValue(CommonUsages.gripButton, out bool gripButtonValue) && gripButtonValue)
        {
            // This should be used to try and sumbit the player's answer
            Debug.Log("Pressing the grip button Button");
            playerSumbit.SubmitAnswer();

        }
        DoRaycast();
        
        if(playerPoints.Count < 2) return;
        GenerateLines(); // Called every frame so that the line updates while the player is moving a point
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
        if(DoRaycast() == false) return; // Stop the player from placing a point if they are not looking at the grid

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

    private bool DoRaycast(){
        Vector3 dir = rayInteractor.End - playerHandRight.position;
        Debug.DrawRay(playerHandRight.position, dir, Color.blue, 1f);
        if(Physics.Raycast(playerHandRight.position, dir, out RaycastHit hit, float.MaxValue, LayerMask.GetMask("GridObject"))){
            Debug.Log("Hit Object");
            SnapToGrid(hit.point);
            return true;
        }
        return false; // Stop the player from placing a point if they are not looking at the grid
    }

    // Cannot use the generate line method on the shape manager as this class holds a list of transforms instead of Vector3
    private void GenerateLines(){
        for (int i = 0; i < playerPoints.Count; i++){
            lineRenderer.SetPosition(i, new Vector3(playerPoints[i].position.x, playerPoints[i].position.y, LevelGrid.Instance.Marker.position.z - 0.01f));
        }

        lineRenderer.positionCount = playerPoints.Count + 1; // Don't know if this is needed
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, new Vector3(playerPoints[0].position.x, playerPoints[0].position.y, LevelGrid.Instance.Marker.position.z - 0.01f));
        
    }
}
