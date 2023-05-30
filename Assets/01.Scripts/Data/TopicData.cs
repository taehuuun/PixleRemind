using System;
using System.Collections.Generic;
using Firebase.Firestore;
using LTH.PixelRemind.Data.Conditions;

namespace LTH.PixelRemind.Data
{
    [FirestoreData, Serializable]
    public class TopicData
    {
        [FirestoreProperty] public string ID { get; set; }
        [FirestoreProperty] public string Title { get; set; }
        [FirestoreProperty] public string ThumbData {get;set;}
        [FirestoreProperty] public string Introduction { get; set; }
        [FirestoreProperty] public int CompleteCount {get;set;}
        [FirestoreProperty] public int TotalCount {get;set;}
        [FirestoreProperty] public int ThumbSize { get; set; }
        [FirestoreProperty] public bool Complete {get;set;}
        [FirestoreProperty] public bool Updateable { get; set; }
        [FirestoreProperty] public bool IsLocked { get; set; }
        [FirestoreProperty] public UnlockCondition UnlockCondition { get; set; }
        [FirestoreProperty] public DateTime LastUpdated { get; set; }
        [FirestoreProperty] public List<PixelArtData> PixelArtDatas { get; set; }

        public TopicData()
        {
            Title = "";
            ThumbData = "";
            Introduction = "";
            CompleteCount = 0;
            TotalCount = 0;
            ThumbSize = 0;
            Complete = false;
            Updateable = false;
            IsLocked = false;
            UnlockCondition = new UnlockCondition();
            LastUpdated = DateTime.Now;
            PixelArtDatas = new List<PixelArtData>();
        }
        public TopicData(
            string title,
            string thumbData,
            string introduction,
            int completeCount,
            int totalCount,
            int thumbSize,
            bool complete,
            bool updateable,
            bool isLocked,
            UnlockCondition unlockCondition,
            DateTime lastUpdated, 
            List<PixelArtData> pixelArtDatas)
        {
            Title = title;
            ThumbData = thumbData;
            Introduction = introduction;
            CompleteCount = completeCount;
            TotalCount = totalCount;
            ThumbSize = thumbSize;
            Complete = complete;
            Updateable = updateable;
            IsLocked = isLocked;
            UnlockCondition = unlockCondition;
            LastUpdated = lastUpdated;
            PixelArtDatas = pixelArtDatas;
        }
    }    
}