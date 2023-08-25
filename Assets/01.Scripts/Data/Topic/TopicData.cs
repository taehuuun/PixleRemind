using System;
using System.Collections.Generic;
using Firebase.Firestore;

[FirestoreData, Serializable]
public class TopicData : BaseThumbnailData
{
    [FirestoreProperty] public int CompleteCount { get; private set; }
    [FirestoreProperty] public int TotalCount { get; private set; }
    [FirestoreProperty] public bool Complete { get; private set; }
    [FirestoreProperty] public bool Updateable { get; private set; }
    [FirestoreProperty] public bool IsLocked { get; private set; }
    [FirestoreProperty] public UnlockCondition UnlockCondition { get; private set; }
    [FirestoreProperty] public DateTime LastUpdated { get; private set; }
    [FirestoreProperty] public List<PixelArtData> PixelArtDataList { private get; set; }

    public TopicData() : this(string.Empty, string.Empty,string.Empty,string.Empty,0,0,0,false,false,false,new UnlockCondition(),DateTime.Now,new List<PixelArtData>()) { }

    public TopicData(
        string id,
        string title,
        string description,
        string thumbnailData,
        int thumbnailSize,
        int completeCount,
        int totalCount,
        bool complete,
        bool updateable,
        bool isLocked,
        UnlockCondition unlockCondition,
        DateTime lastUpdated,
        List<PixelArtData> pixelArtDataList) : base(id, title, description, thumbnailData, thumbnailSize)
    {
        CompleteCount = completeCount;
        TotalCount = totalCount;
        Complete = complete;
        Updateable = updateable;
        IsLocked = isLocked;
        UnlockCondition = unlockCondition;
        LastUpdated = lastUpdated;
        PixelArtDataList = pixelArtDataList;
    }
}