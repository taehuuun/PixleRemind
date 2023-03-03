using UnityEngine;
using UnityEngine.UI;

namespace LTH.ColorMatch
{
    public class ColorSlot : MonoBehaviour
    {
        public Image slotImage; 
        public string slotHexCode;

        public void SetSlot(Color color)
        {
            slotImage.color = color;
            slotHexCode = ColorUtility.ToHtmlStringRGB(color);
        }
    }
}
