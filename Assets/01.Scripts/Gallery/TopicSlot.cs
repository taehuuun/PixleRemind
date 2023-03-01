using UnityEngine;

namespace LTH.ColorMatch.UI
{
    public class TopicSlot : GallerySlot
    {
        public override void OnSlotClick()
        {
            PlayerPrefs.SetString("Topic",titleText.text);
            PlayerPrefs.Save();
            
            Debug.Log($"Topic Slot Click : {titleText.text}");
        }
    }
}
