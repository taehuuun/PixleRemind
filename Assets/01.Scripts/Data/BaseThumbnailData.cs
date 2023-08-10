using System;
using Firebase.Firestore;

[FirestoreData, Serializable]
public class BaseThumbnailData : BaseData
{
    [FirestoreProperty] public string ThumbnailData { get; set; }
    [FirestoreProperty] public int ThumbnailSize { get; set; }

    public BaseThumbnailData()
    {
        ThumbnailData = string.Empty;
        ThumbnailSize = 0;
    }

    public BaseThumbnailData(string id, string title, string description, string thumbnailData, int thumbnailSize) 
        : base(id, title, description)
    {
        ThumbnailData = thumbnailData;
        ThumbnailSize = thumbnailSize;
    }
}