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

    // Update is called once per frame
    void Update()
    {
        
    }
}
