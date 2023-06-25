using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class PopupManager : MonoBehaviour
{
    public static PopupManager Instance;

    [SerializeField] private Popup defaultPopup;
    [SerializeField] private Transform popupParent;
    [SerializeField] private Popup popupPrefab;

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
        // 뒤로가기키를 누를 경우 팝업이 닫히도록 구현
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Backspace))
        {
            ClosePopup();
        }
    }
    
    /// <summary>
    /// 팝업을 활성화 하는 메서드
    /// </summary>
    /// <param name="title">팝업 타이틀</param>
    /// <param name="body">팝업 내용</param>
    public void ShowPopup(string title, string body)
    {
        Popup popup;
        
        // 활성화 된 팝업이 없다면 default 팝업을 활성화
        // 아니라면, 추가 팝업이 필요함으로 팝업 생성
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
        popup.SetCloseButton("닫기", ClosePopup);
        popup.Show();
        _popupStack.Push(popup);
    }

    /// <summary>
    /// 팝업을 닫는 메서드
    /// </summary>
    public void ClosePopup()
    {
        // 스택이 비어있으면 리턴
        if (_popupStack.Count == 0)
        {
            return;
        }
        
        // 한개 남아있다면 defaultPopup이므로
        // Hide후 ClearAddedButtons 진행
        if (_popupStack.Count == 1)
        {
            _popupStack.Pop();
            defaultPopup.Hide();
            defaultPopup.ClearAddedButtons();
            return;
        }
        
        // 이외에는 추가 생성된 팝업이므로 Pop후, 오브젝트 삭제
        var topPopup = _popupStack.Pop();
        Destroy(topPopup.gameObject);
    }

    /// <summary>
    /// 현재 팝업에 버튼을 추가하는 메서드
    /// </summary>
    /// <param name="buttonText">버튼 텍스트</param>
    /// <param name="action">버튼 이벤트</param>
    public void AddButtonToCurrentPopup(string buttonText, UnityAction action)
    {
        if (_popupStack.Count > 0)
        {
            var topPopup = _popupStack.Peek();
            topPopup.AddButton(buttonText, action);
        }
    }
    
    /// <summary>
    /// 외부 클래스에서 활성화된 팝업이 있는지 체크하는 메서드
    /// </summary>
    /// <returns>활성된 팝업 유무 반환</returns>
    public bool IsPopupOpen()
    {
        return _popupStack.Count > 0;
    }
}
