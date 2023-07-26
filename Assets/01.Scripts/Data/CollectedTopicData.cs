using System;
using System.Collections.Generic;
using Firebase.Firestore;

[FirestoreData, Serializable]
public class CollectedTopicData
{
    [FirestoreProperty] public string Title { get; private set; }
    [FirestoreProperty] public string Description  { get; private set; }
    [FirestoreProperty] public int TotalPixelArtDataCount { get; private set; }
    [FirestoreProperty] public List<CollectedPixelArtData> CollectedPixelArtDataList { get; private set; }

    public CollectedTopicData(string title, string description, int totalPixelArtDataCount)
    {
        Title = title;
        Description = description;
        TotalPixelArtDataCount = totalPixelArtDataCount;
        CollectedPixelArtDataList = new List<CollectedPixelArtData>();
    }

    public void AddCollectedPixelArtData(CollectedPixelArtData collectedPixelArtData)
    {
        CollectedPixelArtDataList?.Add(collectedPixelArtData);
    }
}