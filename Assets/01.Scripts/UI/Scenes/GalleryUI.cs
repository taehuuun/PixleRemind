using UnityEngine;

public class GalleryUI : BodyUI
{
    public Transform pixelArtSlotContainer;
    public PixelArtSlot pixelArtSlotPrefab;

    private void Start()
    {
        var selectedTopic = GalleryManager.ins.TopicDatas[GalleryManager.ins.SelTopicIdx];

        foreach (var pixelArtData in selectedTopic.PixelArtDatas)
        {
            PixelArtSlot pixelArtSlot = Instantiate(pixelArtSlotPrefab, pixelArtSlotContainer);
            pixelArtSlot.pixelData = pixelArtData;
            pixelArtSlot.SetSlot();
        }
    }
}