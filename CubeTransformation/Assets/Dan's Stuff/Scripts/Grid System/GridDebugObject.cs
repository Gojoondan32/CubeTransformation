using UnityEngine;
using TMPro;

public class GridDebugObject : MonoBehaviour
{
    [SerializeField] private TextMeshPro textMeshPro;
    [SerializeField] private TextMeshPro xNumber;
    [SerializeField] private TextMeshPro yNumber;
    [SerializeField] private TextMeshPro xAxis;
    [SerializeField] private TextMeshPro yAxis;
    private GridObject gridObject;
    public void SetGridObject(GridObject gridObject)
    {
        this.gridObject = gridObject;
    }


    public void DisplayXNumber(int number)
    {
        xNumber.text = number.ToString();
        xNumber.gameObject.SetActive(true);
    }
    public void DisplayYNumber(int number)
    {
        yNumber.text = number.ToString();
        yNumber.gameObject.SetActive(true);
    }

    public void DisplayXAxis(){
        xAxis.gameObject.SetActive(true);
    }
    public void DisplayYAxis(){
        yAxis.gameObject.SetActive(true);
    }

    private void Update()
    {
        //textMeshPro.text = gridObject.ToString();
    }
}