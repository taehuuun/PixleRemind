using System.Collections.Generic;
using System.Drawing;
using System.Security.Cryptography;
using LTH.ColorMatch.Data;
using LTH.ColorMatch.Enums;
using LTH.ColorMatch.Managers;
using LTH.ColorMatch.Utill;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;
using Color = UnityEngine.Color;

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
                Debug.Log("Load");
                testData = DataManager.LoadJsonData<PixelArtData>(testImage.name);
            }
            else
            {
                Debug.Log("New Gen And Save");
                testData = PixelDataExtractor.ExportPixelData(GalleryTopic.Animal, testImage.name, testImage,Difficulty.Easy);
                DataManager.SaveJsonData(JsonConvert.SerializeObject(testData), testImage.name);
            }
            
            Debug.Log(testData.size);
            
            if (isBlackWhite)
            {
                GenerateBoard(600, testData.size, testData.blackWhiteData.Pixels);
            }
            else
            {
                GenerateBoard(600, testData.size, testData.colorData.Pixels);
            }
        }
        
        private void GenerateBoard(int boardSize, int cellSize, List<List<CustomColor>> pixelData)
        {
            board = new Cell[cellSize,cellSize];
            int width = boardSize / cellSize;
            int height = boardSize / cellSize;

            for (int y = 0; y < cellSize; y++)
            {
                for (int x = 0; x < cellSize; x++)
                {
                    board[y, x] = Instantiate(cellPrefabs);
                    board[y, x].SetCell(cellParent, width, height, x, y, pixelData[y][x].ConvertColor());
                }
            }
        }
    }
}
