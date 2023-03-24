using System;
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
    public class CustomPixel
    {
        [FirestoreProperty] public ColorValue OriginalColor { get; set; }
        [FirestoreProperty] public ColorValue GrayColor { get; set; }
        [FirestoreProperty] public bool IsFilled { get; set; }
        [FirestoreProperty] public int X { get; set;  }
        [FirestoreProperty] public int Y { get; set; }

        public CustomPixel()
        {
            OriginalColor = new ColorValue(0, 0, 0, 0);
            GrayColor = new ColorValue(0, 0, 0, 0);
            IsFilled = false;
        }
        public CustomPixel(ColorValue colorValue)
        {
            float grayValue = GetGrayValue(colorValue.R, colorValue.G, colorValue.B, colorValue.A);
            
            OriginalColor = new ColorValue(colorValue.R, colorValue.G, colorValue.B, colorValue.A);
            GrayColor = new ColorValue(grayValue, grayValue, grayValue, colorValue.A);
            IsFilled = false;
        }

        public CustomPixel(float r, float g, float b, float a, int x, int y)
        {
            float grayValue = GetGrayValue(r,g,b,a);
            
            OriginalColor = new ColorValue(r, g, b, a);
            GrayColor = new ColorValue(grayValue, grayValue, grayValue, a);
            IsFilled = false;
            X = x;
            Y = y;
        }
        private float GetGrayValue(float r,float g,float b,float a)
        {
            return (r + g + b) / 3;
        }
    }
}
