using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class GalleryManager : MonoBehaviour
{
    public static GalleryManager ins;

    public GalleryPage CurPage { get; set; }
    public PixelArtData SelPixelArtData { get; set; }
    public List<PixelArtData> PixelArtDatas { get; private set; }

    private void Awake()
    {
        if (ins == null)
        {
            ins = this;
            DontDestroyOnLoad(this);
        }

        PixelArtDatas = new List<PixelArtData>();
    }

    // public void LoadTopicDataFromFiles()
    // {
    //     List<string> topicNames = DataManager.GetTargetFolderFileNames(DataPath.GalleryDataPath);
    //
    //     foreach (var topicName in topicNames)
    //     {
    //         TopicDatas.Add(GetTopicData(topicName));
    //     }
    // }

    public void LoadPixelDataForTopic(TopicData topicData)
    {
        PixelArtDatas = topicData.PixelArtDatas;
    }

    // public void UpdateAndSavePixelArtData(PixelArtData updateData)
    // {
    //     TopicData saveTopic = TopicDatas[SelTopicIdx];
    //     saveTopic.PixelArtDatas[SelPixelArtIdx] = updateData;
    //     saveTopic.Complete = saveTopic.CompleteCount == saveTopic.TotalCount;
    //     saveTopic.ThumbData = updateData.ThumbnailData;
    //     SavePixelArtData(saveTopic);
    // }

    private void SavePixelArtData(TopicData data)
    {
        string jsonData = JsonConvert.SerializeObject(data);
        DataManager.SaveJsonData(DataPath.GalleryDataPath, data.ID, jsonData);
    }

    private static TopicData GetTopicData(string topicName)
    {
        return DataManager.LoadJsonData<TopicData>(DataPath.GalleryDataPath, topicName);
    }
}