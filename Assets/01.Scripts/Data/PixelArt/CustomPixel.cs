using System;
using System.Collections.Generic;
using Firebase.Firestore;

[FirestoreData, Serializable]
public class CustomPixel
{
    [FirestoreProperty] public CustomColor OriginalColor { get; private set; }
    [FirestoreProperty] public CustomColor GrayColor { get; private set; }
    [FirestoreProperty] public List<PixelCoord> PixelCoords { private get; set; }

    public CustomPixel() : this(new CustomColor()) { }
    public CustomPixel(CustomColor customColor)
    {
        OriginalColor = customColor;
        GrayColor = GetGrayValue(customColor);
        PixelCoords = new List<PixelCoord>();
    }

    public void AddPixelCoord(PixelCoord coord)
    {
        PixelCoords.Add(coord);
    }
    public void RemovePixelCoord(int index)
    {
        if (!IsValidIndexRange(index))
        {
            return;
        }
        
        PixelCoords.RemoveAt(index);
    }
    public int GetPixelCoordCount()
    {
        return PixelCoords.Count;
    }
    public PixelCoord GetPixelCoord(int index)
    {
        if (!IsValidIndexRange(index))
            return null;

        return PixelCoords[index];
    }

    private bool IsValidIndexRange(int index)
    {
        return index >= 0 && index < PixelCoords.Count;
    }
    private CustomColor GetGrayValue(CustomColor origin)
    {
        float grayValue = (origin.R + origin.G + origin.B) / 3;
        return new CustomColor(grayValue, grayValue,grayValue,origin.A);
    }
}