using System;
using Firebase.Firestore;

[FirestoreData, Serializable]
public class BaseThumbnailData : BaseData
{
    [FirestoreProperty] public string ThumbnailData { get; private set; }
    [FirestoreProperty] public int ThumbnailSize { get; private set; }

    public BaseThumbnailData() : this(string.Empty, string.Empty, string.Empty, string.Empty, 0) { }

    public BaseThumbnailData(string id, string title, string description, string thumbnailData, int thumbnailSize) 
        : base(id, title, description)
    {
        ThumbnailData = thumbnailData;
        ThumbnailSize = thumbnailSize;
    }

    public void UpdateThumbnailData(string thumbnailData, int thumbnailSize)
    {
        ThumbnailData = thumbnailData;
        ThumbnailSize = thumbnailSize;
    }
}