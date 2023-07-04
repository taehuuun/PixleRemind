using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class BodyUI : MonoBehaviour
{
    public void MoveScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}