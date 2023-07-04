using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Serialization;

public abstract class Slot : MonoBehaviour
{
    public TMP_Text titleText;
    [FormerlySerializedAs("thumbnailImb")] public Image thumbnailImage;

    public virtual void OnSlotClick() { }

    public abstract void SetSlot();
}