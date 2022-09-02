using Player;
using TMPro;
using UnityEngine;
using static Main;

namespace UI
{
    public class Score : MonoBehaviour
    {
        private int _score;
        private int _highScore;
        private TextMeshPro _textMesh;
        private void Start()
        {
            _textMesh = GetComponent<TextMeshPro>();
            UpdateScore();
            _textMesh.text = "Tap to Start";
        }
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.CompareTag("Player")) return;
            
            _score++;
            _textMesh.text = _score.ToString();
        
            if (_score <= _highScore) return;
        
            _highScore = _score;
            PlayerPrefs.SetInt(Difficulty.ToString(), _highScore);
        }
        public void UpdateScore()
        {
            _textMesh.text = "Tap to Start";
            _textMesh.fontSize = Ball.MapNum(Difficulty, 3, 63, 6, 15, 2);
        }
        public void InitScore()
        {
            _highScore = PlayerPrefs.GetInt(Difficulty.ToString(), 0);
            _textMesh.text = "0";
        }
        
    }
}
