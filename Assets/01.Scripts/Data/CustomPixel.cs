using System;
using System.Collections.Generic;
using Firebase.Firestore;

namespace LTH.ColorMatch.Data
{
    [FirestoreData,Serializable]
    public class ColorValue
    {
        [FirestoreProperty] public float R { get; set; }
        [FirestoreProperty] public float G { get; set; }
        [FirestoreProperty] public float B { get; set; }
        [FirestoreProperty] public float A { get; set; }
        public ColorValue() { }
        public ColorValue(float r, float g, float b, float a)
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
        
        public PixelCoord() {}

        public PixelCoord(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
    
    [FirestoreData, Serializable]
    public class CustomPixel
    {
        [FirestoreProperty] public ColorValue OriginalColor { get; set; }
        [FirestoreProperty] public ColorValue GrayColor { get; set; }
        [FirestoreProperty] public List<PixelCoord> PixelCoords { get; set; } = new List<PixelCoord>();

        public CustomPixel()
        {
            OriginalColor = new ColorValue(0, 0, 0, 0);
            GrayColor = new ColorValue(0, 0, 0, 0);
        }
        public CustomPixel(ColorValue colorValue)
        {
            float grayValue = GetGrayValue(colorValue.R, colorValue.G, colorValue.B);
            
            OriginalColor = new ColorValue(colorValue.R, colorValue.G, colorValue.B, colorValue.A);
            GrayColor = new ColorValue(grayValue, grayValue, grayValue, colorValue.A);
        }
        public CustomPixel(float r, float g, float b, float a)
        {
            float grayValue = GetGrayValue(r,g,b);
            
            OriginalColor = new ColorValue(r, g, b, a);
            GrayColor = new ColorValue(grayValue, grayValue, grayValue, a);
        }
        private float GetGrayValue(float r,float g,float b)
        {
            return (r + g + b) / 3;
        }
    }
}
