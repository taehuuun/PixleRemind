using UnityEngine;

public abstract class BaseSceneUI : MonoBehaviour
{
    private void Start()
    {
        Initialize();
    }
    
    protected abstract void Initialize();
}