using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textMeshPro;

    private void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
        textMeshPro.text = "Loading.....";
    }

    public void LoadHighScore(int diff)
    {
        textMeshPro.text =   $"Colors : {diff} High Score : {PlayerPrefs.GetInt(diff.ToString(), 0)}";
    }
}
