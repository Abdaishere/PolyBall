using System;
using System.Collections;
using TMPro;
using UnityEngine;
using static Main;
using Random = UnityEngine.Random;

namespace UI
{
    [RequireComponent(typeof(TMP_Text))]
    public class RainbowWord : MonoBehaviour
    {
        private TMP_Text _textMesh;
        private Vector3 _originalPosition;
        // Start is called before the first frame update
        private void Start()
        {
            _originalPosition = transform.position;
            _textMesh = GetComponent<TMP_Text>();
            UpdateColors();
        }

        private IEnumerator ColorRainbow()
        {
            var start = 0;
            while (true)
            {
                var count = start;
                for (var i = _textMesh.textInfo.characterCount - 1; i >= 0; --i)
                {
                    if (count >= UsedColors.Count)
                        count = 0;
                    var color = UsedColors[count];
                    var meshIndex = _textMesh.textInfo.characterInfo[i].materialReferenceIndex;
                    var vertexIndex = _textMesh.textInfo.characterInfo[i].vertexIndex;
                    var vertexColors = _textMesh.textInfo.meshInfo[meshIndex].colors32;

                    vertexColors[vertexIndex] = color;
                    vertexColors[vertexIndex + 1] = color;
                    vertexColors[vertexIndex + 2] = color;
                    vertexColors[vertexIndex + 3] = color;

                    count++;
                }
                
                _textMesh.UpdateVertexData(TMP_VertexDataUpdateFlags.All);
                yield return new WaitForSeconds(1f / Difficulty);
                start++;
            
                if (start >= UsedColors.Count)
                    start = 0;
            }
            // ReSharper disable once IteratorNeverReturns
        }

        private IEnumerator Shake(float duration, float magnitude)
        {
            var elapsed = 0f;
            while (elapsed < duration)
            {
                var x = Random.Range(-1f, 1f) * magnitude;
                var y = Random.Range(-0.12f, 0.12f) * magnitude;

                transform.position = new Vector3(x, y + _originalPosition.y, 0);
                elapsed += Time.deltaTime;
                yield return 0;
            }
            transform.position = _originalPosition;
        }

        public void UpdateColors()
        {
            StopAllCoroutines();
            StartCoroutine(ColorRainbow());
            
            if (Difficulty > 12)
                StartCoroutine(Shake(Difficulty / 100f, Difficulty / 80f));
        }
    }
}
