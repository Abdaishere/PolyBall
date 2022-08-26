using System;
using TMPro;
using UnityEditor.UIElements;
using UnityEngine;

public class HighScore : MonoBehaviour
{
    public TextMeshPro textMesh;
    public  float speed;
    private float _timer;
    private void Start()
    {
        textMesh.text = $"High Score\n {PlayerPrefs.GetInt("HighScore", 0).ToString()}";
    }

    // private void Update()
    // {
    //     transform.position +=new Vector3(0, speed, 0);
    //     _timer += Time.deltaTime;
    //     if (_timer > 1f)
    //     {
    //         _timer = 0;
    //         speed *= -1;
    //     }
    // }
}
