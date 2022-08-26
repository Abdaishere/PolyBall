using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighScore : MonoBehaviour
{
    public TextMeshPro textMesh;

    private void Start()
    {
        textMesh.text = $"High Score\n {PlayerPrefs.GetInt("HighScore", 0).ToString()}";
    }
}
