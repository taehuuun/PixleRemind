using System;
using Firebase.Firestore;
using UnityEngine;

[FirestoreData, Serializable]
public class CollectPixelArtData : MonoBehaviour
{
    [FirestoreProperty] public string Title { get; set; }
    [FirestoreProperty] public string Description { get; set; }
    [FirestoreProperty] public string ThumbnailData { get; set; }
    [FirestoreProperty] public int PlayTime { get; set; }
}