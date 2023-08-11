using System;
using System.Collections.Generic;
using Firebase.Firestore;

/// <summary>
/// Firestore 업로드를 위한 커스텀 Color 클래스
/// </summary>
[FirestoreData, Serializable]
public class CustomRGBA
{
    [FirestoreProperty] public float R { get; set; }
    [FirestoreProperty] public float G { get; set; }
    [FirestoreProperty] public float B { get; set; }
    [FirestoreProperty] public float A { get; set; }

    public CustomRGBA()
    {
        R = 0;
        G = 0;
        B = 0;
        A = 0;
    }

    public CustomRGBA(float r, float g, float b, float a)
    {
        R = r;
        G = g;
        B = b;
        A = a;
    }
}

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

[FirestoreData, Serializable]
public class CustomPixel
{
    // 픽셀 아트의 원본 색상 값
    [FirestoreProperty] public CustomRGBA OriginalColor { get; set; }
    
    // 픽셀 아트의 흑백 색상 값
    [FirestoreProperty] public CustomRGBA GrayColor { get; set; }
    [FirestoreProperty] public List<PixelCoord> PixelCoords { get; set; } = new List<PixelCoord>();

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