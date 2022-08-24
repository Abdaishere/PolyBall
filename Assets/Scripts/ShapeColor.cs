using System;
using UnityEngine;

public class ShapeColor
{
    public readonly String ColorName;
    public Color32 Value;
    
    public ShapeColor(string colorName, Color value)
    {
        this.ColorName = colorName;
        Value = value;
    }
}
