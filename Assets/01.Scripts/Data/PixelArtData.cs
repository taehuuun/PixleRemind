using System;
using System.Collections.Generic;
using Firebase.Firestore;
using LTH.PixelRemind.Enums;

namespace LTH.PixelRemind.Data
{
    [FirestoreData, Serializable]
    public class PixelArtData
    {
        [FirestoreProperty] public string TitleID {get;set;}
        [FirestoreProperty] public string ThumbnailData{get;set;}
        [FirestoreProperty] public int PlayTime { get; set; }
        [FirestoreProperty] public int Size{get;set;}
        [FirestoreProperty] public bool IsCompleted{get;set;}
        [FirestoreProperty] public Difficulty Difficulty{get;set;}
        [FirestoreProperty] public PixelColorData PixelColorData{get;set;}

        public PixelArtData()
        {
            TitleID = "";
            ThumbnailData = "";
            PlayTime = 0;
            Size = 0;
            IsCompleted = false;
            Difficulty = Difficulty.Easy;

            PixelColorData = new PixelColorData()
            {
                RemainingPixels = 0,
                CustomPixels = new List<CustomPixel>()
            };
        }
        public PixelArtData(string titleID, string thumbnailData, int playTime,int size, bool isComplete, Difficulty difficulty, PixelColorData pixelColorData)
        {
            TitleID = titleID;
            ThumbnailData = thumbnailData;
            PlayTime = playTime;
            Size = size;
            IsCompleted = isComplete;
            Difficulty = difficulty;
            PixelColorData = pixelColorData;
        }
    }
}
