using System.Collections;
using LTH.ColorMatch.Data;
using LTH.ColorMatch.Interfaces;
using LTH.ColorMatch.Managers;
using LTH.ColorMatch.Utill;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LTH.ColorMatch.UI
{
    public class GalleryColorMatchPage : Page,IObserver
    {
        public Image board;
        public TMP_Text fillCountText;
        public TMP_Text similarText;
        
        // public Cell cellPrefab;
        // public Transform cellGenParent;
        public ColorMatchSystem system;
        
        // private Cell[,] _boardData;
        private PixelArtData _data;

        public MoveUI boardMove;
        public MoveUI playBtnMove;
        public MoveUI matchUIMove;
        
        private void OnEnable()
        {
            SetPage();
            system.ReStart();
        }

        public void UpdateSubjectState()
        {
            // UpdateCorrect();
            UpdateGameOver(system.IsGameOver);
            UpdateSimilarity(system.SimilarRange);
        }
        public void UpdateSimilarity(float similarity)
        {
            float result = 100 - similarity;
            similarText.text =$"유사도 : {string.Format("{0:N2}", result)}";
        }
        public void FillRandomPixel()
        {
            while (true)
            {
                if (_data.fillCount == 0)
                {
                    Debug.LogError("해당 PixelArt의 FillCount가 모두 소진됨");
                    return;
                }

                if (_data.colorData.remainPixel == 0)
                {
                    Debug.LogError("해당 PixelArt을 모두 채움");
                    return;
                }
                
                int x = Random.Range(0, _data.size);
                int y = Random.Range(0, _data.size);

                bool complete = _data.colorData.Pixels[y][x].complete;
                ColorMatchColor originColor = _data.colorData.Pixels[y][x].originColorMatchColor;
                
                if (!complete && originColor.a != 0)
                {
                    _data.fillCount--;
                    _data.colorData.remainPixel--;
                    _data.colorData.Pixels[y][x].complete = true;
                    
                    UpdateCountText(_data.fillCount);
                    GalleryManager.ins.SavePixelArtData(_data);
                    board.sprite = PixelArtUtill.MakeThumbnail(_data.thumbData, _data.size);
                    return;
                }
            }
        }
        public void SelectSlot(ColorSlot slot)
        {
            if (system.CheckMatch(slot))
            {
                _data.fillCount++;
                UpdateCountText(_data.fillCount);
            }
        }
        public void PlayBtnEvent()
        {
            print("GalleryMode 플레이 시작");
            StartCoroutine(CheckPlaying());
            StartCoroutine(ShowMatchUI());
        }
        
        private void SetPage()
        {
            system.RegisterObserver(this);
            _data = GalleryManager.ins.LoadPixelArtData();
            int boardSize = (int)board.rectTransform.rect.width;
            int cellSize = _data.size;

            board.sprite = PixelArtUtill.MakeThumbnail(_data.thumbData, _data.size);
            // GenerateBoard(boardSize, cellSize, _data);
            UpdateCountText(_data.fillCount);
        }
        // private void GenerateBoard(int boardSize, int cellSize, PixelArtData generateData)
        // {
        //     _boardData = new Cell[boardSize, boardSize];
        //
        //     List<List<CustomColor>> pixelData = generateData.colorData.Pixels;
        //     int width = boardSize / cellSize;
        //     int height = boardSize / cellSize;
        //
        //     for (int y = 0; y < cellSize; y++)
        //     {
        //         for (int x = 0; x < cellSize; x++)
        //         {
        //             _boardData[y, x] = Instantiate(cellPrefab);
        //             _boardData[y, x].SetCell(cellGenParent, width, height, x, y, pixelData[y][x]);
        //         }
        //     }
        // }
        private void UpdateCountText(int count)
        {
            // _data.fillCount++;
            fillCountText.text = $"O : {count}";
        }
        private void UpdateGameOver(bool gameOver)
        {
            if (gameOver)
            {
                GalleryManager.ins.SavePixelArtData(_data);
            }
        }

        private IEnumerator CheckPlaying()
        {
            print("CheckPlaying 시작");
            GalleryManager.ins.isMatching = true;
            yield return new WaitUntil(() => system.IsGameOver);
            print("CheckPlaying 종료");
            StartCoroutine(HideMatchUI());
        }
        private IEnumerator ShowMatchUI()
        {
            print("ShowMatchUI 시작");
            boardMove.StartMove();
            playBtnMove.StartMove();

            yield return new WaitUntil(() => (boardMove.isComplete && playBtnMove.isComplete));
            matchUIMove.StartMove();
        }
        private IEnumerator HideMatchUI()
        {
            print("HideMatchUI 시작");
            matchUIMove.Return();
            yield return new WaitUntil(() => matchUIMove.isComplete);
            GalleryManager.ins.isMatching = false;
            boardMove.Return();
            playBtnMove.Return();
        }
    }
}
