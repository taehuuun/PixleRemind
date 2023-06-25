using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text bodyText;
    [SerializeField] private Button closeButton;

    [SerializeField] private Transform buttonContainer;
    [SerializeField] private Button buttonPrefab;

    private List<Button> _addedButtons = new List<Button>();

    /// <summary>
    /// 팝업의 타이틀 텍스트 설정 메서드
    /// </summary>
    /// <param name="title">변경할 텍스트</param>
    public void SetTitle(string title)
    {
        titleText.text = title;
    }
    
    /// <summary>
    /// 본문 내용을 설정하는 메서드
    /// </summary>
    /// <param name="body">본문 내용</param>
    public void SetBody(string body)
    {
        bodyText.text = body;
    }
    /// <summary>
    /// 기본 팝업의 Close버튼을 세팅하는 메서드
    /// </summary>
    /// <param name="buttonText">버튼 텍스트</param>
    /// <param name="action">버튼 이벤트</param>
    public void SetCloseButton(string buttonText, UnityAction action)
    {
        closeButton.GetComponentInChildren<Text>().text = buttonText;
        closeButton.onClick.AddListener(action);
    }

    /// <summary>
    /// 팝업에 버튼을 추가하는 메서드
    /// </summary>
    /// <param name="text">추가할 버튼의 텍스트</param>
    /// <param name="action">추가할 버턴의 이벤트</param>
    public void AddButton(string text, UnityAction action)
    {
        Button newButton = Instantiate(buttonPrefab, buttonContainer);
        newButton.GetComponentInChildren<Text>().text = text;
        newButton.onClick.AddListener(action);
        
        _addedButtons.Add(newButton);
    }
    
    /// <summary>
    /// 추가한 버튼들을 제거하는 메서드
    /// </summary>
    public void ClearAddedButtons()
    {
        foreach (var button in _addedButtons)
        {
            Destroy(button.gameObject);
        }
        
        _addedButtons.Clear();
    }

    /// <summary>
    /// 팝업을 화면에 띄우는 메서드
    /// </summary>
    public void Show()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// 팝업을 화면에서 숨기는 메서드
    /// </summary>
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}