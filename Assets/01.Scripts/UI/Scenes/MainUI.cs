using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : BodyUI
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
        
        List<string> localTopicIds = DataManager.instance.userData.LocalTopicDataIDs;

        for (int i = 0; i < localTopicIds.Count; i++)
        {
            TopicSlot topicSlot = Instantiate(topicSlotPrefab, topicSlotContainer);
            topicSlot.data = DataManager.LoadJsonData<TopicData>(DataPath.GalleryDataPath, localTopicIds[i]);
            topicSlot.SetSlot();
            topicSlot.OnClick += HandleTopicSlotClick;
        }
    }
    
    /// <summary>
    /// 해당 토픽 슬롯을 클릭 했을 때 호출되는 메서드
    /// </summary>
    /// <param name="clickedTopicData">클릭한 토픽 슬롯</param>
    private void HandleTopicSlotClick(TopicData clickedTopicData)
    {
        GalleryManager.ins.SelTopicData = clickedTopicData;
        LoadingTaskManager.Instance.NextSceneName = SceneNames.GalleryScene;
        MoveScene(SceneNames.LoadingScene);
    }
}