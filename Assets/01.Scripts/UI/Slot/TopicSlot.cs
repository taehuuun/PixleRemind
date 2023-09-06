using TMPro;
using UnityEngine;

public class TopicSlot : Slot
{
    public TMP_Text completeCountText;
    public GameObject completeMark;
    public TopicData data;

    public override void OnSlotClick()
    {
        DataManager.LocalData.SetTopicDataID(data.ID);
        DataManager.SaveLocalData();
        LoadingManager.Instance.StartNextSceneLoading(SceneNames.GalleryScene);
    }
    public override void SetSlot()
    {
        titleText.text = data.Title;
        completeMark.SetActive(data.Complete);
        completeCountText.text = $"{data.CompleteCount} / {data.GetPixelArtsCount()}";
        thumbnailImage.sprite = PixelArtHelper.MakeThumbnail(data.ThumbnailData, data.ThumbnailSize);
    }
}