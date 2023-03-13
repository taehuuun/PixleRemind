using System.Collections.Generic;
using System.IO;
using LTH.ColorMatch.Data;
using LTH.ColorMatch.Enums;
using LTH.ColorMatch.Managers;
using LTH.ColorMatch.Utill;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;

namespace LTH.ColorMatch.Test
{
    public enum TestMode  {Upload,Load};
    public class ExtractPixelTest : MonoBehaviour
    {
        public TestMode mode;
        public Image board;
        public List<Texture2D> pixelArts;
        public GalleryTopic topic;
        public Difficulty difficulty;
        public List<TopicData> testData;
        
        private async void Start()
        {
            if (mode == TestMode.Load)
            {
                if (DataManager.LocalDataExists())
                {
                    List<string> topics = DataManager.GetTopics();

                    for (int i = 0; i < topics.Count; i++)
                    {
                        string path = Path.Combine(DataManager.GalleryDataPath, "Topics");
                        TopicData loadTopicData = DataManager.LoadJsonData<TopicData>(path, topics[i]);
                        testData.Add(loadTopicData);
                    }
                }
                else
                {
                    if (await FirebaseManager.ins.CheckCollectionExists("GalleryData"))
                    {
                        List<TopicData> topicDataList = await FirebaseManager.ins.GetAllTopicData();
                        testData.AddRange(topicDataList);

                        for (int i = 0; i < topicDataList.Count; i++)
                        {
                            Debug.Log($"{i} : Topic = {topicDataList[i].topic}");
                            Debug.Log($"{i} : thumData = {topicDataList[i].thumbData}");
                            Debug.Log($"{i} : completeCount = {topicDataList[i].completeCount}");
                            Debug.Log($"{i} : totalCount = {topicDataList[i].totalCount}");
                            Debug.Log($"{i} : complete = {topicDataList[i].complete}");
                            Debug.Log($"{i} : pixelArts Count = {topicDataList[i].pixelArtDatas.Count}");
                            
                            string path = Path.Combine(DataManager.GalleryDataPath, "Topics");
                            DataManager.SaveJsonData(path, topicDataList[i].topic.ToString(),JsonConvert.SerializeObject(topicDataList[i]));
                        }
                    }
                }
            }
            else
            {
                List<string> pixelArtsDataJson = new List<string>();
                string topicThumbData = "";
                int topicThumbSize = 0;
                for (int i = 0; i < pixelArts.Count; i++)
                {
                    PixelArtData extractData =
                        PixelArtUtill.ExportPixelData(topic, pixelArts[i].name, pixelArts[i], difficulty);

                    if (i == 0)
                    {
                        topicThumbData = extractData.thumbData;
                        topicThumbSize = extractData.size;
                    }
                    pixelArtsDataJson.Add(JsonConvert.SerializeObject(extractData));
                }

                TopicData newTopicData = new TopicData(topic, topicThumbData, 0, pixelArtsDataJson.Count,
                    topicThumbSize, false, pixelArtsDataJson);

                await FirebaseManager.ins.AddTopicData("GalleryData", topic.ToString(), newTopicData);
            }
        }
    }
}
