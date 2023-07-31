using TMPro;
using UnityEngine;

public class CollectTopicSlot : MonoBehaviour
{
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private Transform genPixelArtSlotParent;

    public void SetSlot(DownloadTopicData downloadTopicData)
    {
        titleText.text = $"{downloadTopicData.Title} ({downloadTopicData.CollectedPixelArtDataList.Count} / {downloadTopicData.TotalPixelArtDataCount})";
    }

    public Transform GetCollectPixelArtParent()
    {
        return genPixelArtSlotParent;
    }
}