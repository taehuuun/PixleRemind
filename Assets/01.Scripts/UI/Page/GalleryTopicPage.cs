using System.Collections.Generic;
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
        
        private List<string> _topics = new List<string>();

        private void Start()
        {
            SetPage();
        }

        private void SetPage()
        {
            _topics = DataManager.GetDirectorys();
            CreateTopicSlot();
        }
        private void CreateTopicSlot()
        {
            foreach (var topic in _topics)
            {
                TopicSlot newTopicSlot = Instantiate(topicSlotPrefab, topicGenTrans);
                newTopicSlot.titleText.text = topic;
                newTopicSlot.GetComponent<Button>().onClick.AddListener(() => ui.SelectPage(ui.pixelArtPage));
            }
        }
    }
}
