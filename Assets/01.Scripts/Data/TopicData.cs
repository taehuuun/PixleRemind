using System;
using System.Collections.Generic;
using Firebase.Firestore;

namespace LTH.PixelRemind.Data
{
    [FirestoreData, Serializable]
    public class TopicData
    {
        [FirestoreProperty] public string ID { get; set; }
        [FirestoreProperty] public string ThumbData {get;set;}
        [FirestoreProperty] public int CompleteCount {get;set;}
        [FirestoreProperty] public int TotalCount {get;set;}
        [FirestoreProperty] public int ThumbSize { get; set; }
        [FirestoreProperty] public bool Complete {get;set;}
        [FirestoreProperty] public bool Updateable { get; set; }
        [FirestoreProperty] public DateTime LastUpdated { get; set; }
        [FirestoreProperty] public List<PixelArtData> PixelArtDatas { get; set; }

        public TopicData() {}
        public TopicData(string id,string thumbData, int completeCount, int totalCount,int thumbSize, bool complete, bool updateable
            , DateTime lastUpdated, List<PixelArtData> pixelArtDatas)
        {
            ID = id;
            ThumbData = thumbData;
            CompleteCount = completeCount;
            TotalCount = totalCount;
            ThumbSize = thumbSize;
            Complete = complete;
            Updateable = updateable;
            LastUpdated = lastUpdated;
            PixelArtDatas = pixelArtDatas;
        }
    }    
}