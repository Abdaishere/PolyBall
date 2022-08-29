using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPolygon : MonoBehaviour
{
    public float radius;

    public GameObject liInterface;

    public List<Vector3> points;
    public List<GameObject> lines;

    private const float RotationSpeed = 10;
    private float _width;
    private static int _sides;
    public static float Rotation;
    private static float _rTarget = -1;
    private static float _rotationAlpha;
    private Rotator _rotator;
    private const float Tau = 2 * Mathf.PI;
    private void Start()
    {
        _rotator = GetComponent<Rotator>();
        radius = Main.Radius;
        _sides = Main.Difficulty;
        _width = Main.LineWidth;

        GetPoints();
        CreateLines();
    }

    // Update is called once per frame
    private void Update()
    {
        if (RotationAnimation())
        {
            Rotation = -_rTarget;
            _rotator.rotate = false;
        }
        
        // controls 
        if (Input.GetKeyDown(KeyCode.D) || Input.GetMouseButtonDown(1))
        {
            Rotation = _rTarget;
            _rTarget -= _rotationAlpha;
            if (_rTarget < 0)
            {
                _rTarget += 360;
            }
        }

        if (Input.GetKeyDown(KeyCode.A) || Input.GetMouseButtonDown(0))
        {
            Rotation = _rTarget;
            _rTarget += _rotationAlpha;
            if (_rTarget > 360)
            {
                _rTarget -= 360;
            }
        }
        
        UpdateLines();
    }

    private void CreateLines()
    {
        var tempLine = Instantiate(liInterface, transform);
        tempLine.GetComponent<DrawLine>().DrawLineInit(0,
            _width, new[]{points[0], points[points.Count - 1]});
        lines.Add(tempLine);
        
        for (var i = 1; i < _sides; i++)
        {
            tempLine = Instantiate(liInterface, transform);
            tempLine.GetComponent<DrawLine>().DrawLineInit(i,
                _width, new[]{points[i], points[i - 1]});
            lines.Add(tempLine);
        }
    }
    // ReSharper disable Unity.PerformanceAnalysis
    private void UpdateLines()
    {
        points.Clear();
        GetPoints();
        lines[0].GetComponent<DrawLine>().UpdatePoints(new[]{points[0], points[points.Count - 1]});
        for (var i = 1; i < _sides; i++)
        {
            lines[i].GetComponent<DrawLine>().UpdatePoints(new[]{points[i], points[i - 1]});
        }
    }
    // return Done ?
    private static Boolean RotationAnimation()
    {
        if (Rotation > _rTarget) return true;
        Rotation += RotationSpeed;
        return false;
    }

    public static void SetRotation()
    {
        _rTarget = _sides % 2 != 0 ? -90 : -45;
        _rotationAlpha =  360f / _sides ;
    }
    private void GetPoints()
    {
        points = new List<Vector3>(_sides);
        const int newPositionY = 5;
        for (var currentPoint = 0; currentPoint < _sides; currentPoint++)
        {
            var currentRadian = ((float)currentPoint / _sides) * Tau;
            var x = Mathf.Cos(currentRadian) * radius;
            var y = (Mathf.Sin(currentRadian) * radius);
            var newPosition = new Vector3(x, y, 0);
            newPosition = Quaternion.Euler(0, 0, Rotation) * newPosition;
            newPosition.y -= newPositionY;
            points.Add(newPosition);
        }
    }
}
