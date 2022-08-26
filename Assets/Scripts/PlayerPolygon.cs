using System.Collections.Generic;
using UnityEngine;

public class PlayerPolygon : MonoBehaviour
{
    public float radius;

    public GameObject liInterface;

    public List<Vector3> points;
    public List<GameObject> lines;

    private float _width;
    private int _sides;
    public float rotation;
    public float rotationAlpha;
    private const float Tau = 2 * Mathf.PI;
    private void Start()
    {
        radius = Main.Radius;
        _sides = Main.Difficulty;
        _width = Main.LineWidth;
        
        SetRotation();
        GetPoints();
        CreateLines();
    }

    // Update is called once per frame
    private void Update()
    {
        // controls 
        if (Input.GetKeyDown(KeyCode.D))
        {
            rotation -= rotationAlpha;
            if (rotation < 0)
            {
                rotation += 360;
            }
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            rotation += rotationAlpha;
            if (rotation > 360)
            {
                rotation -= 360;
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

    private void SetRotation()
    {
        rotation = _sides % 2 != 0 ? -90 : -45;
        rotationAlpha =  360f / _sides ;
    }
    private void GetPoints()
    {
        points = new List<Vector3>(_sides);

        for (var currentPoint = 0; currentPoint < _sides; currentPoint++)
        {
            var currentRadian = ((float)currentPoint / _sides) * Tau;
            var x = Mathf.Cos(currentRadian) * radius;
            var y = (Mathf.Sin(currentRadian) * radius);
            var newPosition = new Vector3(x, y, 0);
            newPosition = Quaternion.Euler(0, 0, rotation) * newPosition;
            newPosition.y -= 6;
            points.Add(newPosition);
        }
    }
}
