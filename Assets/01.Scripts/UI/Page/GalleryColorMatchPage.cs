using System.Collections;
using System.Linq;
using LTH.ColorMatch.Data;
using LTH.ColorMatch.Enums;
using LTH.ColorMatch.Interfaces;
using LTH.ColorMatch.Managers;
using LTH.ColorMatch.Utill;
using TMPro;
using Unity.VisualScripting;
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
            // UpdateUI(new GameOverUiUpdate(this,system.IsGameOver));
            UpdateUI(new SimilarityUIUpdate(this,system.SimilarRange));
            UpdateUI(new FillCountUIUpdate(this, _data.RemainingFills));

            GalleryManager.ins.UpdateAndSavePixelArtData(_data);
        }        
        public void FillRandomPixel()
        {
            if (_data.RemainingFills == 0)
            {
                Debug.LogError("해당 PixelArt의 FillCount가 모두 소진됨");
                return;
            }

            if (_data.PixelColorData.RemainPixel > 0)
            {
                var availablePixels = _data.PixelColorData.Pixels.Where(p => !p.IsFeel).ToList();
                
                int selectPixelIdx = Random.Range(0, availablePixels.Count);

                var selectedPixel = availablePixels[selectPixelIdx];
                
                _data.RemainingFills--;
                _data.PixelColorData.RemainPixel--;
                selectedPixel.IsFeel = true;
                _data.ThumbnailData = PixelArtUtill.ExtractThumbnailData(_data.PixelColorData, _data.Size);
                
                board.sprite = PixelArtUtill.MakeThumbnail(_data.ThumbnailData, _data.Size);
            }

            if (_data.PixelColorData.RemainPixel == 0)
            {
                Debug.Log("해당 PixelArt을 모두 채움");
                _data.IsCompleted = true;
                system.IsGameOver = true;
                // playBtnMove.gameObject.SetActive(false);
                GalleryManager.ins.TopicDatas[GalleryManager.ins.SelTopicIdx].CompleteCount++;
            }

            UpdateSubjectState();
        }
        public void SelectSlot(ColorSlot slot)
        {
            if (system.CheckMatch(slot))
            {
                _data.RemainingFills++;
                UpdateSubjectState();
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
            playBtnMove.gameObject.SetActive(!_data.IsCompleted);
            board.sprite = PixelArtUtill.MakeThumbnail(_data.ThumbnailData, _data.Size);
            UpdateSubjectState();
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
