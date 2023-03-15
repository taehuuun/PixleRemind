using System.Collections;
using System.Linq;
using LTH.ColorMatch.Data;
using LTH.ColorMatch.Enums;
using LTH.ColorMatch.Interfaces;
using LTH.ColorMatch.Managers;
using LTH.ColorMatch.Utill;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LTH.ColorMatch.UI
{
    public class GalleryColorMatchPage : Page,IObserver
    {
        private abstract class UIUpdate
        {
            protected readonly GalleryColorMatchPage Page;
            protected UIUpdate(GalleryColorMatchPage page)
            {
                Page = page;
            }

            public abstract void UpdateUI();
        }
        private class SimilarityUIUpdate : UIUpdate
        {
            private readonly float _similarity;

            public SimilarityUIUpdate(GalleryColorMatchPage page, float similarity) : base(page)
            {
                _similarity = similarity;
            }
            public override void UpdateUI()
            {
                float result = 100 - _similarity;
                Page.similarText.text = $"유사도 : {result:N2}";
            }
        }
        private class FillCountUIUpdate : UIUpdate
        {
            private readonly int _count;

            public FillCountUIUpdate(GalleryColorMatchPage page, int count) : base(page)
            {
                _count = count;
            }

            public override void UpdateUI()
            {
                Page.fillCountText.text = $"O : {_count}";
            }
        }
        private class  GameOverUiUpdate : UIUpdate
        {
            private readonly bool _gameOver;

            public GameOverUiUpdate(GalleryColorMatchPage page, bool gameOver) : base(page)
            {
                _gameOver = gameOver;
            }

            public override void UpdateUI()
            {
                if (_gameOver)
                {
                    GalleryManager.ins.SavePixelArtData(GalleryManager.ins.CurrentTopicArt);
                }
            }
        }
        
        public Image board;
        public TMP_Text fillCountText;
        public TMP_Text similarText;
        
        public ColorMatchSystem system;
        
        private PixelArtData _data;

        public MoveUI boardMove;
        public MoveUI playBtnMove;
        public MoveUI matchUIMove;
        
        private void OnEnable()
        {
            system.RegisterObserver(this);
            InitializePage();
        }
        private void InitializePage()
        {
            GalleryManager.ins.CurPage = GalleryPage.ColorMatch;
            SetPage();
            system.ReStart();
        }
        private void UpdateUI(UIUpdate uiUpdate)
        {
            uiUpdate.UpdateUI();
        }
        
        public void UpdateSubjectState()
        {
            UpdateUI(new GameOverUiUpdate(this,system.IsGameOver));
            UpdateUI(new SimilarityUIUpdate(this,system.SimilarRange));
            UpdateUI(new FillCountUIUpdate(this, _data.fillCount));
        }        
        public void FillRandomPixel()
        {
            if (_data.fillCount == 0)
            {
                Debug.LogError("해당 PixelArt의 FillCount가 모두 소진됨");
                return;
            }

            if (_data.colorData.remainPixel == 0)
            {
                Debug.LogError("해당 PixelArt을 모두 채움");
                _data.complete = true;
                return;
            }
            
            var availablePixels = _data.colorData.Pixels.Where(p => !p.isFeel).ToList();

            if (availablePixels.Count == 0)
            {
                Debug.LogError("더 이상 채울 픽셀이 없음");
                return;
            }
            
            int selectPixel = Random.Range(0, availablePixels.Count);

            // Use var keyword to reduce repetition
            var selectedPixel = availablePixels[selectPixel];
            
            _data.fillCount--;
            _data.colorData.remainPixel--;
            selectedPixel.isFeel = true;
            _data.thumbData = PixelArtUtill.ExtractThumbnailData(_data.colorData, _data.size);
            
            UpdateCountText(_data.fillCount);
            board.sprite = PixelArtUtill.MakeThumbnail(_data.thumbData, _data.size);

            int selIdx = GalleryManager.ins.SelPixelArtIdx;

            GalleryManager.ins.CurrentTopicArt.PixelArtDatas[selIdx] = JsonConvert.SerializeObject(_data);
            GalleryManager.ins.SavePixelArtData(GalleryManager.ins.CurrentTopicArt);
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
            StartCoroutine(CheckPlaying());
            StartCoroutine(ShowMatchUI());
        }
        
        private void SetPage()
        {
            system.RegisterObserver(this);
            _data = GalleryManager.ins.PixelArtDatas[GalleryManager.ins.SelPixelArtIdx];
            board.sprite = PixelArtUtill.MakeThumbnail(_data.thumbData, _data.size);
            UpdateCountText(_data.fillCount);
        }
        private void UpdateCountText(int count)
        {
            fillCountText.text = $"O : {count}";
        }
        private IEnumerator CheckPlaying()
        {
            GalleryManager.ins.IsMatching = true;
            yield return new WaitUntil(() => system.IsGameOver);
            StartCoroutine(HideMatchUI());
        }
        private IEnumerator ShowMatchUI()
        {
            boardMove.StartMove();
            playBtnMove.StartMove();

            yield return new WaitUntil(() => (boardMove.isComplete && playBtnMove.isComplete));
            matchUIMove.StartMove();
        }
        private IEnumerator HideMatchUI()
        {
            matchUIMove.Return();
            yield return new WaitUntil(() => matchUIMove.isComplete);
            GalleryManager.ins.IsMatching = false;
            boardMove.Return();
            playBtnMove.Return();
        }
    }
}
