using TMPro;
using UnityEngine;

public class CollectTopicSlot : Slot
{
    public TMP_Text completeCountTest;
    public CollectTopicData collectTopicData;
    public GameObject completeMark;
    public override void SetSlot()
    {
        titleText.text = collectTopicData.Title;
        thumbnailImage.sprite = PixelArtHelper.MakeThumbnail(collectTopicData.ThumbnailData, collectTopicData.ThumbnailSize);
    }
}
