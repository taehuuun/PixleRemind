using System;
using System.Collections.Generic;
using System.Linq;
using Firebase.Firestore;

[FirestoreData, Serializable]
public class UserData
{
    [FirestoreProperty] public Dictionary<string,DownloadTopicData> DownloadTopicData { private get; set; }
    [FirestoreProperty] public DateTime LastUpdated { get; private set; }

    public UserData()
    {
        DownloadTopicData = new Dictionary<string, DownloadTopicData>();
        LastUpdated = new DateTime();
    }

    public void SetLastUpdateDate(DateTime dateTime)
    {
        LastUpdated = dateTime;
    }
    public void SetDownloadTopicData(string key, DownloadTopicData data)
    {
        DownloadTopicData[key] = data;
    }
    public void AddCollectedPixelArt(string key, CollectedPixelArtData data)
    {
        if(DownloadTopicData.TryGetValue(key, out var list))
        {
            list.CollectedPixelArtDataList.Add(data);
        }
    }
    public IReadOnlyList<DownloadTopicData> GetDownloadTopicDataList()
    {
        return DownloadTopicData.Values.ToList().AsReadOnly();
    }
    public IReadOnlyList<string> GetDownloadTopicDataKeys()
    {
        return DownloadTopicData.Keys.ToList().AsReadOnly();
    }
    public List<CollectedPixelArtData> GetCollectedPixelArtDataList(string key)
    {
        return DownloadTopicData[key].CollectedPixelArtDataList;
    }
}