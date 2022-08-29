using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public TextMeshPro textMesh;
    public int score;
    public int highScore;
    public CircleCollider2D cc;
    private const float F = 0.2f;
    private void Start()
    {
        cc.radius = (Main.Radius - Main.LineWidth - F) / 4;
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
