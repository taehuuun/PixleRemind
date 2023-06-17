using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : BodyUI
{
    public UpdateManager updateManager;
    public Button playBtn;

    public Transform topicSlotContainer;
    public Transform pixelArtSlotContainer;

    public TopicSlot topicSlotPrefab;
    public PixelArtSlot pixelArtSlotPrefab;

    public GameObject galleryScreen;

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
            topicSlot.name = topicSlot.data.Title;
            topicSlot.SetSlot();
            topicSlot.OnClick += HandleTopicSlotClick;
        }

        yield break;
    }

    private void HandleTopicSlotClick(TopicData clickedTopicData)
    {
        GalleryManager.ins.LoadPixelDataForTopic(clickedTopicData);
        galleryScreen.SetActive(true);

        foreach (Transform child in pixelArtSlotContainer)
        {
            Destroy(child.gameObject);
        }

        foreach (var pixelArtData in clickedTopicData.PixelArtDatas)
        {
            PixelArtSlot pixelArtSlot = Instantiate(pixelArtSlotPrefab, pixelArtSlotContainer);
            pixelArtSlot.pixelData = pixelArtData;
            pixelArtSlot.SetSlot();
        }
    }
}