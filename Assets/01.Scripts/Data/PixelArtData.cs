using System;
using LTH.ColorMatch.Enums;

namespace LTH.ColorMatch.Data
{
    [Serializable]
    public class PixelArtData
    {
        public GalleryTopic topic;
        public string title;
        public string thumbData;
        public int size;
        public int fillCount;
        public bool complete;
        public Difficulty difficulty;
        public ColorData colorData;


        public PixelArtData(GalleryTopic gt, string t,string td, int s,int cnt, bool cp ,Difficulty d, ColorData c)
        {
            topic = gt;
            title = t;
            thumbData = td;
            size = s;
            fillCount = cnt;
            complete = cp;
            difficulty = d;
            colorData = c;
        }
    }
}
