using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class BaseSceneUI : MonoBehaviour
{
    public void MoveScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}