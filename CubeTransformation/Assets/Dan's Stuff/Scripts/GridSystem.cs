using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem
{
    private int width, height;
    private float cellSize;
    private GridObject[,] gridObjectArray;
    public GridSystem(int width, int height, float cellSize)
    {
        //Initilialising varaibles
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;

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
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // 
                GridPosition gridPosition = new GridPosition(x, y);

                Transform debugTransform = GameObject.Instantiate(debugPrefab, GetWorldPosition(gridPosition), Quaternion.identity);
                GridDebugObject gridDebugObject = debugTransform.GetComponent<GridDebugObject>();

                gridDebugObject.SetGridObject(GetGridObject(gridPosition));
            }
        }
    }

    public GridObject GetGridObject(GridPosition gridPosition) => gridObjectArray[gridPosition.x, gridPosition.y];

    public bool IsValidGridPosition(GridPosition gridPosition) => gridPosition.x >= 0 && gridPosition.y >= 0
        && gridPosition.x < width && gridPosition.y < height;


    public int GetWidth() => width;
    public int GetHeight() => height;
}

