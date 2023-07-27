using TMPro;
using UnityEngine;

public class TopicSlot : Slot
{
    public TMP_Text completeCountText;
    public GameObject completeMark;
    public TopicData data;

    public override void OnSlotClick()
    {
        DataManager.userData.SelectTopicID = data.ID;
    }
    public override void SetSlot()
    {
        titleText.text = data.Title;
        completeMark.SetActive(data.Complete);
        completeCountText.text = $"{data.CompleteCount} / {data.TotalCount}";
        thumbnailImage.sprite = PixelArtHelper.MakeThumbnail(data.ThumbData, data.ThumbSize);
    }
}