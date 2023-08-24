using System.Collections.Generic;
using System.Linq;

public class LocalData
{
    public Dictionary<string, TopicData> LocalTopicData { private get;  set; }
    public Dictionary<string, List<CollectedPixelArtData>> LocalCollectedPixelArtData { private get; set; }

    public string SelectTopicDataID { get; private set; }
    public string SelectPixelArtDataID { get; private set; }

    public LocalData()
    {
        LocalTopicData = new Dictionary<string, TopicData>();
        LocalCollectedPixelArtData = new Dictionary<string, List<CollectedPixelArtData>>();
        SelectTopicDataID = string.Empty;
        SelectPixelArtDataID = string.Empty;
    }

    public void SetPixelArtDataID(string id)
    {
        SelectPixelArtDataID = id;
    }
    public void SetTopicDataID(string id)
    {
        SelectTopicDataID = id;
    }
    public void AddLocalTopicData(string key, TopicData data)
    {
        LocalTopicData.Add(key,data);
    }
    public void AddCollectedPixelArtList(string key, List<CollectedPixelArtData> collectedPixelArtDataList)
    {
        LocalCollectedPixelArtData.Add(key, collectedPixelArtDataList);
    }
    public void AddCollectedPixelArt(string key, CollectedPixelArtData data)
    {
        LocalCollectedPixelArtData[key].Add(data);
    }
    public void RemoveTopicData(string key)
    {
        LocalTopicData.Remove(key);
    }
    public bool ContainTopicDataKey(string key)
    {
        return LocalTopicData.ContainsKey(key);
    }
    public List<CollectedPixelArtData> GetCollectedPixelArtList(string key)
    {
        return LocalCollectedPixelArtData[key];
    }
    public TopicData GetTopicData(string key)
    {
        return LocalTopicData[key];
    }
    public List<string> GetTopicDataKeys()
    {
        return LocalTopicData.Keys.ToList();
    }
}