using System;
using Firebase.Firestore;

[FirestoreData, Serializable]
public class CollectedPixelArtData : BaseThumbnailData
{
    public CollectedPixelArtData() { }

    public CollectedPixelArtData(string id, string title, string description, string thumbnailData, int thumbnailSize)
        : base(id, title, description, thumbnailData, thumbnailSize) { }
}