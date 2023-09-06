using TMPro;
using UnityEngine;

public class PixelArtSlot : Slot
{
    public TMP_Text difficultyText;
    public TMP_Text playTimeText;
    public PixelArtData pixelData;
    public GameObject completeMark;
    
    public override void OnSlotClick()
    {
        DataManager.LocalData.SetPixelArtDataID(pixelData.ID);
        DataManager.SaveLocalData();
        LoadingManager.Instance.StartNextSceneLoading(SceneNames.PlayScene);
    }
    public override void SetSlot()
    {
        difficultyText.text = pixelData.Difficulty.ToString();
        titleText.text = pixelData.Title;
        thumbnailImage.sprite = PixelArtHelper.MakeThumbnail(pixelData.ThumbnailData, pixelData.ThumbnailSize);

        if (pixelData.IsCompleted)
        {
            completeMark.SetActive(true);
            playTimeText.gameObject.SetActive(true);
            playTimeText.text = StringHelper.FormatSecondsToTimeString(pixelData.PlayTime);
        }

        completeMark.SetActive(pixelData.IsCompleted);
        playTimeText.gameObject.SetActive(pixelData.IsCompleted);
    }
}