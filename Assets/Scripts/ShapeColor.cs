using UnityEngine;

public class ShapeColor
{
    public readonly int ColorIndex;
    public Color32 Value;
    
    public ShapeColor(int colorIndex, Color value)
    {
        ColorIndex = colorIndex;
        Value = value;
    }
}
