using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class PopupManager : MonoBehaviour
{
    public static PopupManager Instance;
    public Popup popupPrefab;

    private Stack<Popup> popupStack = new Stack<Popup>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Backspace))
        {
            ClosePopup();
        }
    }

    public void ShowPopup(string title, string body)
    {
        var newPopup = Instantiate(popupPrefab);
        newPopup.SetTitle(title);
        newPopup.SetBody(body);
        newPopup.Show();
        popupStack.Push(newPopup);
    }

    public void ClosePopup()
    {
        if (popupStack.Count > 0)
        {
            var topPopup = popupStack.Pop();
            Destroy(topPopup.gameObject);
        }
    }

    public void AddButtonToCurrentPopup(string buttonText, UnityAction action)
    {
        if (popupStack.Count > 0)
        {
            var topPopup = popupStack.Peek();
            topPopup.AddButton(buttonText, action);
        }
    }
    
    public bool IsPopupOpen()
    {
        return popupStack.Count > 0;
    }
}
