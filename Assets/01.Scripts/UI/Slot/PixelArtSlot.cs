using TMPro;
using UnityEngine;

public class PixelArtSlot : GallerySlot
{
    public TMP_Text difficultyText;
    public TMP_Text playTimeText;
    public PixelArtData pixelData;
    public GameObject completeMark;

    public delegate void PixelArtSlotClickHandler(PixelArtData clickPixelArtData);

    public event PixelArtSlotClickHandler OnClick;

    public override void OnSlotClick()
    {
        OnClick?.Invoke(pixelData);
    }

    public override void SetSlot()
    {
        difficultyText.text = pixelData.Difficulty.ToString();
        titleText.text = pixelData.Title;
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