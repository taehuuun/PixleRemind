using System;
using Firebase.Firestore;

[FirestoreData, Serializable]
public class BaseData
{
    [FirestoreProperty] public string ID { get; private set; }
    [FirestoreProperty] public string Title { get; private set; }
    [FirestoreProperty] public string Description { get; private set; }
    
    public BaseData() : this(string.Empty, string.Empty,string.Empty) { }
    public BaseData(string id) :this(id,string.Empty,string.Empty) {}
    public BaseData(string id, string title) : this(id, title, string.Empty) {}
    public BaseData(string id, string title, string description)
    {
        ID = id;
        Title = title;
        Description = description;
    }
}