using System.Collections.Generic;

public class GridObject
{
    private GridSystem gridSystem;

    //This represents the index that this grid object occupies in the 2D array on the grid system
    private GridPosition gridPosition;
    
    //private Point point //Use this to keep track of which point is on which grid object so they cannot overlap

    public GridObject(GridSystem gridSystem, GridPosition gridPosition)
    {
        this.gridSystem = gridSystem;
        this.gridPosition = gridPosition;
    }
    public override string ToString()
    {
        /*
        string unitString = "";
        foreach (Unit unit in unitList)
        {
            unitString += unit + "\n";
        }
        */
        return $"x: {gridPosition.x} y: {gridPosition.y} \n point:  ";
    }
}
