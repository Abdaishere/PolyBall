using System.Collections;
using TMPro;
using UnityEngine;
using static Main;

namespace UI.Animations
{
    [RequireComponent(typeof(TMP_Text))]
    public class RainbowWord : MonoBehaviour
    {
        private TMP_Text _textMesh;

        private Shaky _shaky;

        // Start is called before the first frame update
        private void Start()
        {
            _textMesh = GetComponent<TMP_Text>();
            _shaky = GetComponent<Shaky>();
            
            StartCoroutine(ColorRainbow());
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
        public void UpdateColors(bool start)
        {
            if (!start)
                StopAllCoroutines();
            else
            {
                StartCoroutine(ColorRainbow());
                if (Difficulty > 12)
                    _shaky.ShakeItBaby(Difficulty / 100f, Difficulty / 80f);
            }
        }
    }
}
