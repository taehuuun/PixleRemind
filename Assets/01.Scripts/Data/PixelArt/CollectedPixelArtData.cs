using System;
using Firebase.Firestore;

[FirestoreData, Serializable]
public class CollectedPixelArtData : BaseThumbnailData
{
    [FirestoreProperty] public int PlayTime { get; set; }

    public CollectedPixelArtData() : this(string.Empty, string.Empty, string.Empty, string.Empty, 0, 0){}

    public CollectedPixelArtData(string id, string title, string description, string thumbnailData, int thumbnailSize,
        int playTime)
        : base(id, title, description, thumbnailData, thumbnailSize)
    {
        PlayTime = playTime;
    }
}