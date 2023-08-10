using System;
using Firebase.Firestore;

[FirestoreData, Serializable]
public class BaseData
{
    [FirestoreProperty] public string ID { get; set; }
    [FirestoreProperty] public string Title { get; set; }
    [FirestoreProperty] public string Description { get; set; }
    
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