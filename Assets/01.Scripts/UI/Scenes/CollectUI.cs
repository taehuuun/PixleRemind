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
        List<DownloadTopicData> downloadTopicDataList = DataManager.userData.DownloadTopicDataList;

        foreach (DownloadTopicData downloadTopicData in downloadTopicDataList)
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

    private void CollectPixelArtSlotClickHandler(CollectedPixelArtData collectedPixelArtData)
    {
        collectPixelArtDetailPopup.SetPopup(collectedPixelArtData);
        collectPixelArtDetailPopup.gameObject.SetActive(true);
    }
}
