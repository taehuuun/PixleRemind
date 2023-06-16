using UnityEngine;
using UnityEngine.UI;

public class GalleryTopicPage : Page
{
    [SerializeField] private GalleryUI ui;
    public TopicSlot[] topicSlots;

    private void OnEnable()
    {
        GalleryManager.ins.TopicDatas.Clear();
        GalleryManager.ins.LoadTopicDataFromFiles();

        SetPage();
    }

    private void OnDisable()
    {
        for (int i = 0; i < topicSlots.Length; i++)
        {
            topicSlots[i].gameObject.SetActive(false);
            topicSlots[i].data = null;
        }
    }

    private void SetPage()
    {
        GalleryManager.ins.CurPage = GalleryPage.Topic;
        SetTopicSlot();
    }

    private void SetTopicSlot()
    {
        for (int i = 0; i < GalleryManager.ins.TopicDatas.Count; i++)
        {
            TopicData curTopicData = GalleryManager.ins.TopicDatas[i];
            topicSlots[i].gameObject.SetActive(true);
            topicSlots[i].titleText.text = curTopicData.ID;
            topicSlots[i].data = curTopicData;

            int topicDataIdx = i;
            GalleryPage page = GalleryPage.PixelArt;

            topicSlots[i].GetComponent<Button>().onClick.AddListener(() =>
            {
                GalleryManager.ins.SelTopicIdx = topicDataIdx;
                ui.SelectPage(page);
            });
            topicSlots[i].SetSlot();
        }
    }
}