using System;
using System.Collections.Generic;
using Firebase.Firestore;

namespace LTH.PixelRemind.Data
{
    [FirestoreData, Serializable]
    public class UserData
    {
        [FirestoreProperty] public List<string> LocalTopicDataIDs { get; set; }
        [FirestoreProperty] public DateTime LastUpdated { get; set; }

        public UserData()
        {
            LocalTopicDataIDs = new List<string>();
        }
    }
}
