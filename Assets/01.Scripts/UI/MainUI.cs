using System.Collections;
using System.Collections.Generic;
using LTH.PixelRemind.Data;
using LTH.PixelRemind.Managers.Data;
using LTH.PixelRemind.Managers.Data.Paths;
using LTH.PixelRemind.Managers.Gallery;
using LTH.PixelRemind.UI.Slots;
using UnityEngine;
using UnityEngine.UI;

namespace LTH.PixelRemind.UI
{
    public class MainUI : BodyUI
    {
        public Button playBtn;
        
        public Transform topicSlotContainer;
        public Transform pixelArtSlotContainer;
        
        public TopicSlot topicSlotPrefab;
        public PixelArtSlot pixelArtSlotPrefab;
        
        public GameObject galleryScreen;
        
        private void Start()
        {
            StartCoroutine(SetTopicSlot());
        }

        private IEnumerator SetTopicSlot()
        {
            List<string> localTopicIds = DataManager.Instance.userData.LocalTopicDataIDs;

            for (int i = 0; i < localTopicIds.Count; i++)
            {
                TopicSlot topicSlot = Instantiate(topicSlotPrefab, topicSlotContainer);
                topicSlot.data = DataManager.LoadJsonData<TopicData>(DataPath.GalleryDataPath, localTopicIds[i]);
                topicSlot.name = topicSlot.data.Title;
                topicSlot.OnClick += HandleTopicSlotClick;
            }
            
            yield break;
        }

        private void HandleTopicSlotClick(TopicData clickedTopicData)
        {
            GalleryManager.ins.LoadPixelDataForTopic(clickedTopicData);
            galleryScreen.SetActive(true);

            foreach (Transform child in pixelArtSlotContainer)
            {
                Destroy(child.gameObject);
            }

            foreach (var pixelArtData in clickedTopicData.PixelArtDatas)
            {
                PixelArtSlot pixelArtSlot = Instantiate(pixelArtSlotPrefab, pixelArtSlotContainer);
                pixelArtSlot.pixelData = pixelArtData;
                pixelArtSlot.SetSlot();
            }
        }
    }
}
