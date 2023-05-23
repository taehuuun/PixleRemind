using System;
using System.Collections.Generic;
using Firebase.Firestore;
using UnityEngine;

namespace LTH.ColorMatch.Data
{
    [FirestoreData, Serializable]
    public class UserData : MonoBehaviour
    {
        [FirestoreProperty] public List<string> LocalTopicDataIDs { get; set; }
        [FirestoreProperty] public int LocalTopicDataVersion { get; set; }
        public UserData() { }
    }
}
