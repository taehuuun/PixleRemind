using System;
using Firebase.Firestore;

[FirestoreData, Serializable]
public class BaseData
{
    [FirestoreProperty] public string ID { get; private set; }
    [FirestoreProperty] public string Title { get; private set; }
    [FirestoreProperty] public string Description { get; private set; }
    
    public BaseData()
    {
        ID = string.Empty;
        Title = string.Empty;
        Description = string.Empty;
    }
    
    public BaseData(string id, string title, string description)
    {
        ID = id;
        Title = title;
        Description = description;
    }
}