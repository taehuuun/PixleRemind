using System.Collections.Generic;
using Firebase.Firestore;

[FirestoreData]
public class PixelColorData
{
    [FirestoreProperty] public int RemainingPixels { get; private set; }

    [FirestoreProperty] public List<CustomPixel> CustomPixels { private get; set; }

    public PixelColorData()
    {
        RemainingPixels = 0;
        CustomPixels = new List<CustomPixel>();
    }
}