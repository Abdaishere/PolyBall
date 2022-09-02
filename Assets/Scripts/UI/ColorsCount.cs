using Player;
using TMPro;
using UnityEngine;
using static Main;

namespace UI
{
    [RequireComponent(typeof(TextMeshPro))]
    public class ColorsCount : MonoBehaviour
    {
    
        private static TextMeshPro _textMesh;
        private void Start()
        {
            _textMesh = GetComponent<TextMeshPro>();
            UpdateHighScore();
        }
        public static void UpdateHighScore()
        {
            if (Difficulty > 12)
            {
                var r = (byte)(255 - Ball.MapNum(Difficulty, 11, 63, 0, 255, 2));
                _textMesh.color = new Color32(255, r, r, 255);
            }
            else
                _textMesh.color = Color.white;
            
            _textMesh.text = $"Colors: {Difficulty.ToString()}";
        }
    }
}
