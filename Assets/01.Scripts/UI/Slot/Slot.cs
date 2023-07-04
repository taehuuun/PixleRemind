using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class Slot : MonoBehaviour
{
    public TMP_Text titleText;
    public Image thumbnailImb;

    public virtual void OnSlotClick() { }

    public abstract void SetSlot();
}