using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CollectPixelArtSlot : MonoBehaviour
{
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private Image thumbnailImage;
    private CollectedPixelArtData _collectPixelArtData;

    public delegate void CollectPixelArtSlotClickHandler(CollectedPixelArtData collectedPixelArtData);
    public event CollectPixelArtSlotClickHandler OnClick;

    public void OnSlotClick()
    {
        OnClick?.Invoke(_collectPixelArtData);
    }
    public void SetSlot(CollectedPixelArtData collectedPixelArtData)
    {
        _collectPixelArtData = collectedPixelArtData;
        titleText.text = $"{_collectPixelArtData.Title}";
        thumbnailImage.sprite = PixelArtHelper.MakeThumbnail(_collectPixelArtData.ThumbnailData, _collectPixelArtData.ThumbnailSize);
    }
}
