using UnityEngine;

public abstract class BaseSceneUI : MonoBehaviour
{
    public void MoveScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}