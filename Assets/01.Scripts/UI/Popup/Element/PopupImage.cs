using UnityEngine;
using UnityEngine.UI;

public class PopupImage : PopupElement
{
    [SerializeField] private Image popupImage;

    public void SetImage(Sprite sprite)
    {
        popupImage.sprite = sprite;
    }
    public void SetImageColor(Color color)
    {
        popupImage.color = color;
    }
    
    public override void ResetElements()
    {
        popupImage.sprite = null;
    }
    public override void UpdateElements() { }
}