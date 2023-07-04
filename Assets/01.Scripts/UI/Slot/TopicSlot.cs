using TMPro;
using UnityEngine;

public class TopicSlot : Slot
{
    public TMP_Text completeCountText;
    public GameObject completeMark;
    public TopicData data;

    public delegate void TopicSlotClickHandler(TopicData clickedTopicData);

    public event TopicSlotClickHandler OnClick;

    public override void OnSlotClick()
    {
        // GalleryManager.ins.LoadPixelDataForTopic(data);
        OnClick?.Invoke(data);
    }

    public override void SetSlot()
    {
        titleText.text = data.Title;
        completeMark.SetActive(data.Complete);
        completeCountText.text = $"{data.CompleteCount} / {data.TotalCount}";
        thumbnailImb.sprite = PixelArtHelper.MakeThumbnail(data.ThumbData, data.ThumbSize);
    }
}