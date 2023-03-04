using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem
{
    private int width, height;
    private float cellSize;
    private Transform parentPosition;
    private GridObject[,] gridObjectArray;
    public GridSystem(int width, int height, float cellSize, Transform parentPosition)
    {
        //Initilialising varaibles
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.parentPosition = parentPosition;

        gridObjectArray = new GridObject[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                //Creating a new grid position struct to store the position of each grid object
                GridPosition gridPosition = new GridPosition(x, y);
                //Creating a new grid object and stroing its position inside a 2D array
                gridObjectArray[x, y] = new GridObject(this, gridPosition);

            }
        }
    }

    //Convert the grid position to the world position
    public Vector3 GetWorldPosition(GridPosition gridPosition)
    {
        return new Vector3(gridPosition.x, gridPosition.y, 0) * cellSize;
    }

    //Convert the world position to the nearest grid position
    public GridPosition GetGridPosition(Vector3 worldPosition) 
    {
        return new GridPosition(
            Mathf.RoundToInt(worldPosition.x / cellSize),
            Mathf.RoundToInt(worldPosition.y / cellSize)
        );
    }

    public void CreateDebugObjects(Transform debugPrefab)
    {
        bool displayYGraphics = true;
        bool dispalyXGraphics = true;
        GridDebugObject gridDebugObject = null;
        for (int x = 0; x < width; x++)
        {
            //Should display x number here
            dispalyXGraphics = true;
            for (int y = 0; y < height; y++)
            {
                GridPosition gridPosition = new GridPosition(x, y);
                Transform debugTransform = GameObject.Instantiate(debugPrefab, GetWorldPosition(gridPosition), Quaternion.identity, parentPosition);
                debugPrefab.localScale = new Vector3(cellSize, cellSize, 1); //! Might need to change z at a later date
                gridDebugObject = debugTransform.GetComponent<GridDebugObject>();

                gridDebugObject.SetGridObject(GetGridObject(gridPosition));
                
                //All Y graphics should only be displayed on the first iteration
                if(displayYGraphics){
                    gridDebugObject.DisplayYNumber(y);
                    if(y == height - 1) gridDebugObject.DisplayYAxis();
                }
                //X graphics should be displayed once every itertation
                if(dispalyXGraphics){
                    gridDebugObject.DisplayXNumber(x);
                    if(x == width - 1) gridDebugObject.DisplayXAxis();
                    dispalyXGraphics = false;
                }
            }
            displayYGraphics = false;
        }
    }

    public GridObject GetGridObject(GridPosition gridPosition) => gridObjectArray[gridPosition.x, gridPosition.y];

    public bool IsValidGridPosition(GridPosition gridPosition) => gridPosition.x >= 0 && gridPosition.y >= 0
        && gridPosition.x < width && gridPosition.y < height;


    /// <Summary>
    /// Convert a point on the grid to its corrosponding position in world space
    /// </Summary>
    public Vector3 TransposeGridPositionToWorldPosition(Vector3 position){
        return new Vector3((position.x - parentPosition.position.x) / cellSize, (position.y - parentPosition.position.y) / cellSize, 0);
    }

    /// <Summary>
    /// Convert a point calculated in world position to the corrosponding position on the grid
    /// </Summary>
    public Vector3 TransposeWorldPositionToGridPosition(Vector3 position){
        return new Vector3((position.x * cellSize) + parentPosition.position.x, (position.y * cellSize) + parentPosition.position.y, 0);
    }

    public int GetWidth() => width;
    public int GetHeight() => height;
    public float GetCellSize() => cellSize;
    public Vector3 GetStartingPosition() => parentPosition.position;
}

