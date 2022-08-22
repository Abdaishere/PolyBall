using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public GameObject ball;
    
    public static List<ShapeColor> UsedColors;

    private const int ColorsSize = 5;
    private List<ShapeColor> _allColors;
    
    // Start is called before the first frame update
    private void Start ()
    {
        // Add All Colors to a list
        _allColors = new List<ShapeColor>(7)
        {
            new ShapeColor("Blue", new Color(0, 133, 255)),
            new ShapeColor("Cyan", new Color(0, 208, 208)),
            new ShapeColor("Green", new Color(43, 196, 11)),
            new ShapeColor("Orange", new Color(243, 105, 0)),
            new ShapeColor("Purple", new Color(106, 27, 255)),
            new ShapeColor("Red", new Color(185, 10, 25)),
            new ShapeColor("Yellow", new Color(231, 175, 0))
        };

        // Add the used Colors
        UsedColors = new List<ShapeColor>(ColorsSize);
        for (var i = 0; i < ColorsSize; i++)
        {
            var index = Random.Range(0, _allColors.Count);
            UsedColors.Add(_allColors[index]);
            Debug.Log(_allColors[index].ColorName);
            _allColors.RemoveAt(index);
        }

        Debug.Log("Spawning Things");
        ball = Instantiate(ball);
    }
}
