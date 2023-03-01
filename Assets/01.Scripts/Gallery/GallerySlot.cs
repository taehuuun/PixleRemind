using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace LTH.ColorMatch.UI
{
    public class GallerySlot : MonoBehaviour
    {
        public TMP_Text titleText;
        public Image thumbnailImb;

        public virtual void OnSlotClick() { }
    }
}
