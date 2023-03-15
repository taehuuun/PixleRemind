using System.Collections.Generic;
using System.IO;
using LTH.ColorMatch.Data;
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

        // private List<TopicSlot> topicSlots = new List<TopicSlot>();

        private void Start()
        {
            SetPage();
        }

        // private void OnDisable()
        // {
        //     for (int i = 0; i < topicSlots.Count; i++)
        //     {
        //         Destroy(topicSlots[i]);
        //     }
        //     
        //     topicSlots.Clear();
        // }

        private void SetPage()
        {
            foreach (var data in GalleryManager.ins.PixelArtDatas)
            {
                GalleryManager.ins.PixelArtDatas.Remove(data);
            }
            GalleryManager.ins.PixelArtDatas.Clear();
            GalleryManager.ins.CurPage = GalleryPage.Topic;
            CreateTopicSlot();
        }
        private void CreateTopicSlot()
        {
            List<string> topics = GalleryManager.ins.GetTopics();
            
            foreach (var topic in topics)
            {
                string path = Path.Combine(DataManager.GalleryDataPath, "Topics");
                TopicSlot newTopicSlot = Instantiate(topicSlotPrefab, topicGenTrans);
                newTopicSlot.titleText.text = topic;
                newTopicSlot.data = DataManager.LoadJsonData<TopicData>(path, topic);
                Debug.Log($"TEST : {newTopicSlot.data.pixelArtDatas.Count}");
                newTopicSlot.GetComponent<Button>().onClick.AddListener(() =>
                {
                    ui.SelectPage(GalleryPage.PixelArt);
                });
                newTopicSlot.SetSlot();
            }
        }
    }
}
