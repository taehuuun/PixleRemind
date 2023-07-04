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
}