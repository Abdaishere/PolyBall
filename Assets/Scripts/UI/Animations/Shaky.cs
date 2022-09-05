using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace UI.Animations
{
    public class Shaky : MonoBehaviour
    {
        private Vector3 _origin;

        private void Start()
        {
            _origin = transform.position;
        }

        public void ShakeItBaby(float duration, float magnitude )
        {
            StartCoroutine(Shake(duration, magnitude));
        }
        private IEnumerator<int> Shake(float duration, float magnitude)
        {
            var elapsed = 0f;
            while (elapsed < duration)
            {
                var x = Random.Range(-1f, 1f) * magnitude;
                var y = Random.Range(-0.12f, 0.12f) * magnitude;

                transform.position = new Vector3(x, y + _origin.y, _origin.z);
                elapsed += Time.deltaTime;
                yield return 0;
            }
            transform.position = _origin;
        }
    }
}
