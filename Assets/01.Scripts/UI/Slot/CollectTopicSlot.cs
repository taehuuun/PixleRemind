using TMPro;
using UnityEngine;

public class CollectTopicSlot : MonoBehaviour
{
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private Transform genPixelArtSlotParent;

    public void SetSlot(CollectedTopicData collectedTopicData)
    {
        titleText.text = $"{collectedTopicData.Title} ({collectedTopicData.CollectedPixelArtDataList.Count} / {collectedTopicData.TotalPixelArtDataCount})";

    public Transform GetCollectPixelArtParent()
    {
        return genPixelArtSlotParent;
    }
}