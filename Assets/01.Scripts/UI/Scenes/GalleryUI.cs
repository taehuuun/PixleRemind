using UnityEngine;

public class GalleryUI : BaseSceneUI
{
    public Transform pixelArtSlotContainer;
    public PixelArtSlot pixelArtSlotPrefab;
    
    protected override void Initialize()
    {
        SetPixelArtSlot();
    }

    private void SetPixelArtSlot()
    {
        TopicData topicData = DataManager.LocalData.GetTopicData(DataManager.LocalData.GetTopicID());
        
        foreach (var pixelArtData in topicData.GetPixelArtList())
        {
            PixelArtSlot pixelArtSlot = Instantiate(pixelArtSlotPrefab, pixelArtSlotContainer);
            pixelArtSlot.pixelData = pixelArtData;
            pixelArtSlot.SetSlot();
        }
    }
}