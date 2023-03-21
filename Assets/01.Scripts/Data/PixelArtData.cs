using System;
using System.Collections.Generic;
using Firebase.Firestore;
using LTH.ColorMatch.Enums;

namespace LTH.ColorMatch.Data
{
    [FirestoreData, Serializable]
    public class PixelArtData
    {
        [FirestoreProperty] public GalleryTopic Topic{get;set;}
        [FirestoreProperty] public string Title{get;set;}
        [FirestoreProperty] public string ThumbData{get;set;}
        [FirestoreProperty] public int Size{get;set;}
        [FirestoreProperty] public int FillCount{get;set;}
        [FirestoreProperty] public bool Complete{get;set;}
        [FirestoreProperty] public Difficulty Difficulty{get;set;}
        [FirestoreProperty] public ColorData ColorData{get;set;}

        public PixelArtData()
        {
        }

        public PixelArtData(GalleryTopic gt, string t, string td, int s, int cnt, bool cp, Difficulty d, ColorData c)
        {
            Topic = gt;
            Title = t;
            ThumbData = td;
            Size = s;
            FillCount = cnt;
            Complete = cp;
            Difficulty = d;
            ColorData = c;
        }

        public Dictionary<string, object> ToDictionary()
        {
            var dictionary = new Dictionary<string, object>
            {
                { "Topic", Topic },
                { "Title", Title },
                { "ThumbData", ThumbData },
                { "Size", Size },
                { "FillCount", FillCount },
                { "Complete", Complete },
                { "Difficulty", Difficulty },
                { "ColorData", ColorData.ToDictionary() }
            };

            return dictionary;
        }
    }
}
