using System;
using System.Collections.Generic;
using Firebase.Firestore;
using LTH.ColorMatch.Enums;

namespace LTH.ColorMatch.Data
{
    [FirestoreData, Serializable]
    public class TopicData
    {
        [FirestoreProperty] public GalleryTopic topic {get;set;}
        [FirestoreProperty] public string thumbData {get;set;}
        [FirestoreProperty] public int completeCount {get;set;}
        [FirestoreProperty] public int totalCount {get;set;}
        [FirestoreProperty] public int thumbSize { get; set; }
        [FirestoreProperty] public bool complete {get;set;}
        [FirestoreProperty] public List<string> pixelArtDatas { get; set; }
        
        
        [FirestoreProperty]
        public Dictionary<string, object> ToDictionary
        {
            get
            {
                var dictionary = new Dictionary<string, object>
                {
                    { "topic", topic },
                    { "thumbData", thumbData },
                    { "completeCount", completeCount },
                    { "totalCount", totalCount },
                    { "complete", complete },
                    { "pixelArtDatas", pixelArtDatas }
                };
                return dictionary;
            }
        }

        public TopicData(GalleryTopic tp, string th, int cC, int total,int size, bool cb
            , List<string> p)
        {
            topic = tp;
            thumbData = th;
            completeCount = cC;
            totalCount = total;
            thumbSize = size;
            complete = cb;
            pixelArtDatas = p;
        }
        
        public TopicData() {}
    }    
}