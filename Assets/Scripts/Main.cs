using System.Collections.Generic;
using Player;
using UI;
using UnityEngine;
using Random = UnityEngine.Random;

public class Main : MonoBehaviour
{
    [SerializeField]
    private GameObject ball;
    
    [SerializeField]
    private GameObject player;
    private PlayerPolygon _playerPolygon;
    
    [SerializeField]
    private GameObject titleScreen;

    public static List<Color32> UsedColors;
    
    [Range(3, 63)]
    public static int Difficulty;
    
    public const float LineWidth = 0.5f;
    public static bool GameStarted;
    public static bool GameOver;
    private RainbowWord _rainbowWord;
    
    // Start is called before the first frame update
    private void Start ()
    {
        GameStarted = false;
        GameOver = false;
        Difficulty = PlayerPrefs.GetInt("Difficulty", 5);
        
        // Add the used Colors
        UsedColors = new List<Color32>();
        for (var i = 0; i < Difficulty; i++)
        {
            AddColor();
        }

        titleScreen = Instantiate(titleScreen);
        _rainbowWord = titleScreen.GetComponentInChildren<RainbowWord>();
        
        player = Instantiate(player);
        _playerPolygon = player.GetComponent<PlayerPolygon>();
    }
    // TODO Make a list of different 128 colors that fit well together 
    private static void AddColor()
    {
        if (AllColors.Count == 0)
        {
            // Dynamic Colors
            var color = new Color32(
                (byte)Random.Range(0, 256), //Red
                (byte)Random.Range(0, 256), //Green
                (byte)Random.Range(0, 256), //Blue
                255 //Alpha (transparency)
            );
            UsedColors.Add(color);
        }
        else
        {
            var index = Random.Range(0, AllColors.Count);
            UsedColors.Add(AllColors[index]);
            AllColors.RemoveAt(index);
        }
    }
    private static void RemoveLastColor()
    {
        AllColors.Add(UsedColors[UsedColors.Count - 1]);
        UsedColors.RemoveAt(UsedColors.Count - 1);
    }
    
    private void Update()
    {
        if (GameStarted) return;
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (Difficulty >= 63) return;
            Difficulty+= 2;
            PlayerPolygon.LineBuffer += 2;
            AddColor();
            AddColor();
            _rainbowWord.UpdateColors();
            PlayerPrefs.SetInt("Difficulty", Difficulty);
            return;
        }
        
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (Difficulty <= 3) return;
            Difficulty-= 2;
            PlayerPolygon.LineBuffer -= 2;
            _rainbowWord.UpdateColors();
            RemoveLastColor();
            RemoveLastColor();
            PlayerPrefs.SetInt("Difficulty", Difficulty);
            return;
        }

        if (Difficulty % 2 == 0)
        {
            Difficulty++;
            AddColor();
            _rainbowWord.UpdateColors();
            ++PlayerPolygon.LineBuffer;
            PlayerPrefs.SetInt("Difficulty", Difficulty);
        }
        
        if (!Input.GetKeyDown(KeyCode.Space)) return;
        
        Destroy(titleScreen);

        _playerPolygon.StartGame();
        GameStarted = true;
        
        ball = Instantiate(ball);
        
    }
    
    // Add All 63 Colors to a list
    private static readonly List<Color32> AllColors = new List<Color32>
    {
        new Color32(0, 128, 204, 255),
        new Color32(0, 184, 184, 255),
        new Color32(48, 178, 12, 255),
        new Color32(196, 101, 0, 255),
        new Color32(102, 26, 204, 255),
        new Color32(174, 11, 28, 255),
        new Color32(189, 154, 0, 255)
    };
}
