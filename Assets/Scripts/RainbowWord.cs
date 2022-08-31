using System.Collections;
using UnityEngine;
using TMPro;
using static Main;

[RequireComponent(typeof(TMP_Text))]
public class RainbowWord : MonoBehaviour
{
    private TMP_Text _textMesh;
    // Start is called before the first frame update
    private void Start()
    {
        _textMesh = GetComponent<TMP_Text>();
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

                vertexColors[vertexIndex + 0] = color;
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
}
