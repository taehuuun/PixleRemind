using System;
using Firebase.Firestore;

namespace LTH.ColorMatch.Data
{
    [FirestoreData,Serializable]
    public class ColorMatchColor
    {
        [FirestoreProperty] public float R { get; set; }
        [FirestoreProperty] public float G { get; set; }
        [FirestoreProperty] public float B { get; set; }
        [FirestoreProperty] public float A { get; set; }
        public ColorMatchColor() { }
        public ColorMatchColor(float r, float g, float b, float a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }
    }
    
    [FirestoreData, Serializable]
    public class CustomColor
    {
        [FirestoreProperty] public ColorMatchColor OriginColorMatchColor { get; set; }
        [FirestoreProperty] public ColorMatchColor GrayColorMatchColor { get; set; }
        [FirestoreProperty] public bool IsFeel { get; set; }
        [FirestoreProperty] public int X { get; set;  }
        [FirestoreProperty] public int Y { get; set; }

        public CustomColor()
        {
            OriginColorMatchColor = new ColorMatchColor(0, 0, 0, 0);
            GrayColorMatchColor = new ColorMatchColor(0, 0, 0, 0);
            IsFeel = false;
        }
        public CustomColor(ColorMatchColor colorMatchColor)
        {
            float grayValue = GetGaryValue(colorMatchColor.R, colorMatchColor.G, colorMatchColor.B, colorMatchColor.A);
            
            OriginColorMatchColor = new ColorMatchColor(colorMatchColor.R, colorMatchColor.G, colorMatchColor.B, colorMatchColor.A);
            GrayColorMatchColor = new ColorMatchColor(grayValue, grayValue, grayValue, colorMatchColor.A);
            IsFeel = false;
        }

        public CustomColor(float r, float g, float b, float a, int x, int y)
        {
            float grayValue = GetGaryValue(r,g,b,a);
            
            OriginColorMatchColor = new ColorMatchColor(r, g, b, a);
            GrayColorMatchColor = new ColorMatchColor(grayValue, grayValue, grayValue, a);
            IsFeel = false;
            X = x;
            Y = y;
        }
        private float GetGaryValue(float r,float g,float b,float a)
        {
            return (r + g + b) / 3;
        }
    }
}
