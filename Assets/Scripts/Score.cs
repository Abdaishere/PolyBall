using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    private TextMeshPro _textMesh;
    private int _score;
    private int _highScore;
    private void Start()
    {
        _textMesh = GetComponent<TextMeshPro>();

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
