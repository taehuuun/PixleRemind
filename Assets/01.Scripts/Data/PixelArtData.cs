using System;
using System.Collections.Generic;
using Firebase.Firestore;
using LTH.ColorMatch.Enums;

namespace LTH.ColorMatch.Data
{
    [FirestoreData]
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
        
        [FirestoreProperty]
        public Dictionary<string, object> ToDictionary
        {
            get
            {
                var dictionary = new Dictionary<string, object>
                {
                    { "topic", topic },
                    { "title", title },
                    { "thumbData", thumbData },
                    { "size", size },
                    { "fillCount", fillCount },
                    { "complete", complete },
                    { "difficulty", difficulty },
                    { "colorData", colorData.ToDictionary }
                };

                return dictionary;
            }
        }

    }
}
