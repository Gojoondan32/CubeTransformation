using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class DisplayTransformationData : MonoBehaviour
{
    [SerializeField] private Camera targetCamera;

    [SerializeField] private LineRenderer shapeLineRenderer;
    [SerializeField] private LineRenderer playerLineRenderer;
    [SerializeField] private LineRenderer reflectionLineRenderer;
    [SerializeField] private RenderTexture renderTexture;
    [SerializeField] private RawImage[] gridImages;
    private int currentGrid;

    public void PassInTransformationData(TransformationData transformationData){
        // Convert each point to grid space
        List<Vector3> shapePoints = ConvertPointsToGridSpace(transformationData.shapePoints);
        List<Vector3> playerPoints = ConvertPointsToGridSpace(transformationData.playerPoints);
        List<Vector3> reflectionPoints = ConvertPointsToGridSpace(transformationData.reflectionPoints);

        GenerateLines(shapePoints, shapeLineRenderer, true);
        GenerateLines(playerPoints, playerLineRenderer, true);
        GenerateLines(reflectionPoints, reflectionLineRenderer, false);  

        //ConvertRenderTextureToTexture2D(); 
        TestingNewRender();
        TestingNewRender();
        //ConvertRenderTextureToTexture2D(); //! Second one is for testing purposes
        
        //GenerateShapeLines(shapePoints);
        //GeneratePlayerLines(playerPoints);
        //GenerateReflectionLines(reflectionPoints);
    }

    private List<Vector3> ConvertPointsToGridSpace(Vector3[] points){
        List<Vector3> gridSpacePoints = new List<Vector3>();
        foreach(Vector3 point in points){
            gridSpacePoints.Add(LevelGrid.Instance.gridSystem.TransposeWorldPositionToGridPosition(point));
        }
        return gridSpacePoints;
    }
    private void GenerateLines(List<Vector3> points, LineRenderer lineRenderer, bool connectFirstToLast){
        if(connectFirstToLast) lineRenderer.positionCount = points.Count + 1;
        else lineRenderer.positionCount = points.Count;

        for (int i = 0; i < points.Count; i++){
            lineRenderer.SetPosition(i, new Vector3(points[i].x, points[i].y, 0));
        }

        if(connectFirstToLast) lineRenderer.SetPosition(lineRenderer.positionCount - 1, new Vector3(points[0].x, points[0].y, 0));
    }


    private void TestingNewRender(){
        RenderTexture renderTexture = RenderTexture.active;
        RenderTexture.active = targetCamera.targetTexture;
        targetCamera.Render();

        Texture2D texture = new Texture2D(targetCamera.targetTexture.width, targetCamera.targetTexture.height);
        texture.ReadPixels(new Rect(0, 0, targetCamera.targetTexture.width, targetCamera.targetTexture.height), 0, 0);
        texture.Apply();
        RenderTexture.active = renderTexture;

        gridImages[currentGrid].texture = texture;

        currentGrid++;

    }

    
    private void ConvertRenderTextureToTexture2D(){
        Texture2D outputTexture = new Texture2D(targetCamera.targetTexture.width, targetCamera.targetTexture.height, TextureFormat.RGB24, false);
        RenderTexture currentRT = RenderTexture.active;
        RenderTexture.active = targetCamera.targetTexture;
        outputTexture.ReadPixels(new Rect(0, 0, targetCamera.targetTexture.width, targetCamera.targetTexture.height), 0, 0);
        outputTexture.Apply();
        RenderTexture.active = currentRT;

        gridImages[currentGrid].texture = outputTexture;
        currentGrid++;
        /*
        Texture2D texture = new Texture2D(1022, 1022, TextureFormat.RGB24, false);
        RenderTexture.active = renderTexture;
        texture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture.Apply();

        gridImages[currentGrid].texture = texture;
        currentGrid++;
        */
    }

    /*
    private void TestTexture(){
        RenderTexture renderTexture = new RenderTexture(targetCamera.targetTexture.width, targetCamera.targetTexture.height, 24);
        Texture2D outputTexture = new Texture2D(targetCamera.targetTexture.width, targetCamera.targetTexture.height, TextureFormat.RGB24, false);
        
        RenderTexture.active = renderTexture;
        targetCamera.targetTexture = renderTexture;
        targetCamera.Render();

    }

    private void RenderLineRenderer(LineRenderer lineRenderer, RenderTexture renderTexture){
        RenderTexture tempRenderTexture = RenderTexture.GetTemporary(targetCamera.targetTexture.width, targetCamera.targetTexture.height, 0);
        Graphics.Blit(renderTexture, tempRenderTexture);
        RenderTexture.active = tempRenderTexture;

        GL.PushMatrix();
        GL.LoadPixelMatrix(0, targetCamera.targetTexture.width, targetCamera.targetTexture.height, 0);
        
    }
    */

    #region Old Stuff
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
    #endregion
}
