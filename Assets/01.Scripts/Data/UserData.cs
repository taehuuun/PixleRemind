using System;
using System.Collections.Generic;
using Firebase.Firestore;

[FirestoreData, Serializable]
public class UserData
{
    [FirestoreProperty] public List<DownloadTopicData> DownloadTopicDataList { get; set; }
    [FirestoreProperty] public DateTime LastUpdated { get; set; }

    public UserData()
    {
        DownloadTopicDataList = new List<DownloadTopicData>();
        LastUpdated = new DateTime();
    }
}