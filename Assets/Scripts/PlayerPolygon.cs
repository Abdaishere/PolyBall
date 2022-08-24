using System.Collections.Generic;
using UnityEngine;

public class PlayerPolygon : MonoBehaviour
{
    public float radius = 3;

    public LineRenderer polygonRenderer;
    
    public int extraSteps = 2;
    public bool isLooped;

    private const float Width = 0.5f;
    private int _sides;
    public float rotation;
    public float rotationAlpha;
    private void Start()
    {
        ChangeSides(Main.Difficulty);
    }

    // Update is called once per frame
    private void Update()
    {
        polygonRenderer.startWidth = Width;
        polygonRenderer.endWidth = Width;
        
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
        
        DrawLooped();
    }

    public void ChangeSides(int s)
    {
        _sides = s;
        rotation = _sides switch
        {
            3 => 30,
            4 => 45,
            5 => 54,
            6 => 60,
            7 => 64.3f,
            _ => rotation
        };
        rotationAlpha =  360f / _sides ;
        // SetColors();
    }
    private void DrawLooped()
    {
        polygonRenderer.positionCount = _sides;
        
        const float tau = 2 * Mathf.PI;
        
        for (var currentPoint = 0; currentPoint < _sides; currentPoint++)
        {
            var currentRadian = ((float)currentPoint / _sides) * tau;
            var x = Mathf.Cos(currentRadian) * radius;
            var y = (Mathf.Sin(currentRadian) * radius);
            var newPosition = new Vector3(x, y, 0);
            newPosition = Quaternion.Euler(0, 0, rotation) * newPosition;
            newPosition.y -= 6;
            polygonRenderer.SetPosition(currentPoint, newPosition);
        }
        SetColors();
        polygonRenderer.loop = true;
    }
    // TODO make it stepped gradient or solid colors because fuck line renderer
    private void SetColors()
    {
        var gradient = new Gradient();
        var colors = new List<GradientColorKey>();
        for (var i = 0; i < _sides; i++)
        {
            colors.Add(new GradientColorKey(Main.UsedColors[i].Value, (1f / _sides * (i + 1))+ 0.0001f));
        }

        // gradient.mode = GradientMode.Fixed;
        gradient.colorKeys = colors.ToArray();
        polygonRenderer.colorGradient = gradient;
    }
}
