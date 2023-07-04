using System;
using System.Collections.Generic;
using Firebase.Firestore;

[FirestoreData, Serializable]
public class CollectTopicData
{
    [FirestoreProperty] public string Title { get; set; }
    [FirestoreProperty] public int CompleteCount { get; set; }
    [FirestoreProperty] public int TotalCount { get; set; }
    [FirestoreProperty] public List<CollectPixelArtData> CollectPixelArtDataList { get; set; }

    public CollectTopicData()
    {
        Title = "";
        CompleteCount = 0;
        TotalCount = 0;
        CollectPixelArtDataList = new List<CollectPixelArtData>();
    }

    public CollectTopicData(string title, int completeCount, int totalCount)
    {
        Title = title;
        CompleteCount = completeCount;
        TotalCount = totalCount;
        CollectPixelArtDataList = new List<CollectPixelArtData>();
    }
}