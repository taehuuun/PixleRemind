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
    public enum TestMode  {Save,Load};
    public class ExtractPixelTest : MonoBehaviour
    {
        public TestMode mode;
        public Image board;
        public Texture2D testImage;
        public GalleryTopic topic;
        public Difficulty difficulty;
        public PixelArtData testData;


        private void Start()
        {
            if (mode == TestMode.Save)
            {
                testData = PixelArtUtill.ExportPixelData(topic, testImage.name, testImage, difficulty);
                string path = Path.Combine(DataManager.GalleryDataPath, topic.ToString());
                DataManager.SaveJsonData(path, testImage.name,JsonConvert.SerializeObject(testData));
            }
            else
            {
                string path = Path.Combine(DataManager.GalleryDataPath, topic.ToString());
                testData = DataManager.LoadJsonData<PixelArtData>(path,testImage.name);
                board.sprite = PixelArtUtill.MakeThumbnail(testData.thumbData,testData.size);
            }

        }
    }
}
