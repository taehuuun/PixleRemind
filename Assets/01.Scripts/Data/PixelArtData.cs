using System;
using LTH.ColorMatch.Enums;
using UnityEngine.Serialization;

namespace LTH.ColorMatch.Data
{
    [Serializable]
    public class PixelArtData
    {
        public GalleryTopic topic;
        public string title;
        public int size;
        public int fillCount;
        public int remainPixel;
        public Difficulty difficulty;
        public ColorData colorData;

        public PixelArtData(GalleryTopic gt, string t, ColorData c, int s,int cnt,int remain, Difficulty d)
        {
            topic = gt;
            title = t;
            colorData = c;
            size = s;
            remainPixel = remain;
            fillCount = cnt;
            difficulty = d;
        }
    }
}
