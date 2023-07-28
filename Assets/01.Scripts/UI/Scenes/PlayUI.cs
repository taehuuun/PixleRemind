using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayUI : BodyUI
{
    public Image board;
    public TMP_Text remainPixelText;
    public TMP_Text playTimeText;

    public MoveUI boardMove;
    public MoveUI playBtnMove;
    public MoveUI matchUIMove;

    public void UpdatePlayTime(int playTime)
    {
        playTimeText.text = UIHelper.FormatSecondsToTimeString(playTime);
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
        playBtnMove.gameObject.SetActive(!isComplete);
    }
    public void ShowUI()
    {
        StartCoroutine(UIHelper.FadeEffect(playTimeText, 0.5f, FadeType.In));
        StartCoroutine(UIHelper.FadeEffect(remainPixelText, 0.5f, FadeType.In));
        StartCoroutine(ShowMatchUI());
    }
    public void HideUI()
    {
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