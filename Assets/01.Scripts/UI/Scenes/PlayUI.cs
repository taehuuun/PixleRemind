using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayUI : BaseSceneUI
{
    public Image board;
    public TMP_Text remainPixelText;
    public TMP_Text playTimeText;
    public Transform playBtn;
    public Transform matchUI;
    
    protected override void Initialize()
    {
        remainPixelText.text =remainPixelText.text = $"남은 픽셀 : ?개";
    }
    
    public void UpdatePlayTime(int playTime)
    {
        playTimeText.text = StringHelper.FormatSecondsToTimeString(playTime);
    }
    public void UpdatePixelArt(string thumbnailData, int size)
    {
        board.sprite = PixelArtHelper.MakeThumbnail(thumbnailData, size);
    }
    public void UpdateRemainPixelCount(int remainPixelCount)
    {
        remainPixelText.text = $"남은 픽셀 : {remainPixelCount}";
    }
    public void SetPlayButton(bool isComplete)
    {
        playBtn.gameObject.SetActive(!isComplete);
    }
    public void ShowUI()
    {
        StartCoroutine(UIAnimation.FadeEffect(playTimeText, 0.5f, FadeType.In));
        StartCoroutine(UIAnimation.FadeEffect(remainPixelText, 0.5f, FadeType.In));
        StartCoroutine(ShowMatchUI());
    }
    public void HideUI()
    {
        StartCoroutine(HideMatchUI());
        StartCoroutine(UIAnimation.FadeEffect(playTimeText, 0.5f, FadeType.Out));
        StartCoroutine(UIAnimation.FadeEffect(remainPixelText, 0.5f, FadeType.Out));
    }

    private IEnumerator HideMatchUI()
    {
        yield return UIAnimation.MoveUI(matchUI, new Vector3(0, -710, 0), 0.5f);
        StartCoroutine(UIAnimation.MoveUI(board.transform, new Vector3(0, -300, 0), 0.5f));
        StartCoroutine(UIAnimation.MoveUI(playBtn, new Vector3(0, 400, 0), 0.5f));
    }
    private IEnumerator ShowMatchUI()
    {
        // 두 개의 코루틴을 시작합니다.
        Coroutine coroutine1 = StartCoroutine(UIAnimation.MoveUI(board.transform, new Vector3(0, 300, 0), 0.5f));
        Coroutine coroutine2 = StartCoroutine(UIAnimation.MoveUI(playBtn, new Vector3(0, -400, 0), 0.5f));

        // 두 코루틴이 완료될 때까지 기다립니다.
        yield return coroutine1;
        yield return coroutine2;
        
        StartCoroutine(UIAnimation.MoveUI(matchUI, new Vector3(0, 710, 0), 0.5f));
    }
}