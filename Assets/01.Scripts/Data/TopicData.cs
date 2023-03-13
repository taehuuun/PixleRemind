using System;
using System.Collections.Generic;
using LTH.ColorMatch.Enums;

namespace LTH.ColorMatch.Data
{
    [Serializable]
    public class TopicData
    {
        public GalleryTopic topic;
        public string thumbData;
        public int completeCount;
        public int totalCount;
        public bool complete;
        public List<PixelArtData> pixelArtDatas;
    }    
}