using TMPro;
using UnityEngine;

namespace UI
{
    public class ButtonsManager : MonoBehaviour
    {
        [SerializeField] private GameObject btnAdd;
        [SerializeField] private GameObject btnRemove;

        private static readonly Color32 AddEnabled = new Color32(0,255,0,255);
        private static readonly Color32 AddDisabled = new Color32(0,110,0,255);
        private static readonly Color32 RemoveEnabled = new Color32(255,0,0,255);
        private static readonly Color32 RemoveDisabled = new Color32(150,0,0,255);

        private void Start()
        {
            switch (Main.Difficulty)
            {
                case 63:
                    btnAdd.GetComponent<TextMeshPro>().color = AddDisabled;
                    break;
                case 3:
                    btnRemove.GetComponent<TextMeshPro>().color = RemoveDisabled;
                    break;
            }
            
            btnAdd = Instantiate(btnAdd, transform);
            btnRemove = Instantiate(btnRemove, transform);
        }

        public void Add()
        {
            Main.Add();
            
            switch (Main.Difficulty)
            {
                case 63:
                    btnAdd.GetComponent<TextMeshPro>().color = AddDisabled;
                    break;
                case 5:
                    btnRemove.GetComponent<TextMeshPro>().color = RemoveEnabled;
                    break;
            }
            
        }
        
        public void Remove()
        {
            Main.Remove();
            
            switch (Main.Difficulty)
            {
                case 3:
                    btnRemove.GetComponent<TextMeshPro>().color = RemoveDisabled;
                    break;
                case 61:
                    btnAdd.GetComponent<TextMeshPro>().color = AddEnabled;
                    break;
            }
            
        }

        public void ChangeAllColors()
        {
            Main.ChangeAllColors();
        }
        
    }
}