using System;
using System.Collections.Generic;
using Firebase.Firestore;

[FirestoreData, Serializable]
public class CustomPixel
{
    [FirestoreProperty] public CustomRGBA OriginalColor { get; private set; }
    [FirestoreProperty] public CustomRGBA GrayColor { get; private set; }
    [FirestoreProperty] public List<PixelCoord> PixelCoords { private get; set; }
    
    public CustomPixel(float r, float g, float b, float a)
    {
        float grayValue = GetGrayValue(r, g, b);

        OriginalColor = new CustomRGBA(r, g, b, a);
        GrayColor = new CustomRGBA(grayValue, grayValue, grayValue, a);
    }

    private CustomRGBA GetGrayValue(CustomRGBA origin)
    {
        float grayValue = (origin.R + origin.G + origin.B) / 3;
        return new CustomRGBA(grayValue, grayValue,grayValue,origin.A);
    }
}