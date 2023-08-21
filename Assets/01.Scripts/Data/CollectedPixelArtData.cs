using System;
using Firebase.Firestore;

[FirestoreData, Serializable]
public class CollectedPixelArtData : BaseThumbnailData
{
    [FirestoreProperty] public int PlayTime { get; private set; }

    public CollectedPixelArtData()
    {
        PlayTime = 0;
    }

    public CollectedPixelArtData(string id, string title, string description, string thumbnailData, int thumbnailSize,
        int playTime)
        : base(id, title, description, thumbnailData, thumbnailSize)
    {
        PlayTime = playTime;
    }
}