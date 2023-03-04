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
        public int chanceCnt;
        public Difficulty difficulty;
        public ColorData colorData;

        public PixelArtData(GalleryTopic gt, string t, ColorData c, int s, Difficulty d)
        {
            topic = gt;
            title = t;
            colorData = c;
            size = s;
            difficulty = d;
        }
    }
}
