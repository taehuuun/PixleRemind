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
        public bool isMatching;
        public TopicData currentTopicArt;
        public int selPixelArtIdx;
        public List<PixelArtData> pixelArtDatas;
        public GalleryPage curPage;
        
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