using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace LTH.PixelRemind.UI
{
    public class GallerySlot : MonoBehaviour
    {
        public TMP_Text titleText;
        public Image thumbnailImb;
        
        public virtual void OnSlotClick() { }
        public virtual void SetSlot() { }
    }
}
