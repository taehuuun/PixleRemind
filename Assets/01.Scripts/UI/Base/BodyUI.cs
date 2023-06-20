using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class BodyUI : MonoBehaviour
{
    protected readonly Stack<CloseAbleUI> activePopups = new Stack<CloseAbleUI>();

    protected virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace) && activePopups.Count > 0)
        {
            ClosePopup();
        }
    }

    public void OpenPopup(CloseAbleUI popup)
    {
        if (popup == null)
            return;

        popup.gameObject.SetActive(true);

        if (!popup.isLock)
            activePopups.Push(popup);
    }

    public void ClosePopup()
    {
        if (activePopups.Count == 0)
            return;

        activePopups.Pop().gameObject.SetActive(false);
    }

    public void MoveScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}