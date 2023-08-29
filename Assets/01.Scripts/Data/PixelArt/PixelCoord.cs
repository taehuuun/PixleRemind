using System;
using Firebase.Firestore;

[FirestoreData, Serializable]
public class PixelCoord
{
    [FirestoreProperty] public int X { get; set; }
    [FirestoreProperty] public int Y { get; set; }

    public PixelCoord() : this(0, 0) {}
    public PixelCoord(int x, int y)
    {
        X = x;
        Y = y;
    }
}