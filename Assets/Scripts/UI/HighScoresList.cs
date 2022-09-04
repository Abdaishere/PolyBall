using TMPro;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(Transform))]
    public class HighScoresList : MonoBehaviour
    {
        [SerializeField] private GameObject item;

        private void Start()
        {
            for (var i = 3; i <= 63; i += 2)
            {
                var newItem = Instantiate(item, transform);
                newItem.AddComponent<ItemScript>();
                newItem.GetComponent<ItemScript>().LoadHighScore(i);
            }
        }
    }
}
