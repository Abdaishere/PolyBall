using System.Collections.Generic;
using UnityEngine;

public class PlayerPolygon : MonoBehaviour
{

    public GameObject liInterface;

    private List<Vector3> _points;
    private List<GameObject> _lines;
    private List<DrawLine> _linesUpdaters;
    
    private float _width;
    private static int _sides;
    private float _radius;
    
    public static float Rotation;
    private static float _rotationAlpha;
    private const float Tau = 2 * Mathf.PI;
    
    private void Start()
    {
        _radius = Main.Radius;
        _sides = Main.Difficulty;
        _width = Main.LineWidth;
        _linesUpdaters = new List<DrawLine>();
        _lines = new List<GameObject>();

        GetPoints();
        InitLines();
    }

    // Update is called once per frame
    private void Update()
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
        
        UpdateLines();
    }

    private void InitLines()
    {
        AddLine(0, _points.Count - 1);
        
        for (var i = 1; i < _sides; i++)
        {
            AddLine(i, i -1);
        }
        
    }
    
    private void UpdateLines()
    {
        _points.Clear();
        GetPoints();
        _linesUpdaters[0].UpdatePoints(new[]{_points[0], _points[_points.Count - 1]});
        for (var i = 1; i < _sides; i++)
        {
            _linesUpdaters[i].UpdatePoints(new[]{_points[i], _points[i - 1]});
        }
    }

    public void AddLine(int start, int end)
    {
        var tempLine = Instantiate(liInterface, transform);
        tempLine.GetComponent<DrawLine>().DrawLineInit(start,
            _width, new[]{_points[start], _points[end]});
        _linesUpdaters.Add(tempLine.GetComponent<DrawLine>());
        _lines.Add(tempLine);
        
    }
    public void DestroyLine(int num)
    {
        Destroy(_lines[num]);
        _linesUpdaters.RemoveAt(num);
    }
    
    public static void SetRotation()
    {
        Rotation = _sides % 2 != 0 ? -90 : -45;
        _rotationAlpha =  360f / _sides ;
    }
    private void GetPoints()
    {
        _points = new List<Vector3>();

        for (var currentPoint = 0; currentPoint < _sides; currentPoint++)
        {
            var currentRadian = (float)currentPoint / _sides * Tau;
            var x = Mathf.Cos(currentRadian) * _radius;
            var y = Mathf.Sin(currentRadian) * _radius;
            
            var newPosition = new Vector3(x, y, 0);
            newPosition = Quaternion.Euler(0, 0, Rotation) * newPosition;
            newPosition.y -= 5;
            
            _points.Add(newPosition);
        }
        
    }
}
