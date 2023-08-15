using UnityEngine;

public class GalleryUI : BodyUI
{
    public Transform pixelArtSlotContainer;
    public PixelArtSlot pixelArtSlotPrefab;

    private void Start()
    {
        SetPixelArtSlot();
    }

    private void SetPixelArtSlot()
    {
        TopicData topicData = DataManager.LocalData.LocalTopicData[DataManager.LocalData.SelectTopicDataID];
        
        foreach (var pixelArtData in topicData.PixelArtDataList)
        {
            PixelArtSlot pixelArtSlot = Instantiate(pixelArtSlotPrefab, pixelArtSlotContainer);
            pixelArtSlot.pixelData = pixelArtData;
            pixelArtSlot.SetSlot();
        }
    }
}