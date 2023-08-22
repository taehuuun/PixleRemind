using System;
using Firebase.Firestore;

[FirestoreData, Serializable]
public class PixelCoord
{
    [FirestoreProperty] public int X { get; private set; }
    [FirestoreProperty] public int Y { get; private set; }

    public PixelCoord() : this(0, 0) {}
    public PixelCoord(int x, int y)
    {
        X = x;
        Y = y;
    }
}