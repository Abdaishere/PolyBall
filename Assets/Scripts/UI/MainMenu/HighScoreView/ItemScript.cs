using Player;
using TMPro;
using UnityEngine;

namespace UI.MainMenu.HighScoreView
{
    public class ItemScript : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI textMesh;
        
        public void LoadHighScore(int diff)
        {
            Color32 color;
            if (diff > 12)
            {
                var r = (byte)(255 - Ball.MapNum(diff, 0, 63, 0, 255, 2));
                color = new Color32(255, r, r, 255);
            }
            else
                color = Color.white;
            
            var tmpText = $"<color=#{ColorUtility.ToHtmlStringRGB(color)}>Colors: {diff.ToString()} </color> ";

            var highScore = PlayerPrefs.GetInt(diff.ToString(), 0);
            var highScoreColor = highScore > 50
                ? ColorUtility.ToHtmlStringRGB(Color.green)
                : ColorUtility.ToHtmlStringRGB(Color.white);
            
            tmpText += $"\t<color=#{highScoreColor}>High Score: {highScore.ToString()}</color>";
            
            textMesh.text = tmpText;
        }
    }
}
