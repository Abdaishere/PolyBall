using UnityEngine;

namespace UI.MainMenu
{
    public class BackButton : MonoBehaviour
    {
        public void Back()
        {
            MenuManager.Pause();
        }
    }
}
