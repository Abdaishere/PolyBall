using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Main : MonoBehaviour
{
    public GameObject ball;
    public GameObject player;

    public static List<ShapeColor> UsedColors;

    public const int Difficulty = 3;
    private List<ShapeColor> _allColors;
    
    // Start is called before the first frame update
    private void Start ()
    {
        // Add All Colors to a list
        _allColors = new List<ShapeColor>()
        {
            new ShapeColor("Blue", new Color32(0, 128, 204, 255)),
            new ShapeColor("Cyan", new Color32(0, 184, 184, 255)),
            new ShapeColor("Green", new Color32(48, 178, 12, 255)),
            new ShapeColor("Orange", new Color32(196, 101, 0, 255)),
            new ShapeColor("Purple", new Color32(102, 26, 204, 255)),
            new ShapeColor("Red", new Color32(174, 11, 28, 255)),
            new ShapeColor("Yellow", new Color32(189, 154, 0, 255))
        };

        // Add the used Colors
        UsedColors = new List<ShapeColor>();
        for (var i = 0; i < Difficulty; i++)
        {
            AddColor();
        }

        Debug.Log("Spawning Things");
        ball = Instantiate(ball);
        player = Instantiate(player);
    }

    public void AddColor()
    {
        var index = Random.Range(0, _allColors.Count);
        UsedColors.Add(_allColors[index]);
        Debug.Log(_allColors[index].ColorName);
        _allColors.RemoveAt(index);
    }
    
    public void RemoveColor()
    {
        UsedColors.RemoveAt(UsedColors.Count - 1);
    }
    // TODO add and delete sides in main menu
    // private void Update()
    // {
    //     // swipe left
    //     if (Input.GetKeyDown(KeyCode.A))
    //     {
    //         
    //     }
    //     
    //     
    //     // swipe right
    //     if (Input.GetKeyDown(KeyCode.D))
    //     {
    //         
    //     }
    // }
}
