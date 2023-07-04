using System;
using Firebase.Firestore;

[FirestoreData, Serializable]
public class PixelArtData
{
    [FirestoreProperty] public string Title { get; set; }
    [FirestoreProperty] public string ThumbnailData { get; set; }
    [FirestoreProperty] public string Description { get; set; }
    [FirestoreProperty] public int PlayTime { get; set; }
    [FirestoreProperty] public int Size { get; set; }
    [FirestoreProperty] public bool IsCompleted { get; set; }
    [FirestoreProperty] public Difficulty Difficulty { get; set; }
    [FirestoreProperty] public PixelColorData PixelColorData { get; set; }

    public PixelArtData()
    {
        Title = "";
        ThumbnailData = "";
        PlayTime = 0;
        Size = 0;
        IsCompleted = false;
        Difficulty = Difficulty.Easy;

        PixelColorData = new PixelColorData();
    }

    public PixelArtData(string title, string thumbnailData, int playTime, int size, bool isComplete,
        Difficulty difficulty, PixelColorData pixelColorData)
    {
        Title = title;
        ThumbnailData = thumbnailData;
        PlayTime = playTime;
        Size = size;
        IsCompleted = isComplete;
        Difficulty = difficulty;
        PixelColorData = pixelColorData;
    }
}