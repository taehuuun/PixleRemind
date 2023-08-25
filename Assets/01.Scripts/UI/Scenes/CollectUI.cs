using UnityEngine;

public class CollectUI : BodyUI
{
    [SerializeField] private Transform genCollectTopicSlotParent;
    [SerializeField] private CollectTopicSlot collectTopicSlotPrefab;
    [SerializeField] private CollectPixelArtSlot collectPixelArtSlotPrefab;
    [SerializeField] private CollectPixelArtDetailPopup collectPixelArtDetailPopup;
    
    private void Start()
    {
        SetSlot();
    }
    
    private void SetSlot()
    {
        // UserData에서 DownloadTopicData 딕셔너리를 가져옴
        var downloadTopicDataDict = DataManager.UserData.GetDownloadTopicDataList();

        // 딕셔너리를 반복하여 각 토픽 데이터를 처리
        foreach (var downloadTopicData in downloadTopicDataDict)
        {
            CollectTopicSlot newCollectTopicSlot = Instantiate(collectTopicSlotPrefab, genCollectTopicSlotParent);
            newCollectTopicSlot.SetSlot(downloadTopicData);

            foreach (CollectedPixelArtData collectedPixelArtData in downloadTopicData.GetCollectedPixelArtData())
            {
                CollectPixelArtSlot newCollectPixelArtSlot = Instantiate(collectPixelArtSlotPrefab, newCollectTopicSlot.GetCollectPixelArtParent());
                newCollectPixelArtSlot.SetSlot(collectedPixelArtData);
                newCollectPixelArtSlot.OnClick += CollectPixelArtSlotClickHandler;
            }
        }
    }

    private void CollectPixelArtSlotClickHandler(CollectedPixelArtData collectedPixelArtData)
    {
        collectPixelArtDetailPopup.SetPopup(collectedPixelArtData);
        collectPixelArtDetailPopup.gameObject.SetActive(true);
    }
}