using System;
using Firebase.Firestore;

[FirestoreData, Serializable]
public class UnlockCondition
{
    // 잠금 해제 타입
    [FirestoreProperty] public UnlockType Type { get; set; }
    
    // 타입별 해제 조건 
    [FirestoreProperty] public int Count { get; set; }

    public UnlockCondition()
    {
        Type = UnlockType.CoinPurchase;
        Count = 0;
    }

    public UnlockCondition(UnlockType type, int count)
    {
        Type = type;
        Count = count;
    }
}