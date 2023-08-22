using System;
using Firebase.Firestore;

[FirestoreData, Serializable]
public class PixelCoord
{
    [FirestoreProperty] public int X { get; set; }
    [FirestoreProperty] public int Y { get; set; }

    public PixelCoord()
    {
        X = 0;
        Y = 0;
    }

    public PixelCoord(int x, int y)
    {
        X = x;
        Y = y;
    }
}