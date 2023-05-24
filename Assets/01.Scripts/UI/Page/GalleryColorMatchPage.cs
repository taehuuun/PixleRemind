using System.Collections;
using LTH.PixelRemind.Data;
using LTH.PixelRemind.Enums;
using LTH.PixelRemind.Interfaces;
using LTH.PixelRemind.Managers;
using LTH.PixelRemind.Managers.Gallery;
using LTH.PixelRemind.Utill;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace LTH.PixelRemind.UI
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
    
        private class RemainPixelCountUIUpdate : UIUpdate
        {
            private readonly int _count;

            public RemainPixelCountUIUpdate(GalleryColorMatchPage page, int count) : base(page)
            {
                _count = count;
            }

            public override void UpdateUI()
            {
                Page.remainPixelText.text = $"남은 픽셀 : {_count}";
            }
        }
        
        public Image board;
        public TMP_Text remainPixelText;
        public TMP_Text playTimeText;
        
        public ColorMatchSystem system;
        
        private PixelArtData _data;

        public MoveUI boardMove;
        public MoveUI playBtnMove;
        public MoveUI matchUIMove;

        private readonly WaitForSeconds _timerDelay = new WaitForSeconds(1f);

        private void OnEnable()
        {
            system.RegisterObserver(this);
            InitializePage();
        }
        private void OnDisable()
        {
            GalleryManager.ins.UpdateAndSavePixelArtData(_data);
            remainPixelText.gameObject.SetActive(false);
            playTimeText.gameObject.SetActive(false);
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
            UpdateUI(new RemainPixelCountUIUpdate(this, _data.PixelColorData.RemainingPixels));

            GalleryManager.ins.UpdateAndSavePixelArtData(_data);
        }        
        public void FillRandomPixel()
        {
            if (_data.PixelColorData.RemainingPixels == 0)
            {
                Debug.LogError("해당 PixelArt가 모두 채워짐");
                return;
            }

            if (_data.PixelColorData.RemainingPixels > 0)
            {
                int selectPixelIdx = Random.Range(0, _data.PixelColorData.CustomPixels.Count);
                
                var selectedPixel = _data.PixelColorData.CustomPixels[selectPixelIdx];
                int selectedCoord = Random.Range(0, selectedPixel.PixelCoords.Count);

                Texture2D pixelArt = PixelArtUtill.SpriteToTexture2D(board.sprite);
                
                Color origin = new Color(selectedPixel.OriginalColor.R, selectedPixel.OriginalColor.G, selectedPixel.OriginalColor.B, selectedPixel.OriginalColor.A);
                
                pixelArt.SetPixel(selectedPixel.PixelCoords[selectedCoord].X,selectedPixel.PixelCoords[selectedCoord].Y,origin);
                pixelArt.Apply();
                
                selectedPixel.PixelCoords.RemoveAt(selectedCoord);

                if (_data.PixelColorData.CustomPixels[selectPixelIdx].PixelCoords.Count == 0)
                {
                    Debug.Log($"{origin} 컬러 값과 해당하는 좌표들을 모두 채웠음 해당 컬러를 리스트에서 제거");
                    _data.PixelColorData.CustomPixels.RemoveAt(selectPixelIdx);
                }
                
                _data.PixelColorData.RemainingPixels--;
                _data.ThumbnailData = PixelArtUtill.ExtractThumbnailData(pixelArt);
                
                board.sprite = PixelArtUtill.MakeThumbnail(_data.ThumbnailData, _data.Size);
            }

            if (_data.PixelColorData.RemainingPixels == 0)
            {
                Debug.Log("해당 PixelArt을 모두 채움");
                _data.IsCompleted = true;
                system.IsGameOver = true;
                playBtnMove.gameObject.SetActive(false);
                GalleryManager.ins.TopicDatas[GalleryManager.ins.SelTopicIdx].CompleteCount++;
            }
            
            system.pixelColorData = _data.PixelColorData;
            
            UpdateSubjectState();
        }
        public void SelectSlot(ColorSlot slot)
        {
            if (system.CheckMatch(slot))
            {
                FillRandomPixel();
                UpdateSubjectState();
            }
        }
        public void PlayBtnEvent()
        {
            StartCoroutine(UIHelper.FadeEffect(playTimeText, 0.5f, FadeType.In));
            StartCoroutine(UIHelper.FadeEffect(remainPixelText, 0.5f, FadeType.In));
            playTimeText.text = UIHelper.FormatSecondsToTimeString(_data.PlayTime);
            remainPixelText.gameObject.SetActive(true);
            playTimeText.gameObject.SetActive(true);
            
            StartCoroutine(CheckPlaying());
            StartCoroutine(ShowMatchUI());
            StartCoroutine(PlayTimer());
        }
        
        private void SetPage()
        {
            system.RegisterObserver(this);
            _data = GalleryManager.ins.PixelArtDatas[GalleryManager.ins.SelPixelArtIdx];
            system.pixelColorData = _data.PixelColorData;
            playBtnMove.gameObject.SetActive(!_data.IsCompleted);
            board.sprite = PixelArtUtill.MakeThumbnail(_data.ThumbnailData, _data.Size);
            UpdateSubjectState();
        }

        private IEnumerator PlayTimer()
        {
            while (!system.IsGameOver)
            {
                yield return _timerDelay;
                _data.PlayTime++;
                playTimeText.text = UIHelper.FormatSecondsToTimeString(_data.PlayTime);
            }
        }
        private IEnumerator CheckPlaying()
        {
            GalleryManager.ins.IsMatching = true;
            yield return new WaitUntil(() => system.IsGameOver);
            StartCoroutine(HideMatchUI());
            StartCoroutine(UIHelper.FadeEffect(playTimeText, 0.5f, FadeType.Out));
            StartCoroutine(UIHelper.FadeEffect(remainPixelText, 0.5f, FadeType.Out));
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
