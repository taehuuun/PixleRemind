using System.IO;
using LTH.ColorMatch.Data;
using LTH.ColorMatch.Enums;
using LTH.ColorMatch.Managers;
using LTH.ColorMatch.Utill;
using Newtonsoft.Json;
using UnityEngine;

namespace LTH.ColorMatch.Test
{
    public class ExtractPixelTest : MonoBehaviour
    {
        public Texture2D testImage;
        public GalleryTopic topic;
        public Difficulty difficulty;
        public PixelArtData testData;

        private void Start()
        {
            testData = PixelDataExtractor.ExportPixelData(topic, testImage.name, testImage, difficulty);
            DataManager.SaveJsonData( topic.ToString(),testImage.name,JsonConvert.SerializeObject(testData));
        }
    }
}
