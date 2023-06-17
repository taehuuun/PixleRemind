using TMPro;
using UnityEngine;

public class PixelArtSlot : GallerySlot
{
    public TMP_Text difficultyText;
    public TMP_Text playTimeText;
    public PixelArtData pixelData;
    public GameObject completeMark;

    public override void SetSlot()
    {
        difficultyText.text = pixelData.Difficulty.ToString();
        titleText.text = pixelData.TitleID;
        thumbnailImb.sprite = PixelArtHelper.MakeThumbnail(pixelData.ThumbnailData, pixelData.Size);

        if (pixelData.IsCompleted)
        {
            completeMark.SetActive(true);
            playTimeText.gameObject.SetActive(true);
            playTimeText.text = UIHelper.FormatSecondsToTimeString(pixelData.PlayTime);
        }

        completeMark.SetActive(pixelData.IsCompleted);
        playTimeText.gameObject.SetActive(pixelData.IsCompleted);
    }
}