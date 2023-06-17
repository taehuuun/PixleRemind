using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UpdatePopup : CloseAbleUI
{
    public Transform contentParent;
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

    public void Show(List<TopicData> updateDataList, List<TopicData> missingDataList)
    {
        Debug.Log("UpdatePopup Show");
        Debug.Log($"Update List Count : {updateDataList.Count}");
        Debug.Log($"Missing List Count : {missingDataList.Count}");
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

    private async void OnUpdateButtonClicked()
    {
        var selectedSlots = _topicSlots.Where(slot => slot.IsSelected).ToList();

        if (DataManager.Instance.userData.LocalTopicDataIDs.Count == 0 && selectedSlots.Count == 0)
        {
            Debug.Log("최소 1개 이상 슬롯을 선택 해야 합니다.");
            return;
        }

        foreach (var slot in selectedSlots)
        {
            Debug.Log($"선택된 Topic : {slot.titleText.text}");
            Debug.Log($"선택된 Topic ID : {slot.GetTopicData().ID}");
            await updateManager.DownloadTopicData(slot.GetTopicData().ID);
        }
    }
    private void OnCloseButtonClicked()
    {
        var selectedSlots = _topicSlots.Where(slot => slot.IsSelected).ToList();

        if (DataManager.Instance.userData.LocalTopicDataIDs.Count == 0 && selectedSlots.Count == 0)
        {
            Debug.Log("최소 1개 이상 슬롯을 선택 해야 합니다.");
            return;
        }

        gameObject.SetActive(false);
    }
    private void HandleDownloadCompleted()
    {
        Debug.Log("다운로드를 성공적으로 완료 함");
        gameObject.SetActive(false);
    }
    private void HandleDownloadFailed(Exception e)
    {
        Debug.LogError($"다운로드 실패 : {e.Message}");
    }
}