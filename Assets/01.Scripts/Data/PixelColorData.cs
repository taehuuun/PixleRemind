using System.Collections.Generic;
using Firebase.Firestore;

[FirestoreData]
public class PixelColorData
{
    [FirestoreProperty] public int RemainingPixels { get; set; }

    [FirestoreProperty] public List<CustomPixel> CustomPixels { get; set; } = new List<CustomPixel>();

    public PixelColorData()
    {
        RemainingPixels = 0;
        CustomPixels = new List<CustomPixel>();
    }
}