using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayUI : BodyUI, IObserver
{
    private abstract class UIUpdate
    {
        protected readonly PlayUI playUI;

        protected UIUpdate(PlayUI playUI)
        {
            this.playUI = playUI;
        }

        public abstract void UpdateUI();
    }
    private class RemainPixelCountUIUpdate : UIUpdate
    {
        private readonly int _count;

        public RemainPixelCountUIUpdate(PlayUI playUI, int count) : base(playUI)
        {
            _count = count;
        }

        public override void UpdateUI()
        {
            playUI.remainPixelText.text = $"남은 픽셀 : {_count}";
        }
    }

    public Image board;
    public TMP_Text remainPixelText;
    public TMP_Text playTimeText;

    public MoveUI boardMove;
    public MoveUI playBtnMove;
    public MoveUI matchUIMove;

    private ColorMatchSystem _system;
    private PixelArtData _data;
    private readonly WaitForSeconds _timerDelay = new WaitForSeconds(1f);

    private void Start()
    {
        _system = gameObject.GetComponent<ColorMatchSystem>();
        _system.RegisterObserver(this);
        InitializePage();
    }

    public void SelectSlot(ColorSlot slot)
    {
        if (_system.CheckMatch(slot))
        {
            FillRandomPixel();
            UpdateSubjectState();
        }
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

            Texture2D pixelArt = PixelArtHelper.SpriteToTexture2D(board.sprite);

            Color origin = new Color(selectedPixel.OriginalColor.R, selectedPixel.OriginalColor.G,
                selectedPixel.OriginalColor.B, selectedPixel.OriginalColor.A);

            pixelArt.SetPixel(selectedPixel.PixelCoords[selectedCoord].X, selectedPixel.PixelCoords[selectedCoord].Y,
                origin);
            pixelArt.Apply();

            selectedPixel.PixelCoords.RemoveAt(selectedCoord);

            if (_data.PixelColorData.CustomPixels[selectPixelIdx].PixelCoords.Count == 0)
            {
                Debug.Log($"{origin} 컬러 값과 해당하는 좌표들을 모두 채웠음 해당 컬러를 리스트에서 제거");
                _data.PixelColorData.CustomPixels.RemoveAt(selectPixelIdx);
            }

            _data.PixelColorData.RemainingPixels--;
            _data.ThumbnailData = PixelArtHelper.ExtractThumbnailData(pixelArt);

            board.sprite = PixelArtHelper.MakeThumbnail(_data.ThumbnailData, _data.Size);
        }

        if (_data.PixelColorData.RemainingPixels == 0)
        {
            _data.IsCompleted = true;
            _system.IsGameOver = true;
            playBtnMove.gameObject.SetActive(false);
            GalleryManager.ins.SelTopicData.CompleteCount++;

            CollectPixelArtData newCollectPixelArt = new CollectPixelArtData(_data.Title, _data.Description, _data.ThumbnailData, _data.PlayTime);
            GalleryManager.ins.UpdateCollectPixelArtData(newCollectPixelArt);
        }

        _system.pixelColorData = _data.PixelColorData;

        UpdateSubjectState();
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
    
    public void UpdateSubjectState()
    {
        UpdateUI(new RemainPixelCountUIUpdate(this, _data.PixelColorData.RemainingPixels));

        GalleryManager.ins.UpdateAndSavePixelArtData(_data);
    }
    
    private void InitializePage()
    {
        GalleryManager.ins.CurPage = GalleryPage.ColorMatch;
        SetPage();
        _system.ReStart();
    }

    private void SetPage()
    {
        _system.RegisterObserver(this);
        _data = GalleryManager.ins.SelPixelArtData;
        _system.pixelColorData = _data.PixelColorData;
        playBtnMove.gameObject.SetActive(!_data.IsCompleted);
        board.sprite = PixelArtHelper.MakeThumbnail(_data.ThumbnailData, _data.Size);
        UpdateSubjectState();
    }

    private void UpdateUI(UIUpdate uiUpdate)
    {
        uiUpdate.UpdateUI();
        GalleryManager.ins.UpdateAndSavePixelArtData(_data);
    }

    private IEnumerator PlayTimer()
    {
        while (!_system.IsGameOver)
        {
            yield return _timerDelay;
            _data.PlayTime++;
            playTimeText.text = UIHelper.FormatSecondsToTimeString(_data.PlayTime);
        }
    }

    private IEnumerator CheckPlaying()
    {
        GalleryManager.ins.IsMatching = true;
        yield return new WaitUntil(() => _system.IsGameOver);
        StartCoroutine(HideMatchUI());
        StartCoroutine(UIHelper.FadeEffect(playTimeText, 0.5f, FadeType.Out));
        StartCoroutine(UIHelper.FadeEffect(remainPixelText, 0.5f, FadeType.Out));
    }

    private IEnumerator HideMatchUI()
    {
        matchUIMove.Return();
        yield return new WaitUntil(() => matchUIMove.isComplete);
        GalleryManager.ins.IsMatching = false;
        boardMove.Return();
        playBtnMove.Return();
    }

    private IEnumerator ShowMatchUI()
    {
        boardMove.StartMove();
        playBtnMove.StartMove();

        yield return new WaitUntil(() => (boardMove.isComplete && playBtnMove.isComplete));
        matchUIMove.StartMove();
    }
}