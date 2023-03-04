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

        public PixelArtData testData;
        public Transform cellParent;

        public Cell cellPrefabs;
        public Cell[,] board;
        
        public bool isBlackWhite = false;
        private void Start()
        {
            if (DataManager.JsonFileExist(testImage.name))
            {
                testData = DataManager.LoadJsonData<PixelArtData>(testImage.name);
            }
            else
            {
                testData = PixelDataExtractor.ExportPixelData(GalleryTopic.Animal, testImage.name, testImage,Difficulty.Easy);
                DataManager.SaveJsonData(JsonConvert.SerializeObject(testData), testImage.name);
            }
        }
    }
}
