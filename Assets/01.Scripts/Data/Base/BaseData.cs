using System;
using Firebase.Firestore;

[FirestoreData, Serializable]
public class BaseData
{
    [FirestoreProperty] public string ID { get; set; }
    [FirestoreProperty] public string Title { get; set; }
    [FirestoreProperty] public string Description { get; set; }
    
    public BaseData() : this(string.Empty, string.Empty,string.Empty) { }
    public BaseData(string id) :this(id,string.Empty,string.Empty) {}
    public BaseData(string id, string title) : this(id, title, string.Empty) {}
    public BaseData(string id, string title, string description)
    {
        ID = id;
        Title = title;
        Description = description;
    }

    public void SetID(string id)
    {
        ID = id;
    }

    public void SetTitle(string title)
    {
        Title = title;
    }

    public void SetDescription(string description)
    {
        Description = description;
    }
}