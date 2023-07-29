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
        List<CollectedTopicData> curCollectTopicDataList = DataManager.userData.CollectedTopicDataList;

        foreach (CollectedTopicData collectedTopicData in curCollectTopicDataList)
        {
            CollectTopicSlot newCollectTopicSlot = Instantiate(collectTopicSlotPrefab, genCollectTopicSlotParent);
            newCollectTopicSlot.SetSlot(collectedTopicData);

            foreach (CollectedPixelArtData collectedPixelArtData in collectedTopicData.CollectedPixelArtDataList)
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
