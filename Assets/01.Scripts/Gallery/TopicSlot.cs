using LTH.ColorMatch.Data;
using LTH.ColorMatch.Managers;
using UnityEngine;

namespace LTH.ColorMatch.UI
{
    public class TopicSlot : GallerySlot
    {
        public TopicData data;
        public override void OnSlotClick()
        {
            GalleryManager.ins.selectedTopic = titleText.text;
            Debug.Log($"Topic Slot Click : {titleText.text}");
        }
    }
}
