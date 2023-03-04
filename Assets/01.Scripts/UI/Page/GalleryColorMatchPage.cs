using System.Collections.Generic;
using System.IO;
using LTH.ColorMatch.Data;
using LTH.ColorMatch.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace LTH.ColorMatch.UI
{
    public class GalleryColorMatchPage : Page
    {
        public Image board;
        public Cell cellPrefab;
        public Transform cellGenParent;
        
        private Cell[,] _boardData;
        private PixelArtData _data;
        
        private void Start()
        {
            SetPage();
        }

        private void SetPage()
        {
            _data = LoadPixelArtData();
            int boardSize = (int)board.rectTransform.rect.width;
            int cellSize = _data.size;

            GenerateBoard(boardSize, cellSize, _data);
        }

        private void GenerateBoard(int boardSize, int cellSize, PixelArtData generateData)
        {
            _boardData = new Cell[boardSize, boardSize];

            List<List<CustomColor>> pixelData = generateData.colorData.Pixels;
            int width = boardSize / cellSize;
            int height = boardSize / cellSize;

            for (int y = 0; y < cellSize; y++)
            {
                for (int x = 0; x < cellSize; x++)
                {
                    Color color = pixelData[y][x].complete ? pixelData[y][x].originColor : pixelData[y][x].grayColor;
                    _boardData[y, x] = Instantiate(cellPrefab);
                    _boardData[y, x].SetCell(cellGenParent, width, height, x, y, color);
                }
            }
        }
        private PixelArtData LoadPixelArtData()
        {
            return DataManager.LoadJsonData<PixelArtData>(Path.Combine(GalleryManager.topic, GalleryManager.pixelArt));
        }
    }
}
