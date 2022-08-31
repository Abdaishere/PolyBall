using TMPro;
using UnityEngine;

public class HighScore : MonoBehaviour
{
    public TextMeshPro textMesh;
    private void Start()
    {
        textMesh.text = $"{PlayerPrefs.GetInt("HighScore", 0).ToString()}";
    }
}
