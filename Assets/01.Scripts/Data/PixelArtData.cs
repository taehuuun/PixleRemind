using System;
using System.Collections.Generic;
using Firebase.Firestore;
using LTH.PixelRemind.Enums;

namespace LTH.PixelRemind.Data
{
    [FirestoreData, Serializable]
    public class PixelArtData
    {
        [FirestoreProperty] public string ID {get;set;}
        [FirestoreProperty] public string ThumbnailData{get;set;}
        [FirestoreProperty] public int PlayTime { get; set; }
        [FirestoreProperty] public int Size{get;set;}
        [FirestoreProperty] public bool IsCompleted{get;set;}
        [FirestoreProperty] public Difficulty Difficulty{get;set;}
        [FirestoreProperty] public PixelColorData PixelColorData{get;set;}
        
        public PixelArtData() { }
        public PixelArtData(string id, string thumbnailData, int playTime,int size, bool isComplete, Difficulty difficulty, PixelColorData pixelColorData)
        {
            ID = id;
            ThumbnailData = thumbnailData;
            PlayTime = playTime;
            Size = size;
            IsCompleted = isComplete;
            Difficulty = difficulty;
            PixelColorData = pixelColorData;
        }
    }
}
