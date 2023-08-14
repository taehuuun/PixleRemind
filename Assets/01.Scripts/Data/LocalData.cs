using System.Collections.Generic;

public class LocalData
{
    public Dictionary<string, TopicData> LocalTopicData { get; set; }
    public Dictionary<string, List<CollectedPixelArtData>> LocalCollectedPixelArtData { get; set; }

    public string SelectTopicDataID { get; set; }
    public string SelectPixelArtDataID { get; set; }

    public LocalData()
    {
        LocalTopicData = new Dictionary<string, TopicData>();
        LocalCollectedPixelArtData = new Dictionary<string, List<CollectedPixelArtData>>();
        SelectTopicDataID = string.Empty;
        SelectPixelArtDataID = string.Empty;
    }
}