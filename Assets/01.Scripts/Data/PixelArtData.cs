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
        public Difficulty difficulty;
        public ColorData colorData;
        public ColorData blackWhiteData;

        public PixelArtData(GalleryTopic gt, string t, ColorData c, ColorData b, int s, Difficulty d)
        {
            topic = gt;
            title = t;
            colorData = c;
            blackWhiteData = b;
            size = s;
            difficulty = d;
        }
    }
}
