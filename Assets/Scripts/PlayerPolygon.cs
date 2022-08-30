using System.Collections.Generic;
using UnityEngine;

public class PlayerPolygon : MonoBehaviour
{

    public GameObject liInterface;

    public List<Vector3> points;
    public List<GameObject> lines;
    public List<DrawLine> linesUpdaters;
    
    private float _width;
    private static int _sides;
    private float _radius;
    
    public static float Rotation;
    public static int LineBuffer;
    private static float _rotationAlpha;
    private const float Tau = 2 * Mathf.PI;
    
    private void Start()
    {
        _radius = Main.Radius;
        _sides = Main.Difficulty;
        _width = Main.LineWidth;
        linesUpdaters = new List<DrawLine>();
        lines = new List<GameObject>();
        LineBuffer = 0;
        
        GetPoints();
        InitLines();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Main.GameStarted)
        {
            // controls 
            if (Input.GetKeyDown(KeyCode.D) || Input.GetMouseButtonDown(1))
            {
                Rotation -= _rotationAlpha;
                if (Rotation < 0)
                {
                    Rotation += 360;
                }
            }

            if (Input.GetKeyDown(KeyCode.A) || Input.GetMouseButtonDown(0))
            {
                Rotation += _rotationAlpha;
                if (Rotation > 360)
                {
                    Rotation -= 360;
                }
            }
        }
        else
        {
            while (LineBuffer != 0)
            {
                Debug.Log("update buffer");
                if (LineBuffer > 0)
                {
                    _sides++;
                    GetPoints();
                    AddLine(_sides - 1, _sides - 2);
                    LineBuffer--;
                }
                else
                {
                    _sides--;
                    linesUpdaters.RemoveAt(linesUpdaters.Count - 1);
                    Destroy(lines[lines.Count - 1]);
                    lines.RemoveAt(lines.Count - 1);
                    LineBuffer++;
                }
            }
        }
        UpdateLines();
    }

    private void InitLines()
    {
        AddLine(0, points.Count - 1);
        
        for (var i = 1; i < _sides; i++)
        {
            AddLine(i, i -1);
        }
        
    }
    
    private void UpdateLines()
    {
        points.Clear();
        GetPoints();
        linesUpdaters[0].UpdatePoints(new[]{points[0], points[points.Count - 1]});
        for (var i = 1; i < _sides; i++)
        {
            linesUpdaters[i].UpdatePoints(new[]{points[i], points[i - 1]});
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void AddLine(int start, int end)
    {
        var tempLine = Instantiate(liInterface, transform);
        tempLine.GetComponent<DrawLine>().DrawLineInit(start,
            _width, new[]{points[start], points[end]});
        linesUpdaters.Add(tempLine.GetComponent<DrawLine>());
        lines.Add(tempLine);
        
    }
    public static void SetRotation()
    {
        Rotation = _sides % 2 != 0 ? -90 : -45;
        _rotationAlpha =  360f / _sides ;
    }
    private void GetPoints()
    {
        points = new List<Vector3>();

        for (var currentPoint = 0; currentPoint < _sides; currentPoint++)
        {
            var currentRadian = (float)currentPoint / _sides * Tau;
            var x = Mathf.Cos(currentRadian) * _radius;
            var y = Mathf.Sin(currentRadian) * _radius;
            
            var newPosition = new Vector3(x, y, 0);
            newPosition = Quaternion.Euler(0, 0, Rotation) * newPosition;
            newPosition.y -= 5;
            
            points.Add(newPosition);
        }
        
    }
}
