using System.Collections.Generic;
using UnityEngine;

public class PlayerPolygon : MonoBehaviour
{

    private GameObject _liInterface;

    private List<Vector3> _points;
    private List<GameObject> _lines;

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
        _liInterface = GameObject.Find("Assets/Prefabs/DrawLine.prefab");
        
        GetPoints();
        CreateLines();
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

    private void CreateLines()
    {
        _lines = new List<GameObject>();
        var tempLine = Instantiate(_liInterface, transform);
        tempLine.GetComponent<DrawLine>().DrawLineInit(0,
            _width, new[]{_points[0], _points[_points.Count - 1]});
        _lines.Add(tempLine);
        
        for (var i = 1; i < _sides; i++)
        {
            tempLine = Instantiate(_liInterface, transform);
            tempLine.GetComponent<DrawLine>().DrawLineInit(i,
                _width, new[]{_points[i], _points[i - 1]});
            _lines.Add(tempLine);
        }
    }
    // ReSharper disable Unity.PerformanceAnalysis
    private void UpdateLines()
    {
        _points.Clear();
        GetPoints();
        _lines[0].GetComponent<DrawLine>().UpdatePoints(new[]{_points[0], _points[_points.Count - 1]});
        for (var i = 1; i < _sides; i++)
        {
            _lines[i].GetComponent<DrawLine>().UpdatePoints(new[]{_points[i], _points[i - 1]});
        }
    }

    public static void SetRotation()
    {
        Rotation = _sides % 2 != 0 ? -90 : -45;
        _rotationAlpha =  360f / _sides ;
    }
    private void GetPoints()
    {
        _points = new List<Vector3>(_sides);

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
