using UnityEngine;

namespace UI.MainMenu.HighScoreView
{
    [RequireComponent(typeof(Transform))]
    public class HighScoresList : MonoBehaviour
    {
        [SerializeField] private GameObject item;
        
        [SerializeField] private GameObject content;
        public void UpdateList()
        {
            for (var i = 3; i <= 63; i += 2)
            {
                var newItem = Instantiate(item, content.transform);
                newItem.GetComponent<ItemScript>().LoadHighScore(i);
            }
        }
    }
}
