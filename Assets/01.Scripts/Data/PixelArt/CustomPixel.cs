using System;
using System.Collections.Generic;
using Firebase.Firestore;

[FirestoreData, Serializable]
public class CustomPixel
{
    [FirestoreProperty] public CustomColor OriginalColor { get; private set; }
    [FirestoreProperty] public CustomColor GrayColor { get; private set; }
    [FirestoreProperty] public List<PixelCoord> PixelCoords { private get; set; }
    
    public CustomPixel(CustomColor customColor)
    {
        OriginalColor = customColor;
        GrayColor = GetGrayValue(customColor);
        PixelCoords = new List<PixelCoord>();
    }

    private CustomColor GetGrayValue(CustomColor origin)
    {
        float grayValue = (origin.R + origin.G + origin.B) / 3;
        return new CustomColor(grayValue, grayValue,grayValue,origin.A);
    }
}