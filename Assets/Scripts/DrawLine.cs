using UnityEngine;

public class DrawLine : MonoBehaviour
{
    public int sideNum = -1;
    public Vector3[] points;
    private LineRenderer _lineRenderer;
    public float width; 
    public void DrawLineInit(int num,float w, Vector3[] se)
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.positionCount = 2;
        _lineRenderer.startWidth = w;
        _lineRenderer.endWidth = w;
        _lineRenderer.numCapVertices = 70;
        _lineRenderer.SetPositions(points);
        
        _lineRenderer.startColor = Main.UsedColors[num];
        _lineRenderer.endColor = Main.UsedColors[num];

        width = w;
        sideNum = num;
        points = se;
    }
    
    // Update is called once per frame
    private void Update()
    {
        if (sideNum != -1)
            _lineRenderer.SetPositions(points);
    }

    private void OnDestroy()
    {
        Destroy(_lineRenderer);
    }

    public void UpdatePoints(Vector3[] se)
    {
        points = se;
    }
}
