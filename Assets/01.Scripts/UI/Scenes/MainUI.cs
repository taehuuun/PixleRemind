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
        StartCoroutine(SetTopicSlot());
        
        updateManager.OnDownloadCompleted+= ()=>StartCoroutine(SetTopicSlot());
    }

    private IEnumerator SetTopicSlot()
    {
        foreach (Transform child in topicSlotContainer)
        {
            Destroy(child.gameObject);
        }
        
        List<string> localTopicIds = DataManager.Instance.userData.LocalTopicDataIDs;

        for (int i = 0; i < localTopicIds.Count; i++)
        {
            TopicSlot topicSlot = Instantiate(topicSlotPrefab, topicSlotContainer);
            topicSlot.data = DataManager.LoadJsonData<TopicData>(DataPath.GalleryDataPath, localTopicIds[i]);
            topicSlot.SetSlot();
            topicSlot.OnClick += HandleTopicSlotClick;
        }

        yield break;
    }

    private void HandleTopicSlotClick(TopicData clickedTopicData)
    {
        GalleryManager.ins.LoadPixelDataForTopic(clickedTopicData);
        LoadingTaskManager.Instance.NextSceneName = SceneNames.GalleryScene;
        MoveScene(SceneNames.GalleryScene);
    }
}