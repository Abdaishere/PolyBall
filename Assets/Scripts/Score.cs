using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    private TextMeshPro _textMesh;
    private int _score;
    private int _highScore;
    private CircleCollider2D _cc;
    private const float F = 0.2f;
    private void Start()
    {
        _textMesh = GetComponent<TextMeshPro>();
        _cc = GetComponent<CircleCollider2D>();
        
        _cc.radius = (Main.Radius - Main.LineWidth - F) / 4;
        _highScore = PlayerPrefs.GetInt("HighScore", 0);
        _textMesh.text = "0";
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Player")) return;
        _textMesh.text = (++_score).ToString();
        
        if (_score <= _highScore) return;
        _highScore = _score;
        PlayerPrefs.SetInt("HighScore", _highScore);
    }
}
