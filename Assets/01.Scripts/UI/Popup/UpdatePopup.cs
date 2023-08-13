using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UpdatePopup : CloseAbleUI
{
    // 토픽 데이터들을 추가할 Transform
    public Transform contentParent;
    
    // 스크롤뷰에 추가될 UpdateTopicSlot 프리팹
    public UpdateSlot updateTopicSlotPrefab;
    public Button updateButton;
    public Button closeButton;

    public UpdateManager updateManager;

    private List<UpdateSlot> _topicSlots = new List<UpdateSlot>();

    private void Start()
    {
        updateButton.onClick.AddListener(OnUpdateButtonClicked);
        closeButton.onClick.AddListener(OnCloseButtonClicked);

        updateManager.OnDownloadCompleted += HandleDownloadCompleted;
        updateManager.OnDownloadFailed += HandleDownloadFailed;
    }

    /// <summary>
    /// 업데이트 팝업을 표시하는 메서드
    /// </summary>
    /// <param name="updateDataList">새로 추가된 토픽 데이터 리스트</param>
    /// <param name="missingDataList">아직 다운로드 하지 않은 데이터 리스트</param>
    public void Show(List<TopicData> updateDataList, List<TopicData> missingDataList)
    {
        gameObject.SetActive(true);

        foreach (var topicSlot in _topicSlots)
        {
            Destroy(topicSlot.gameObject);
        }

        _topicSlots.Clear();

        foreach (var topicData in updateDataList)
        {
            var topicSlot = Instantiate(updateTopicSlotPrefab, contentParent);
            topicSlot.SetSlot(topicData, false);
            _topicSlots.Add(topicSlot);
        }

        foreach (var topicData in missingDataList)
        {
            var topicSlot = Instantiate(updateTopicSlotPrefab, contentParent);
            topicSlot.SetSlot(topicData, true);
            _topicSlots.Add(topicSlot);
        }
    }

    /// <summary>
    /// 업데이트 버튼을 눌렀을때 호출되는 메서드
    /// </summary>
    private async void OnUpdateButtonClicked()
    {
        var selectedSlots = _topicSlots.Where(slot => slot.IsSelected).ToList();

        if (DataManager.localData.LocalTopicData.Keys.Count == 0 && selectedSlots.Count == 0)
        {
            PopupManager.Instance.ShowPopup("경고",$"최소 1개 이상 슬롯을 선택 해야 합니다.");
            return;
        }

        foreach (var slot in selectedSlots)
        {
            await updateManager.DownloadTopicData(slot.GetTopicData().ID);
        }
    }
    
    /// <summary>
    /// 닫기 버튼을 눌렀을 떄 호출되는 메서드
    /// </summary>
    private void OnCloseButtonClicked()
    {
        var selectedSlots = _topicSlots.Where(slot => slot.IsSelected).ToList();
        
        if (DataManager.localData.LocalTopicData.Keys.Count == 0 && selectedSlots.Count == 0)
        {
            PopupManager.Instance.ShowPopup("경고",$"최소 1개 이상 슬롯을 선택 해야 합니다.");
            // Debug.Log("최소 1개 이상 슬롯을 선택 해야 합니다.");
            return;
        }

        gameObject.SetActive(false);
    }
    /// <summary>
    /// 다운로드가 완료되었을 때 호출되는 메서드
    /// </summary>
    private void HandleDownloadCompleted()
    {
        PopupManager.Instance.ShowPopup("다운로드 완료",$"데이터를 모두 다운로드 하였습니다");
        gameObject.SetActive(false);
    }
    /// <summary>
    /// 다운로드가 실패 했을 때 호출되는 메서드
    /// </summary>
    /// <param name="e">예외 객체</param>
    private void HandleDownloadFailed(Exception e)
    {
        PopupManager.Instance.ShowPopup("다운로드 실패",$"{e.Message}");
    }
}