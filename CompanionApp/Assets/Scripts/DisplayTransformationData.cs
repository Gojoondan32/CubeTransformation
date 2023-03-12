using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayTransformationData : MonoBehaviour
{
    [SerializeField] private LineRenderer shapeLineRenderer;
    [SerializeField] private LineRenderer playerLineRenderer;
    [SerializeField] private LineRenderer reflectionLineRenderer;

    public void PassInTransformationData(TransformationData transformationData){
        // Convert each point to grid space
        List<Vector3> shapePoints = ConvertPointsToGridSpace(transformationData.shapePoints);
        List<Vector3> playerPoints = ConvertPointsToGridSpace(transformationData.playerPoints);
        List<Vector3> reflectionPoints = ConvertPointsToGridSpace(transformationData.reflectionPoints);

        GenerateShapeLines(shapePoints);
        GeneratePlayerLines(playerPoints);
        GenerateReflectionLines(reflectionPoints);
    }

    private List<Vector3> ConvertPointsToGridSpace(Vector3[] points){
        List<Vector3> gridSpacePoints = new List<Vector3>();
        foreach(Vector3 point in points){
            gridSpacePoints.Add(LevelGrid.Instance.gridSystem.TransposeWorldPositionToGridPosition(point));
        }
        return gridSpacePoints;
    }

    private void GenerateShapeLines(List<Vector3> points){
        shapeLineRenderer.positionCount = points.Count + 1;
        for (int i = 0; i < points.Count; i++){
            shapeLineRenderer.SetPosition(i, new Vector3(points[i].x, points[i].y, 0));
        }
        shapeLineRenderer.SetPosition(shapeLineRenderer.positionCount - 1, new Vector3(points[0].x, points[0].y, 0));
    }
    private void GeneratePlayerLines(List<Vector3> points)
    {
        playerLineRenderer.positionCount = points.Count + 1;
        for (int i = 0; i < points.Count; i++){
            playerLineRenderer.SetPosition(i, new Vector3(points[i].x, points[i].y, 0));
        }
        playerLineRenderer.SetPosition(playerLineRenderer.positionCount - 1, new Vector3(points[0].x, points[0].y, 0));
    }
    private void GenerateReflectionLines(List<Vector3> points)
    {
        reflectionLineRenderer.positionCount = points.Count;
        for (int i = 0; i < points.Count; i++){
            reflectionLineRenderer.SetPosition(i, new Vector3(points[i].x, points[i].y, 0));
        }
    }
}
