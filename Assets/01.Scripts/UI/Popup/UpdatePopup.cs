using System.Collections.Generic;
using LTH.PixelRemind.Data;
using LTH.PixelRemind.UI.Slots;
using UnityEngine;
using UnityEngine.UI;

namespace LTH.PixelRemind.UI
{
    public class UpdatePopup : CloseAbleUI
    {
        public Transform contentParent;
        public TopicSlot topicSlotPrefab;
        public Button updateButton;

        private List<TopicSlot> _topicSlots = new List<TopicSlot>();
        private List<string> _selectedTopicIDs = new List<string>();

        private void Start()
        {
            // updateButton.onClick.AddListener(OnUpdateButtonClicked);
        }

        public void Show(List<TopicData> topicDataList)
        {
            foreach (var topicSlot in _topicSlots)
            {
                Destroy(topicSlot.gameObject);
            }
            
            _topicSlots.Clear();

            foreach (var topicData in topicDataList)
            {
                var topicSlot = Instantiate(topicSlotPrefab, contentParent);
                
                // topicSlot.SetSlot(topicData);
            }
        }
    }
}
