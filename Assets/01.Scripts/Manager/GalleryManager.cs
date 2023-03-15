using System.Collections.Generic;
using System.IO;
using LTH.ColorMatch.Data;
using LTH.ColorMatch.Enums;
using Newtonsoft.Json;
using UnityEngine;

namespace LTH.ColorMatch.Managers
{
    public class GalleryManager : MonoBehaviour
    {
        public static GalleryManager ins;
        public bool IsMatching { get; set; }
        public TopicData CurrentTopicArt { get; set; }
        public int SelPixelArtIdx {get; set; }
        public List<PixelArtData> PixelArtDatas{get; set; }
        public GalleryPage CurPage{get; set; }
        
        private void Awake()
        {
            if (ins == null)
            {
                ins = this;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
        
        public List<string> GetTopics() => DataManager.GetTopics();

        public void SavePixelArtData(TopicData data)
        {
            string jsonData = JsonConvert.SerializeObject(data);
            string path = Path.Combine(DataManager.GalleryDataPath, "Topics");
            DataManager.SaveJsonData(path, data.topic.ToString(), jsonData);
        }
    }
}