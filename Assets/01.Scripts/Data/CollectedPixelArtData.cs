using System;
using Firebase.Firestore;

[FirestoreData, Serializable]
public class CollectedPixelArtData
{
    [FirestoreProperty] public string Title { get; private set; }
    [FirestoreProperty] public string ThumbnailData { get; private set; }
    [FirestoreProperty] public string Description { get; private set; }
    [FirestoreProperty] public int ThumbnailSize { get; private set; }

    public CollectedPixelArtData()
    {
        Title = "";
        ThumbnailData = "";
        Description = "";
        ThumbnailSize = 0;
    }
    public CollectedPixelArtData(string title, string thumbnailData, string description)
    {
        Title = title;
        ThumbnailData = thumbnailData;
        Description = description;
    }
}