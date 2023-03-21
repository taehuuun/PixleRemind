using System.Collections.Generic;
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
            string cummonPath = DataManager.GalleryDataPath;
            if (mode == TestMode.Load)
            {
                if (DataManager.LocalDirectoryExists(cummonPath))
                {
                    List<string> topics = DataManager.GetTargetFolderFileNames(cummonPath);

                    for (int i = 0; i < topics.Count; i++)
                    {
                        TopicData loadTopicData = DataManager.LoadJsonData<TopicData>(cummonPath, topics[i]);
                        testData.Add(loadTopicData);
                    }
                }
                else
                {
                    if (await FirebaseManager.ins.CheckCollectionExists("GalleryData"))
                    {
                        List<TopicData> topicDataList = await FirebaseManager.ins.GetAllTopicData();
                        testData.AddRange(topicDataList);

                        foreach (var topicData in topicDataList)
                        {
                            DataManager.SaveJsonData(cummonPath, topicData.Topic.ToString(),JsonConvert.SerializeObject(topicData));
                        }
                    }
                }
            }
            else
            {
                string topicThumbData = "";
                int topicThumbSize = 0;

                List<PixelArtData> pixelArtDatas = new List<PixelArtData>();
                
                for (int i = 0; i < pixelArts.Count; i++)
                {
                    PixelArtData newPixelArtData =
                        PixelArtUtill.ExportPixelData(topic, pixelArts[i].name, pixelArts[i], difficulty);
                    pixelArtDatas.Add(newPixelArtData);

                    if (i == 0)
                    {
                        topicThumbData = newPixelArtData.ThumbData;
                        topicThumbSize = newPixelArtData.Size;
                    }
                }

                TopicData newTopicData = new TopicData(topic, topicThumbData, 0, pixelArtDatas.Count,
                    topicThumbSize, false, pixelArtDatas);

                await FirebaseManager.ins.AddTopicData("GalleryData", topic.ToString(), newTopicData);
            }
        }
    }
}
