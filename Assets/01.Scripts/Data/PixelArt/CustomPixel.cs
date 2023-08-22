using System;
using System.Collections.Generic;
using Firebase.Firestore;

[FirestoreData, Serializable]
public class CustomPixel
{
    [FirestoreProperty] public CustomRGBA OriginalColor { get; private set; }
    [FirestoreProperty] public CustomRGBA GrayColor { get; private set; }
    [FirestoreProperty] public List<PixelCoord> PixelCoords { private get; set; }
    
    public CustomPixel(CustomRGBA customRgba)
    {
        OriginalColor = customRgba;
        GrayColor = GetGrayValue(customRgba);
        PixelCoords = new List<PixelCoord>();
    }

    private CustomRGBA GetGrayValue(CustomRGBA origin)
    {
        float grayValue = (origin.R + origin.G + origin.B) / 3;
        return new CustomRGBA(grayValue, grayValue,grayValue,origin.A);
    }
}