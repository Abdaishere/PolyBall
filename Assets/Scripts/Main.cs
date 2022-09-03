using System.Collections.Generic;
using System.Linq;
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

    [SerializeField]
    private new GameObject camera;
    private static Shaky _shaky;

    public static List<Color32> UsedColors;
    
    [Range(3, 63)]
    public static int Difficulty;
    
    public const float LineWidth = 0.5f;
    public static bool GameStarted;
    public static bool GameOver;
    private static RainbowWord _rainbowWord;
    
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
    // Color value steps
    [Range(1, 4)]
    private static int _colorSteps = 2;
    
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

        // camera = Instantiate(camera);
        _shaky = camera.GetComponent<Shaky>();
    }
    // TODO Make a list of different 128 colors that fit well together 
    private static void AddColor()
    {
        
        if (AllColors.Count == 0)
        {
            // Dynamic Colors
            var color = new Color32(
                (byte)(Random.Range(0, 256 / _colorSteps) * _colorSteps), //Red
                (byte)(Random.Range(0, 256 / _colorSteps) * _colorSteps), //Green
                (byte)(Random.Range(0, 256 / _colorSteps) * _colorSteps), //Blue
                255 //Alpha (transparency)
            );
            
            while (true) {
                if (AllColors.Any(col => col.Equals(color)))
                    
                    color = new Color32(
                        (byte)(Random.Range(0, 256 / _colorSteps) * _colorSteps), //Red
                        (byte)(Random.Range(0, 256 / _colorSteps) * _colorSteps), //Green
                        (byte)(Random.Range(0, 256 / _colorSteps) * _colorSteps), //Blue
                        255 //Alpha (transparency)
                    );
                
                else
                    break;
            }

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

    public static void ChangeAllColors()
    {
        if (GameStarted) return;
        
        if (_colorSteps >= 4)
            _colorSteps = 1;
        else
            _colorSteps++;
        
        Debug.Log(_colorSteps);
        _rainbowWord.UpdateColors(false);
        
        AllColors.Clear();
        UsedColors.Clear();
        _shaky.ShakeItBaby(0.1f, 2.5f);
        
        for (var i = 0; i < Difficulty; i++)
            AddColor();
        
        PlayerPolygon.UpdateColors();
        _rainbowWord.UpdateColors(true);
    }
    
    private void Update()
    {
        if (GameStarted) return;
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Add();
            return;
        }
        
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Remove();
            return;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            ChangeAllColors();
            return;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (_colorSteps == 1)
                _colorSteps = 4;
            else
                _colorSteps-= 2;
            
            ChangeAllColors();
            return; 
        }

        if (Difficulty % 2 == 0)
        {
            Difficulty++;
            
            _rainbowWord.UpdateColors(false);
            AddColor();
            _rainbowWord.UpdateColors(true);
            
            ++PlayerPolygon.LineBuffer;
            PlayerPrefs.SetInt("Difficulty", Difficulty);
        }
        
        if (!Input.GetKeyDown(KeyCode.Space)) return;
        
        Destroy(titleScreen);

        _playerPolygon.StartGame();
        GameStarted = true;
        
        ball = Instantiate(ball);
        
    }

    public static void Add()
    {
        if (GameStarted) return;
        if (Difficulty >= 63) return;
        Difficulty += 2;
        PlayerPolygon.LineBuffer += 2;

        _rainbowWord.UpdateColors(false);
        AddColor();
        AddColor();
        _rainbowWord.UpdateColors(true);

        PlayerPrefs.SetInt("Difficulty", Difficulty);
    }
    
    public static void Remove()
    {
        if (GameStarted) return;
        if (Difficulty <= 3) return;
        Difficulty-= 2;
        PlayerPolygon.LineBuffer -= 2;
            
        _rainbowWord.UpdateColors(false);
        RemoveLastColor();
        RemoveLastColor();
        _rainbowWord.UpdateColors(true);
            
        PlayerPrefs.SetInt("Difficulty", Difficulty);
    }
}
