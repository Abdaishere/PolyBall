using UnityEngine;
using UnityEngine.UI;

public class ScoreMenu : MonoBehaviour
{
    public Text score;
    void Start()
    {
        score.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
        Main.Difficulty = PlayerPrefs.GetInt("Difficulty", 3);
    }
    
    
}
