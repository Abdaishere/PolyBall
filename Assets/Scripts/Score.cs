using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public TextMeshPro textMesh;
    public int score;
    public int highScore;

    private void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        textMesh.text = "0";
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Player")) return;
        textMesh.text = (++score).ToString();
        
        if (score <= highScore) return;
        highScore = score;
        PlayerPrefs.SetInt("HighScore", highScore);
    }
}
