using System;
using LTH.ColorMatch.Enums;

namespace LTH.ColorMatch.Data
{
    [Serializable]
    public class PixelArtData
    {
        public GalleryTopic topic;
        public string title;
        public int size;
        public int fillCount;
        public bool complete;
        public Difficulty difficulty;
        public ColorData colorData;


        public PixelArtData(GalleryTopic gt, string t, ColorData c, int s,int cnt, bool cp ,Difficulty d)
        {
            topic = gt;
            title = t;
            colorData = c;
            size = s;
            fillCount = cnt;
            complete = cp;
            difficulty = d;
        }
    }
}
