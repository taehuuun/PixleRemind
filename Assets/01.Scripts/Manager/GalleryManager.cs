using System.Collections.Generic;
using LTH.ColorMatch.Data;
using LTH.ColorMatch.Enums;
using Newtonsoft.Json;
using UnityEngine;

namespace LTH.ColorMatch.Managers
{
    public class GalleryManager : MonoBehaviour
    {
        public static GalleryManager ins;
        
        public GalleryPage CurPage{get; set; }
        public int SelPixelArtIdx {get; set; }
        public int SelTopicIdx { get; set; }
        public List<PixelArtData> PixelArtDatas { get; private set; }
        public List<TopicData> TopicDatas { get; set; }

        public bool IsMatching { get; set; }
        
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

            TopicDatas = new List<TopicData>();
            PixelArtDatas = new List<PixelArtData>();
        }

        public void LoadTopicData()
        {
            List<string> topicNames = DataManager.GetTargetFolderFileNames(DataManager.GalleryDataPath);

            foreach (var topicName in topicNames)
            {
                TopicDatas.Add(GetTopicData(topicName));
            }
        }

        public void LoadPixelData(TopicData topicData)
        {
            PixelArtDatas = topicData.PixelArtDatas;
        }

        public void UpdateAndSavePixelArtData(PixelArtData updateData)
        {
            TopicData saveTopic = TopicDatas[SelTopicIdx]; 
            saveTopic.PixelArtDatas[SelPixelArtIdx] = updateData;
            saveTopic.Complete = saveTopic.CompleteCount == saveTopic.TotalCount;
            saveTopic.ThumbData = updateData.ThumbData;
            SavePixelArtData(saveTopic);
        }

        public void SavePixelArtData(TopicData data)
        {
            string jsonData = JsonConvert.SerializeObject(data);
            DataManager.SaveJsonData(DataManager.GalleryDataPath, data.Topic.ToString(), jsonData);
        }
        
        public static TopicData GetTopicData(string topicName)
        {
            return DataManager.LoadJsonData<TopicData>(DataManager.GalleryDataPath, topicName);
        }
    }
}