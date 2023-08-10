using Firebase.Firestore;

public class BaseData
{
    [FirestoreProperty] public string ID { get; set; }
    [FirestoreProperty] public string Title { get; set; }
    [FirestoreProperty] public string Description { get; set; }
}