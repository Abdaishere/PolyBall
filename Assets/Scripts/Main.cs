using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public GameObject ball;
    public GameObject player;
    public GameObject scoreText;
    public GameObject highScoreText;

    public static List<Color32> UsedColors;
    
    [Range(3, 64)]
    public static int Difficulty;
    [Range(3f, 4.6f)]
    public const float Radius = 3.0f;
    public const float LineWidth = 0.5f;
    public static bool GameStarted;
    
    private List<Color32> _allColors;
    private PlayerPolygon _playerPolygon;

    // Start is called before the first frame update
    private void Start ()
    {
        _playerPolygon = player.GetComponent<PlayerPolygon>();
        
        Difficulty = PlayerPrefs.GetInt("Difficulty", 5);
        GameStarted = false;
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
        if (_allColors.Count == 0)
        {
            // Dynamic Colors
            UsedColors.Add(new Color32(
                    (byte)Random.Range(0, 256), //Red
                    (byte)Random.Range(0, 256), //Green
                    (byte)Random.Range(0, 256), //Blue
                    255 //Alpha (transparency)
                ));
        }
        else
        {
            var index = Random.Range(0, _allColors.Count);
            UsedColors.Add(_allColors[index]);
            _allColors.RemoveAt(index);
        }
    }

    private static void RemoveLastColor()
    {
        UsedColors.RemoveAt(UsedColors.Count - 1);
    }
    
    private void Update()
    {
        if (GameStarted) return;
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (Difficulty >= 64) return;
            Difficulty++;
            AddColor();
            ++PlayerPolygon.LineBuffer;
            return;
        }
        
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (Difficulty <= 3) return;
            Difficulty--;
            --PlayerPolygon.LineBuffer;
            RemoveLastColor();
            return;
        }

        if (!Input.GetKeyDown(KeyCode.Space)) return;
        Destroy(highScoreText);
        scoreText = Instantiate(scoreText);   
        GameStarted = true;
        ball = Instantiate(ball);
    }
}
