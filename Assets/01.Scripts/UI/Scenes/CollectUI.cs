using System.Collections.Generic;
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
        Dictionary<string, List<DownloadTopicData>> downloadTopicDataDict = DataManager.UserData.DownloadTopicData;

        // 딕셔너리를 반복하여 각 토픽 데이터를 처리
        foreach (var downloadTopicDataPair in downloadTopicDataDict)
        {
            // downloadTopicDataPair.Value는 DownloadTopicData 리스트
            foreach (DownloadTopicData downloadTopicData in downloadTopicDataPair.Value)
            {
                CollectTopicSlot newCollectTopicSlot = Instantiate(collectTopicSlotPrefab, genCollectTopicSlotParent);
                newCollectTopicSlot.SetSlot(downloadTopicData);

                foreach (CollectedPixelArtData collectedPixelArtData in downloadTopicData.CollectedPixelArtDataList)
                {
                    CollectPixelArtSlot newCollectPixelArtSlot = Instantiate(collectPixelArtSlotPrefab, newCollectTopicSlot.GetCollectPixelArtParent());
                    newCollectPixelArtSlot.SetSlot(collectedPixelArtData);
                    newCollectPixelArtSlot.OnClick += CollectPixelArtSlotClickHandler;
                }
            }
        }
    }

    private void CollectPixelArtSlotClickHandler(CollectedPixelArtData collectedPixelArtData)
    {
        collectPixelArtDetailPopup.SetPopup(collectedPixelArtData);
        collectPixelArtDetailPopup.gameObject.SetActive(true);
    }
}