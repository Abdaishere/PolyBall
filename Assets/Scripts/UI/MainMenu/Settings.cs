using UnityEngine;

namespace UI.MainMenu
{
    public class Settings : MonoBehaviour
    {
        public void SetQuality(int qualitySlider)
        {
            QualitySettings.SetQualityLevel(qualitySlider);
        }
    }
}