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
        TopicData topicData = DataManager.LoadJsonData<TopicData>(DataPath.GalleryDataPath, DataManager.userData.SelectTopicID);
        
        foreach (var pixelArtData in topicData.PixelArtDatas)
        {
            PixelArtSlot pixelArtSlot = Instantiate(pixelArtSlotPrefab, pixelArtSlotContainer);
            pixelArtSlot.pixelData = pixelArtData;
            pixelArtSlot.SetSlot();
        }
    }
}