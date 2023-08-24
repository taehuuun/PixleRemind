using System.Collections.Generic;
using System.Linq;

public class LocalData
{
    private readonly Dictionary<string, TopicData> _localTopicData;
    private readonly Dictionary<string, List<CollectedPixelArtData>> _localCollectedPixelArtData;

    private string _selectTopicDataID;
    private string _selectPixelArtDataID;

    public LocalData()
    {
        _localTopicData = new Dictionary<string, TopicData>();
        _localCollectedPixelArtData = new Dictionary<string, List<CollectedPixelArtData>>();
        _selectTopicDataID = string.Empty;
        _selectPixelArtDataID = string.Empty;
    }

    public int GetTopicDataCount()
    {
        return _localTopicData.Count;
    }
    public string GetTopicID()
    {
        return _selectTopicDataID;
    }
    public string GetPixelArtID()
    {
        return _selectPixelArtDataID;
    }
    public void SetPixelArtDataID(string id)
    {
        _selectPixelArtDataID = id;
    }
    public void SetTopicDataID(string id)
    {
        _selectTopicDataID = id;
    }
    public void SetLocalTopicData(string key, TopicData data)
    {
        _localTopicData[key] = data;
    }
    public void AddCollectedPixelArtList(string key, List<CollectedPixelArtData> collectedPixelArtDataList)
    {
        _localCollectedPixelArtData[key] = collectedPixelArtDataList;
    }
    public void AddCollectedPixelArt(string key, CollectedPixelArtData data)
    {
        _localCollectedPixelArtData[key].Add(data);
    }
    public void RemoveTopicData(string key)
    {
        _localTopicData.Remove(key);
    }
    public bool ContainTopicDataKey(string key)
    {
        return _localTopicData.ContainsKey(key);
    }
    public List<CollectedPixelArtData> GetCollectedPixelArtList(string key)
    {
        return _localCollectedPixelArtData[key];
    }
    public TopicData GetTopicData(string key)
    {
        return _localTopicData[key];
    }
    public List<string> GetTopicDataKeys()
    {
        return _localTopicData.Keys.ToList();
    }
}