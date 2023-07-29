using TMPro;
using UnityEngine;

public class CollectTopicSlot : MonoBehaviour
{
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private Transform genPixelArtSlotParent;
    [SerializeField] private CollectPixelArtSlot collectPixelArtSlotPrefab;

    public void SetSlot(CollectedTopicData collectedTopicData)
    {
        titleText.text = $"{collectedTopicData.Title} ({collectedTopicData.CollectedPixelArtDataList.Count} / {collectedTopicData.TotalPixelArtDataCount})";

        for (int i = 0; i < collectedTopicData.CollectedPixelArtDataList.Count; i++)
        {
            CollectPixelArtSlot newCollectPixelArtSlot = Instantiate(collectPixelArtSlotPrefab, genPixelArtSlotParent);
        } 
    }
}