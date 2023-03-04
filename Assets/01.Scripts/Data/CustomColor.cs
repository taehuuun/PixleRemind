using UnityEngine;

namespace LTH.ColorMatch.Data
{
    public class ColorMatchColor
    {
        public float r;
        public float g;
        public float b;
        public float a;

        public ColorMatchColor(float r, float g, float b, float a)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = a;
        }
    }
    
    public class CustomColor
    {
        public ColorMatchColor originColorMatchColor;
        public ColorMatchColor grayColorMatchColor;
        public bool complete;
        
        public CustomColor()
        {
            originColorMatchColor = new ColorMatchColor(0, 0, 0, 0);
            grayColorMatchColor = new ColorMatchColor(0, 0, 0, 0);
            complete = false;
        }
        public CustomColor(ColorMatchColor colorMatchColor)
        {
            float grayValue = GetGaryValue(colorMatchColor.r, colorMatchColor.g, colorMatchColor.b, colorMatchColor.a);
            
            originColorMatchColor = new ColorMatchColor(colorMatchColor.r, colorMatchColor.g, colorMatchColor.b, colorMatchColor.a);
            grayColorMatchColor = new ColorMatchColor(grayValue, grayValue, grayValue, colorMatchColor.a);
            complete = false;
        }
        public CustomColor(float r, float g, float b, float a)
        {
            float grayValue = GetGaryValue(r,g,b,a);
            
            originColorMatchColor = new ColorMatchColor(r, g, b, a);
            grayColorMatchColor = new ColorMatchColor(grayValue, grayValue, grayValue, a);
            complete = false;
        }
        private float GetGaryValue(float r,float g,float b,float a)
        {
            return (r + g + b) / 3;
        }
    }
}
