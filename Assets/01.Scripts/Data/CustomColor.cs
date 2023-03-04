using UnityEngine;

namespace LTH.ColorMatch.Data
{
    public class CustomColor
    {
        public Color originColor;
        public Color grayColor;
        public bool complete;
        
        public float r;
        public float g;
        public float b;
        public float a;

        public CustomColor()
        {
            r = 0f;
            g = 0f;
            b = 0f;
            a = 0f;
        }
        public CustomColor(Color color)
        {
            r = color.r;
            g = color.g;
            b = color.b;
            a = color.a;
            
            float grayValue = GetGaryValue(r, g, b, a);
            
            originColor = new Color(r, g, b, a);
            grayColor = new Color(grayValue, grayValue, grayValue, grayValue);
            complete = false;
        }
        public CustomColor(float fr, float fg, float fb, float fa)
        {
            r = fr;
            g = fg;
            b = fb;
            a = fa;

            float grayValue = GetGaryValue(r,g,b,a);
            
            originColor = new Color(r, g, b, a);
            grayColor = new Color(grayValue, grayValue, grayValue, grayValue);
            complete = false;
        }
        private float GetGaryValue(float r,float g,float b,float a)
        {
            return (r + g + b + a) / 3;
        }
    }
}
