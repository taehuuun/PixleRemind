using System;
using Firebase.Firestore;
using LTH.PixelRemind.Enums;

namespace LTH.PixelRemind.Data.Conditions
{
    [FirestoreData, Serializable]
    public class UnlockCondition
    {
        [FirestoreProperty] public UnlockType Type { get; set; }
        [FirestoreProperty] public int Count { get; set; }
        
        public UnlockCondition (){}

        public UnlockCondition(UnlockType type, int count)
        {
            Type = type;
            Count = count;
        }
    }
}
