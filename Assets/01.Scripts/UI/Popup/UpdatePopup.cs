using System.Collections.Generic;
using System.Linq;
using LTH.PixelRemind.Data;
using LTH.PixelRemind.Managers;
using LTH.PixelRemind.Managers.Data;
using LTH.PixelRemind.UI.Slots;
using UnityEngine;
using UnityEngine.UI;

namespace LTH.PixelRemind.UI
{
    public class UpdatePopup : CloseAbleUI
    {
        public Transform contentParent;
        public UpdateSlot updateTopicSlotPrefab;
        public Button updateButton;
        public Button closeButton;

        public UpdateManager updateManager;

        private List<UpdateSlot> _topicSlots = new List<UpdateSlot>();

        private void Start()
        {
            updateButton.onClick.AddListener(OnUpdateButtonClicked);
            closeButton.onClick.AddListener(OnCloseButtonClicked);
        }

        public void Show(List<TopicData> updateDataList, List<TopicData> missingDataList)
        {
            gameObject.SetActive(true);
            
            foreach (var topicSlot in _topicSlots)
            {
                Destroy(topicSlot.gameObject);
            }
            
            _topicSlots.Clear();

            foreach (var topicData in updateDataList)
            {
                var topicSlot = Instantiate(updateTopicSlotPrefab, contentParent);
                topicSlot.SetSlot(topicData,false);
                _topicSlots.Add(topicSlot);
            }

            foreach (var topicData in missingDataList)
            {
                var topicSlot = Instantiate(updateTopicSlotPrefab, contentParent);
                topicSlot.SetSlot(topicData, true);
                _topicSlots.Add(topicSlot);
            }
        }

        private async void OnUpdateButtonClicked()
        {
            var selectedSlots = _topicSlots.Where(slot => slot.IsSelected).ToList();
            
            Debug.Log($"!@#!@#@!#!@# : {selectedSlots ==null}");
            Debug.Log($"!@#!@#@!#!@# : {selectedSlots ==null}");
            
            if (DataManager.Instance.userData.LocalTopicDataIDs.Count == 0)
            {
                Debug.Log("최소 1개 이상 슬롯을 선택 해야 합니다.");
                return;
            }

            foreach (var slot in selectedSlots)
            {
                Debug.Log($"선택된 Topic : {slot.titleText.text}");
                await updateManager.DownloadTopicData(slot.GetTopicData().ID);
            }
        }

        private void OnCloseButtonClicked()
        {
            var selectedSlots = _topicSlots.Where(slot => slot.IsSelected).ToList();
            
            Debug.Log($"2222222222222222 : {selectedSlots ==null}");
            
            if (DataManager.Instance.userData.LocalTopicDataIDs.Count == 0)
            {
                Debug.Log("최소 1개 이상 슬롯을 선택 해야 합니다.");
                return;
            }
            
            gameObject.SetActive(false);
        }
    }
}
