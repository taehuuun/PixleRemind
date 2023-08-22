using System;
using System.Collections.Generic;
using Firebase.Firestore;

[FirestoreData, Serializable]
public class CustomPixel
{
    [FirestoreProperty] public CustomRGBA OriginalColor { get; private set; }
    [FirestoreProperty] public CustomRGBA GrayColor { get; private set; }
    [FirestoreProperty] public List<PixelCoord> PixelCoords { get; private set; }

    public CustomPixel()
    {
        OriginalColor = new CustomRGBA(0, 0, 0, 0);
        GrayColor = new CustomRGBA(0, 0, 0, 0);
        PixelCoords = new List<PixelCoord>();
    }

    public CustomPixel(CustomRGBA customRgba)
    {
        float grayValue = GetGrayValue(customRgba.R, customRgba.G, customRgba.B);

        OriginalColor = new CustomRGBA(customRgba.R, customRgba.G, customRgba.B, customRgba.A);
        GrayColor = new CustomRGBA(grayValue, grayValue, grayValue, customRgba.A);
    }

    public CustomPixel(float r, float g, float b, float a)
    {
        float grayValue = GetGrayValue(r, g, b);

        OriginalColor = new CustomRGBA(r, g, b, a);
        GrayColor = new CustomRGBA(grayValue, grayValue, grayValue, a);
    }

    private float GetGrayValue(float r, float g, float b)
    {
        return (r + g + b) / 3;
    }
}