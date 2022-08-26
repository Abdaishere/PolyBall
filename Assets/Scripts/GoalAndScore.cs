using TMPro;
using UnityEngine;

public class GoalAndScore : MonoBehaviour
{
    public TextMeshPro textMesh;
    public int score;
    public int highScore;
    private void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        textMesh.text = $"High Score\n {highScore.ToString()}";
    }

    public void LocalScore()
    {
        textMesh.text = "0";
        var radius = Main.Radius - Main.LineWidth;
        var transform1 = transform;
        transform1.localScale = new Vector3(radius, radius);
        transform1.position = new Vector3(0, -6, 0);
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
