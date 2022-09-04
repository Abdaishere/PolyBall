using TMPro;
using UnityEngine;
using static Main;

namespace UI.Add___Delete_Buttons
{
    public class ButtonsManager : MonoBehaviour
    {
        [SerializeField] private GameObject btnAdd;
        [SerializeField] private GameObject btnRemove;

        private static readonly Color32 AddEnabled = new Color32(0,255,0,255);
        private static readonly Color32 AddDisabled = new Color32(0,80,0,255);
        private static readonly Color32 RemoveEnabled = new Color32(255,0,0,255);
        private static readonly Color32 RemoveDisabled = new Color32(90,0,0,255);
        
        private static TextColorSetter _addColor;
        private static TextColorSetter _removeColor;
        private void Start()
        {
            btnAdd = Instantiate(btnAdd, transform);
            btnRemove = Instantiate(btnRemove, transform);

            _addColor = btnAdd.GetComponent<TextColorSetter>();
            _removeColor = btnRemove.GetComponent<TextColorSetter>();
            
            switch (Difficulty)
            {
                case 63:
                    _addColor.Text.color = AddDisabled;
                    break;
                case 3:
                    _removeColor.Text.color = RemoveDisabled;
                    break;
            }
        }

        public static void Add()
        {
            if (Difficulty == 63) return; 
            Main.Add();
            
            switch (Difficulty)
            {
                case 63:
                    _addColor.Text.color = AddDisabled;
                    break;
                case 5:
                    _removeColor.Text.color = RemoveEnabled;
                    break;
            }
            
        }
        
        public static void Remove()
        {
            if (Difficulty == 3) return; 
            
            Main.Remove();
            
            switch (Difficulty)
            {
                case 3:
                    _removeColor.Text.color = RemoveDisabled;
                    break;
                case 61:
                    _addColor.Text.color = AddEnabled;
                    break;
            }
            
        }

        public void ChangeAllColors()
        {
            Main.ChangeAllColors();
        }
        
    }
}