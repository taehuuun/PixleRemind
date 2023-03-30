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
        [FirestoreProperty] public string ThumbnailData{get;set;}
        [FirestoreProperty] public string PlayTime { get; set; }
        [FirestoreProperty] public int Size{get;set;}
        [FirestoreProperty] public int RemainingFills{get;set;}
        [FirestoreProperty] public bool IsCompleted{get;set;}
        [FirestoreProperty] public Difficulty Difficulty{get;set;}
        [FirestoreProperty] public PixelColorData PixelColorData{get;set;}
        
        public PixelArtData()
        {
        }
        public PixelArtData(GalleryTopic topic, string title, string thumbnailData, string playTime,int size, int remainingFills, bool isComplete, Difficulty difficulty, PixelColorData pixelColorData)
        {
            Topic = topic;
            Title = title;
            ThumbnailData = thumbnailData;
            PlayTime = playTime;
            Size = size;
            RemainingFills = remainingFills;
            IsCompleted = isComplete;
            Difficulty = difficulty;
            PixelColorData = pixelColorData;
        }
        public Dictionary<string, object> ToDictionary()
        {
            var dictionary = new Dictionary<string, object>
            {
                { "Topic", Topic },
                { "Title", Title },
                { "ThumbnailData", ThumbnailData },
                {"PlayTime",PlayTime},
                { "Size", Size },
                { "RemainingFills", RemainingFills },
                { "IsCompleted", IsCompleted },
                { "Difficulty", Difficulty },
                { "PixelColorData", PixelColorData.ToDictionary() }
            };

            return dictionary;
        }
    }
}
