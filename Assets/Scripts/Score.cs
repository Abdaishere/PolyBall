using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public TextMeshPro textMesh;
    public int score;
    public int highScore;
    public void LocalScore()
    {
        textMesh.text = "0";
        const float radius = Main.Radius - Main.LineWidth;
        transform.localScale = new Vector3(radius, radius);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            textMesh.text = (++score).ToString();
            if (score > highScore)
            {
                highScore = score;
                PlayerPrefs.SetInt("HighScore", highScore);
            }
        }
    }
}
