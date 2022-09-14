using System.Collections.Generic;
using System.Linq;
using Player;
using UI.Add___Delete_Buttons;
using UI.Animations;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(AudioSource))]
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
    
    private static AudioSource _scrambleAudioSource;
    
    public static List<Color32> UsedColors;
    
    [Range(3, 63)]
    public static int Difficulty;
    
    public const float LineWidth = 0.52f;
    public static bool GameStarted;
    public static bool GameOver;
    private static RainbowWord _rainbowWord;
    
    // Add All 63 Colors to a list
    private static List<Color32> _allColors;
    
    // Color value steps
    // TODO change the system to a 3d array of r g b to chose from each time
    [Range(1, 4)]
    private static int _colorSteps = 4;

    // Start is called before the first frame update
    private void Start ()
    {
        _scrambleAudioSource = GetComponent<AudioSource>();
        
        GameStarted = false;
        GameOver = false;
        Difficulty = PlayerPrefs.GetInt("Difficulty", 7);
     
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
        if (_allColors.Count == 0)
        {
            // Dynamic Colors
            var color = new Color32(
                (byte)(Random.Range(0, 256 / _colorSteps) * _colorSteps), //Red
                (byte)(Random.Range(0, 256 / _colorSteps) * _colorSteps), //Green
                (byte)(Random.Range(0, 256 / _colorSteps) * _colorSteps), //Blue
                255 //Alpha (transparency)
            );
            
            while (true) {
                if (UsedColors.Any(col => col.r == color.r || col.g == color.g || col.b == color.b))
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
            var index = Random.Range(0, _allColors.Count);
            UsedColors.Add(_allColors[index]);
            _allColors.RemoveAt(index);
        }
    }
    private static void RemoveLastColor()
    {
        _allColors.Add(UsedColors[UsedColors.Count - 1]);
        UsedColors.RemoveAt(UsedColors.Count - 1);
    }

    public static void ChangeAllColors()
    {
        if (GameStarted) return;
        
        _scrambleAudioSource.Play();
        _colorSteps = _colorSteps == 4 ? 3 : 4;
        _rainbowWord.UpdateColors(false);
        
        _allColors.Clear();
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
            ButtonsManager.Add();
            return;
        }
        
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ButtonsManager.Remove();
            return;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {
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
