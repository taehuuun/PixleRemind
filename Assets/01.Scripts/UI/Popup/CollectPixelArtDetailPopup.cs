using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CollectPixelArtDetailPopup : MonoBehaviour
{
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private Image thumbnailImage;

    public void OnSlotClick()
    {
        gameObject.SetActive(false);
    }
    
    public void SetPopup(CollectedPixelArtData collectedPixelArtData)
    {
        titleText.text = collectedPixelArtData.Title;
        descriptionText.text = collectedPixelArtData.Description;
        thumbnailImage.sprite = PixelArtHelper.MakeThumbnail(collectedPixelArtData.ThumbnailData, collectedPixelArtData.ThumbnailSize);
    }
}
