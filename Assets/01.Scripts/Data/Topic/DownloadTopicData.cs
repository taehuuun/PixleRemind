using System;
using System.Collections.Generic;
using Firebase.Firestore;

[FirestoreData, Serializable]
public class DownloadTopicData : BaseData
{
    [FirestoreProperty] public int TotalPixelArtDataCount { get; private set; }
    [FirestoreProperty] public List<CollectedPixelArtData> CollectedPixelArtDataList { private get; set; }

    public DownloadTopicData() : this(string.Empty,string.Empty, string.Empty,0) { }
    public DownloadTopicData(string id, string title, string description, int totalPixelArtDataCount) : base(id, title, description)
    {
        TotalPixelArtDataCount = totalPixelArtDataCount;
        CollectedPixelArtDataList = new List<CollectedPixelArtData>();
    }

    public void AddCollectedPixelArt(CollectedPixelArtData data)
    {
        CollectedPixelArtDataList.Add(data);
    }
    public int GetCollectedPixelArtCount()
    {
        return CollectedPixelArtDataList.Count;
    }
}