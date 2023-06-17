using UnityEngine;

public class GalleryUI : BodyUI
{
    public Transform pixelArtSlotContainer;
    public PixelArtSlot pixelArtSlotPrefab;

    private void Start()
    {
        // var selectedTopic = GalleryManager.ins.TopicDatas[GalleryManager.ins.SelTopicIdx];
        var pixelArtDatas = GalleryManager.ins.PixelArtDatas;
        
        foreach (var pixelArtData in pixelArtDatas)
        {
            PixelArtSlot pixelArtSlot = Instantiate(pixelArtSlotPrefab, pixelArtSlotContainer);
            pixelArtSlot.pixelData = pixelArtData;
            pixelArtSlot.SetSlot();
        }
    }
}