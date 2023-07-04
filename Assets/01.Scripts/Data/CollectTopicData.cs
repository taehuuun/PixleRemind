using System;
using System.Collections.Generic;
using Firebase.Firestore;

[FirestoreData, Serializable]
public class CollectTopicData
{
    [FirestoreProperty] public string ID { get; set; }
    [FirestoreProperty] public string Title { get; set; }
    [FirestoreProperty] public string ThumbnailData { get; set; }
    [FirestoreProperty] public int ThumbnailSize { get; set; }
    [FirestoreProperty] public int CompleteCount { get; set; }
    [FirestoreProperty] public int TotalCount { get; set; }
    [FirestoreProperty] public List<CollectPixelArtData> CollectPixelArtDataList { get; set; }

    public CollectTopicData()
    {
        ID = "";
        Title = "";
        ThumbnailData = "";
        ThumbnailSize = 0;
        CompleteCount = 0;
        TotalCount = 0;
        CollectPixelArtDataList = new List<CollectPixelArtData>();
    }

    public CollectTopicData(string id, string title,string thumbnailData,int thumbnailSize, int completeCount, int totalCount)
    {
        ID = id;
        Title = title;
        ThumbnailData = thumbnailData;
        ThumbnailSize = thumbnailSize;
        CompleteCount = completeCount;
        TotalCount = totalCount;
        CollectPixelArtDataList = new List<CollectPixelArtData>();
    }
}