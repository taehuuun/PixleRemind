using UnityEngine;

public abstract class Page : MonoBehaviour
{
    public abstract void Open();
    public abstract void Close();
    protected abstract void SetupSlots(); 
}
