using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
    public TMP_Text titleText;
    public TMP_Text bodyText;
    public Button closeButton;

    public Transform buttonContainer;
    public Button buttonPrefab;

    private List<Button> _addedButtons = new List<Button>();

    public void SetTitle(string title)
    {
        titleText.text = title;
    }

    public void SetBody(string body)
    {
        bodyText.text = body;
    }

    public void AddButton(string text, UnityAction action)
    {
        Button newButton = Instantiate(buttonPrefab, buttonContainer);
        newButton.GetComponentInChildren<Text>().text = text;
        newButton.onClick.AddListener(action);
        
        _addedButtons.Add(newButton);
    }

    public void ClearAddedButtons()
    {
        foreach (var button in _addedButtons)
        {
            Destroy(button.gameObject);
        }
        
        _addedButtons.Clear();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}