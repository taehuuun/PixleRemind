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
        var pixelArtDatas = GalleryManager.ins.SelTopicData.PixelArtDatas;
        
        foreach (var pixelArtData in pixelArtDatas)
        {
            PixelArtSlot pixelArtSlot = Instantiate(pixelArtSlotPrefab, pixelArtSlotContainer);
            pixelArtSlot.pixelData = pixelArtData;
            pixelArtSlot.SetSlot();
        }
    }
}