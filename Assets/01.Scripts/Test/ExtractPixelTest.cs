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
            if (DataManager.JsonFileExist(testImage.name))
            {
                testData = DataManager.LoadJsonData<PixelArtData>(testImage.name);
            }
            else
            {
                testData = PixelDataExtractor.ExportPixelData(topic, testImage.name, testImage, difficulty);
                DataManager.SaveJsonData(JsonConvert.SerializeObject(testData), testImage.name);
            }
        }
    }
}
