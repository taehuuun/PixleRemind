using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class PopupManager : MonoBehaviour
{
    public static PopupManager Instance;

    public Popup defaultPopup;
    
    public Transform popupParent;
    public Popup popupPrefab;

    private Stack<Popup> _popupStack = new Stack<Popup>();

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
        Popup popup;
        if (_popupStack.Count == 0)
        {
            popup = defaultPopup;
        }
        else
        {
            popup = Instantiate(popupPrefab, popupParent);
        }
        
        popup.SetTitle(title);
        popup.SetBody(body);
        popup.Show();
        _popupStack.Push(popup);
    }

    public void ClosePopup()
    {
        if (_popupStack.Count == 0)
        {
            return;
        }
        
        if (_popupStack.Count == 1)
        {
            _popupStack.Pop();
            defaultPopup.Hide();
            defaultPopup.ClearAddedButtons();
            return;
        }
        
        var topPopup = _popupStack.Pop();
        Destroy(topPopup.gameObject);
    }

    public void AddButtonToCurrentPopup(string buttonText, UnityAction action)
    {
        if (_popupStack.Count > 0)
        {
            var topPopup = _popupStack.Peek();
            topPopup.AddButton(buttonText, action);
        }
    }
    
    public bool IsPopupOpen()
    {
        return _popupStack.Count > 0;
    }
}
