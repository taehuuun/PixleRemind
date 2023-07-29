using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CollectPixelArtSlot : MonoBehaviour
{
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private Image thumbnailImage;
    private CollectedPixelArtData _collectPixelArtData;

    public void SetSlot(CollectedPixelArtData collectedPixelArtData)
    {
        _collectPixelArtData = collectedPixelArtData;
        titleText.text = $"{_collectPixelArtData.Title}";
        thumbnailImage.sprite = PixelArtHelper.MakeThumbnail(_collectPixelArtData.ThumbnailData, _collectPixelArtData.ThumbnailSize);
    }
}
