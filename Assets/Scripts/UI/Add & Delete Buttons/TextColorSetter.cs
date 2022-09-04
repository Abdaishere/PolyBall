using System;
using TMPro;
using UnityEngine;

namespace UI.Add___Delete_Buttons
{
    public class TextColorSetter : MonoBehaviour
    {
        private TextMeshProUGUI text;

        public TextMeshProUGUI Text => text;

        private void Start()
        {
            text = GetComponentInChildren<TextMeshProUGUI>();
        }
    }
}
