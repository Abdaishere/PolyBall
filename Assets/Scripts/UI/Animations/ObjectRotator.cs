using UnityEngine;

namespace UI.Animations
{
    public class ObjectRotator : MonoBehaviour
    {
        private const float Speed = -130f;

        // Update is called once per frame
        private void Update()
        {
            transform.Rotate(0f, 0f, Speed * Time.deltaTime);
        }
    }
}