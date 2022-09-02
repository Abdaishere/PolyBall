using Player;
using TMPro;
using UnityEngine;
using static Main;

namespace UI
{
    [RequireComponent(typeof(TextMeshPro))]
    public class HighScore : MonoBehaviour
    {
        private static TextMeshPro _textMesh;
        private void Start()
        {
            _textMesh = GetComponent<TextMeshPro>();
            UpdateHighScore();
        }
        public static void UpdateHighScore()
        {
            _textMesh.text = PlayerPrefs.GetInt(Difficulty.ToString(), 0).ToString();
        }
    }
}
