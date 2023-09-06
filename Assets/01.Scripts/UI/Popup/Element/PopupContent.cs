using TMPro;
using UnityEngine;

public class PopupContent : PopupElement
{
    [SerializeField] private TMP_Text contentText;

    public void SetContent(string content)
    {
        contentText.text = content;
    }
    public void SetContentStyle(TMP_Style style)
    {
        contentText.textStyle = style;
    }
    
    public override void ResetElements()
    {
        contentText.text = string.Empty;
    }
    public override void UpdateElements() {}
}