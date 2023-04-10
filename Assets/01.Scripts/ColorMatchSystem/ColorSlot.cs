using System;
using UnityEngine;
using UnityEngine.UI;

namespace LTH.ColorMatch
{
    [Serializable]
    public class ColorSlot
    {
        public Image slotImage; 

        public void SetSlot(Color color)
        {
            slotImage.color = color;
        }
    }
}
