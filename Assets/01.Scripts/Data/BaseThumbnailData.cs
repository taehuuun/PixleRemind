using System;
using Firebase.Firestore;

[FirestoreData, Serializable]
public class BaseThumbnailData : BaseData
{
    [FirestoreProperty] public string ThumbnailData { get; set; }
    [FirestoreProperty] public int ThumbnailSize { get; set; }
}
