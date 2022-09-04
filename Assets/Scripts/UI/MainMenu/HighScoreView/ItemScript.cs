using TMPro;
using UnityEngine;

namespace UI.MainMenu.HighScoreView
{
    public class ItemScript : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI textMeshPro;
        private void Start()
        {
            textMeshPro.text = "Loading.....";
        }

        public void LoadHighScore(int diff)
        {
            textMeshPro.text =   $"Colors : {diff} High Score : {PlayerPrefs.GetInt(diff.ToString(), 0)}";
        }
    }
}
