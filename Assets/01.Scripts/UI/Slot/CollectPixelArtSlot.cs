public class CollectPixelArtSlot : Slot
{
    public PixelArtData pixelArtData;
    public override void SetSlot()
    {
        thumbnailImage.sprite = PixelArtHelper.MakeThumbnail(pixelArtData.ThumbnailData, pixelArtData.Size);
    }
}
