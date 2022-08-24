using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Main : MonoBehaviour
{
    public GameObject ball;
    public GameObject player;

    public static List<ShapeColor> UsedColors;

    public  static readonly int Difficulty = 7;
    private List<ShapeColor> _allColors;
    
    // Start is called before the first frame update
    private void Start ()
    {
        // Add All 7 Colors to a list
        _allColors = new List<ShapeColor>()
        {
            new ShapeColor(0, new Color32(0, 128, 204, 255)),
            new ShapeColor(1, new Color32(0, 184, 184, 255)),
            new ShapeColor(2, new Color32(48, 178, 12, 255)),
            new ShapeColor(3, new Color32(196, 101, 0, 255)),
            new ShapeColor(4, new Color32(102, 26, 204, 255)),
            new ShapeColor(5, new Color32(174, 11, 28, 255)),
            new ShapeColor(6, new Color32(189, 154, 0, 255))
        };
        
        // Dynamic Colors
        while (_allColors.Count < Difficulty)
        {
            _allColors.Add(new ShapeColor((_allColors.Count - 1),new Color32(
                (byte)Random.Range(0, 255), //Red
                (byte)Random.Range(0, 255), //Green
                (byte)Random.Range(0, 255), //Blue
                255 //Alpha (transparency)
            )));
        }
        // Add the used Colors
        UsedColors = new List<ShapeColor>();
        for (var i = 0; i < Difficulty; i++)
        {
            AddColor();
        }
        
        ball = Instantiate(ball);
        player = Instantiate(player);
    }

    private void AddColor()
    {
        var index = Random.Range(0, _allColors.Count);
        UsedColors.Add(_allColors[index]);
        _allColors.RemoveAt(index);
    }
    
    public void RemoveColor()
    {
        UsedColors.RemoveAt(UsedColors.Count - 1);
    }
    // TODO add and delete sides in main menu
    
}
