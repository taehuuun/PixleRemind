using UnityEngine;

public abstract class PopupElement : MonoBehaviour
{
    public bool IsActive => gameObject.activeSelf;

    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
    public abstract void ResetElements();
    public abstract void UpdateElements();
}