using UnityEngine;

public class GalleryUI : BodyUI
{
    public Transform pixelArtSlotContainer;
    public PixelArtSlot pixelArtSlotPrefab;

    private void Start()
    {
        var pixelArtDatas = GalleryManager.ins.SelTopicData.PixelArtDatas;
        
        foreach (var pixelArtData in pixelArtDatas)
        {
            PixelArtSlot pixelArtSlot = Instantiate(pixelArtSlotPrefab, pixelArtSlotContainer);
            pixelArtSlot.pixelData = pixelArtData;
            pixelArtSlot.SetSlot();
            pixelArtSlot.OnClick += HandlePixelSlotClick;
        }
    }

    /// <summary>
    /// 픽셀 아트 슬롯을 클릭 했을 때 호출되는 메서드
    /// </summary>
    /// <param name="clickPixelArtData">클릭한 픽셀 아트</param>
    private void HandlePixelSlotClick(PixelArtData clickPixelArtData)
    {
        GalleryManager.ins.SelPixelArtData = clickPixelArtData;
        LoadingTaskManager.Instance.NextSceneName = SceneNames.PlayScene;
        MoveScene(SceneNames.LoadingScene);
    }
}