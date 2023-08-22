using System;
using System.Collections.Generic;
using Firebase.Firestore;

[FirestoreData, Serializable]
public class TopicData : BaseThumbnailData
{
    [FirestoreProperty] public int CompleteCount { get; set; }
    [FirestoreProperty] public int TotalCount { get; set; }
    [FirestoreProperty] public bool Complete { get; set; }
    [FirestoreProperty] public bool Updateable { get; set; }
    [FirestoreProperty] public bool IsLocked { get; set; }
    [FirestoreProperty] public UnlockCondition UnlockCondition { get; set; }
    [FirestoreProperty] public DateTime LastUpdated { get; set; }
    [FirestoreProperty] public List<PixelArtData> PixelArtDataList { get; set; }

    public TopicData()
    {
        CompleteCount = 0;
        TotalCount = 0;
        Complete = false;
        Updateable = false;
        IsLocked = false;
        UnlockCondition = new UnlockCondition();
        LastUpdated = DateTime.Now;
        PixelArtDataList = new List<PixelArtData>();
    }

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