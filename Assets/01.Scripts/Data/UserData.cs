using System;
using System.Collections.Generic;
using Firebase.Firestore;
using UnityEngine;

namespace LTH.PixelRemind.Data
{
    [FirestoreData, Serializable]
    public class UserData
    {
        [FirestoreProperty] public List<string> LocalTopicDataIDs { get; set; }
        [FirestoreProperty] public int LocalTopicDataVersion { get; set; }
        public UserData() { }
    }
}
