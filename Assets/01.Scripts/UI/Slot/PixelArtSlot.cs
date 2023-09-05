using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        LoadingManager.Instance.NextSceneName = SceneNames.PlayScene;
        SceneManager.LoadScene(SceneNames.LoadingScene);
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
            playTimeText.text = UIHelper.FormatSecondsToTimeString(pixelData.PlayTime);
        }

        completeMark.SetActive(pixelData.IsCompleted);
        playTimeText.gameObject.SetActive(pixelData.IsCompleted);
    }
}