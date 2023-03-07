using System.Collections.Generic;
using LTH.ColorMatch.Enums;
using LTH.ColorMatch.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace LTH.ColorMatch.UI
{
    public class GalleryTopicPage : Page
    {
        [SerializeField] private GalleryUI ui;
        public TopicSlot topicSlotPrefab;
        public Transform topicGenTrans;
        
        private void Start()
        {
            SetPage();
        }

        private void SetPage()
        {
            CreateTopicSlot();
        }
        private void CreateTopicSlot()
        {
            List<string> topics = GalleryManager.ins.GetTopicDatas();
            
            foreach (var topic in topics)
            {
                TopicSlot newTopicSlot = Instantiate(topicSlotPrefab, topicGenTrans);
                newTopicSlot.titleText.text = topic;
                newTopicSlot.GetComponent<Button>().onClick.AddListener(() => ui.SelectPage(GalleryPage.PixelArt));
            }
        }
    }
}
