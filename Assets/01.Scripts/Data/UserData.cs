using System;
using System.Collections.Generic;
using Firebase.Firestore;

[FirestoreData, Serializable]
public class UserData
{
    [FirestoreProperty] public Dictionary<string,List<DownloadTopicData>> DownloadTopicData { get; set; }
    [FirestoreProperty] public DateTime LastUpdated { get; set; }

    public UserData()
    {
        DownloadTopicData = new Dictionary<string, List<DownloadTopicData>>();
        LastUpdated = new DateTime();
    }
}