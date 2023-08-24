using System.Collections.Generic;

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
}