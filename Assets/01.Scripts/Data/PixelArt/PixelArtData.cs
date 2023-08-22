using System;
using Firebase.Firestore;

[FirestoreData, Serializable]
public class PixelArtData : BaseThumbnailData
{
    [FirestoreProperty] public int PlayTime { get; private set; }
    [FirestoreProperty] public bool IsCompleted { get; private set; }
    [FirestoreProperty] public Difficulty Difficulty { get; private set; }
    [FirestoreProperty] public PixelColorData PixelColorData { get; private set; }

    public PixelArtData()
    {
        PlayTime = 0;
        IsCompleted = false;
        Difficulty = Difficulty.Easy;
        PixelColorData = new PixelColorData();
    }

    public PixelArtData(string id, string title, string description, string thumbnailData, int thumbnailSize, int playTime, bool isCompleted, Difficulty difficulty, PixelColorData pixelColorData)
        : base(id, title, description, thumbnailData, thumbnailSize)
    {
        PlayTime = playTime;
        IsCompleted = isCompleted;
        Difficulty = difficulty;
        PixelColorData = pixelColorData;
    }
}