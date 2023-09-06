using TMPro;
using UnityEngine;

public class PopupTitle : PopupElement
{
    [SerializeField] private TMP_Text titleText;

    public void SetTitle(string title)
    {
        titleText.text = title;
    }
    public void SetTitleStyle(TMP_Style style)
    {
        titleText.textStyle = style;
    }
    
    public override void ResetElements()
    {
        titleText.text = string.Empty;
    }
    public override void UpdateElements() { }
}