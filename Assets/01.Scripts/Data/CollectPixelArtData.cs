using System;
using Firebase.Firestore;

[FirestoreData, Serializable]
public class CollectPixelArtData
{
    [FirestoreProperty] public string Title { get; set; }
    [FirestoreProperty] public string Description { get; set; }
    [FirestoreProperty] public string ThumbnailData { get; set; }
    [FirestoreProperty] public int PlayTime { get; set; }
}