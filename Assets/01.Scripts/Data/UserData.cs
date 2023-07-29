using System;
using System.Collections.Generic;
using Firebase.Firestore;

[FirestoreData, Serializable]
public class UserData
{
    [FirestoreProperty] public List<string> LocalTopicDataIDs { get; set; }
    [FirestoreProperty] public List<CollectedTopicData> CollectedTopicDataList { get; set; }
    [FirestoreProperty] public DateTime LastUpdated { get; set; }
    [FirestoreProperty] public string SelectTopicID { get; set; } 
    [FirestoreProperty] public string SelectPixelArtID { get; set; }

    public UserData()
    {
        LocalTopicDataIDs = new List<string>();
        CollectedTopicDataList = new List<CollectedTopicData>();
    }
}