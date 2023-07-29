using System;
using System.Collections.Generic;
using Firebase.Firestore;

[FirestoreData, Serializable]
public class CollectedTopicData
{
    [FirestoreProperty] public string ID { get; private set; }
    [FirestoreProperty] public string Title { get; private set; }
    [FirestoreProperty] public string Description { get; private set; }
    [FirestoreProperty] public int TotalPixelArtDataCount { get; private set; }
    [FirestoreProperty] public List<CollectedPixelArtData> CollectedPixelArtDataList { get; private set; }

    public CollectedTopicData()
    {
        ID = "";
        Title = "";
        Description = "";
        TotalPixelArtDataCount = 0;
        CollectedPixelArtDataList = new List<CollectedPixelArtData>();
    }

public CollectedTopicData(string id, string title, string description, int totalPixelArtDataCount)
    {
        ID = id;
        Title = title;
        Description = description;
        TotalPixelArtDataCount = totalPixelArtDataCount;
        CollectedPixelArtDataList = new List<CollectedPixelArtData>();
    }
}