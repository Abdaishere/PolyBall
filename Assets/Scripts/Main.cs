using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Main : MonoBehaviour
{
    public GameObject ball;
    public GameObject player;
    public GameObject scoreText;
    public GameObject highScoreText;

    public static List<Color32> UsedColors;
    
    [Range(3, 64)]
    public static int Difficulty = 3;
    [Range(3f, 4.6f)]
    public const float Radius = 3.0f;
    public const float LineWidth = 0.5f;
    public static bool GameStarted;
    
    private List<Color32> _allColors;

    // Start is called before the first frame update
    private void Start ()
    {
        // Add All 7 Colors to a list
        _allColors = new List<Color32>
        {
            new Color32(0, 128, 204, 255),
            new Color32(0, 184, 184, 255),
            new Color32(48, 178, 12, 255),
            new Color32(196, 101, 0, 255),
             new Color32(102, 26, 204, 255),
            new Color32(174, 11, 28, 255),
             new Color32(189, 154, 0, 255)
        };
        
        // Dynamic Colors
        while (_allColors.Count < Difficulty)
        {
            _allColors.Add(new Color32(
                (byte)Random.Range(0, 256), //Red
                (byte)Random.Range(0, 256), //Green
                (byte)Random.Range(0, 256), //Blue
                255 //Alpha (transparency)
            ));
        }
        // Add the used Colors
        UsedColors = new List<Color32>();
        for (var i = 0; i < Difficulty; i++)
        {
            AddColor();
        }

        highScoreText = Instantiate(highScoreText);
        player = Instantiate(player);
    }

    private void AddColor()
    {
        var index = Random.Range(0, _allColors.Count);
        UsedColors.Add( _allColors[index]);
        _allColors.RemoveAt(index);
    }
    
    public void RemoveColor()
    {
        UsedColors.RemoveAt(UsedColors.Count - 1);
    }
    // TODO add and delete sides in main menu
    private void Update()
    {
        if (!GameStarted && Input.anyKey)
        {
            Destroy(highScoreText);
            scoreText = Instantiate(scoreText);   
            GameStarted = true;
            ball = Instantiate(ball);
        }
    }
}
