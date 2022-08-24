using UnityEngine;

public class DrawLine : MonoBehaviour
{
    public int sideNum;
    public Vector3[] points;
    public LineRenderer lineRenderer;

    public void DrawLineInit(int num,float width, Vector3[] se)
    {
        lineRenderer.startColor = Main.UsedColors[num].Value;
        lineRenderer.endColor = Main.UsedColors[num].Value;
        
        // tag = Main.UsedColors[num].ColorIndex.ToString();
        lineRenderer.startWidth = width;
        lineRenderer.endWidth = width;
        lineRenderer.numCapVertices = 70;
        
        sideNum = num;
        points = se;
    }
    
    // Update is called once per frame
    private void Update()
    {
        lineRenderer.SetPositions(points);
        
    }

    public void UpdatePoints(Vector3[] se)
    {
        points = se;
    }
}
