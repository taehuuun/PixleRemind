using UnityEngine;
using UnityEngine.UI;

public class MainUI : BaseSceneUI
{
    public UpdateManager updateManager;
    public Button playBtn;

    public Transform topicSlotContainer;
    public TopicSlot topicSlotPrefab;

    private void Start()
    {
        SetTopicSlot();
        
        // 다운로드 완료시 SetTopicSlot을 호출
        updateManager.OnDownloadCompleted+= SetTopicSlot;
    }

    /// <summary>
    /// 다운로드한 토픅 데이터를 메인 화면에 추가하는 메서드
    /// </summary>
    private void SetTopicSlot()
    {
        foreach (Transform child in topicSlotContainer)
        {
            Destroy(child.gameObject);
        }
        
        var localTopicIds = DataManager.LocalData.GetTopicDataKeys();

        foreach (var topicDataID in localTopicIds)
        {
            TopicSlot topicSlot = Instantiate(topicSlotPrefab, topicSlotContainer);
            topicSlot.data = DataManager.LocalData.GetTopicData(topicDataID);
            topicSlot.SetSlot();
        }
    }
}