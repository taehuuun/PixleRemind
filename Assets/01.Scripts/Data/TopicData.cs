using System;
using System.Collections.Generic;
using Firebase.Firestore;
using LTH.ColorMatch.Enums;

namespace LTH.ColorMatch.Data
{
    [FirestoreData, Serializable]
    public class TopicData
    {
        [FirestoreProperty] public GalleryTopic Topic {get;set;}
        [FirestoreProperty] public string ThumbData {get;set;}
        [FirestoreProperty] public int CompleteCount {get;set;}
        [FirestoreProperty] public int TotalCount {get;set;}
        [FirestoreProperty] public int ThumbSize { get; set; }
        [FirestoreProperty] public bool Complete {get;set;}
        [FirestoreProperty] public List<PixelArtData> PixelArtDatas { get; set; }

        public TopicData() {}
        public TopicData(GalleryTopic topic, string thumbData, int completeCount, int totalCount,int thumbSize, bool complete
            , List<PixelArtData> pixelArtDatas)
        {
            Topic = topic;
            ThumbData = thumbData;
            CompleteCount = completeCount;
            TotalCount = totalCount;
            ThumbSize = thumbSize;
            Complete = complete;
            PixelArtDatas = pixelArtDatas;
        }
                
        public Dictionary<string, object> ToDictionary()
        {
            var dictionary = new Dictionary<string, object>
            {
                { "Topic", Topic },
                { "ThumbData", ThumbData },
                { "CompleteCount", CompleteCount },
                { "TotalCount", TotalCount },
                { "Complete", Complete },
                { "PixelArtDatas", PixelArtDatas }
            };
            return dictionary;
        }
    }    
}