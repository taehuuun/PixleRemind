
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace LTH.ColorMatch.Data
{
    public class CustomColor
    {
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
        }
        public CustomColor(float fr, float fg, float fb, float fa)
        {
            r = fr;
            g = fg;
            b = fb;
            a = fa;
        }

        public Color ConvertColor()
        {
            return new Color(r, g, b, a);
        }
    }
}
