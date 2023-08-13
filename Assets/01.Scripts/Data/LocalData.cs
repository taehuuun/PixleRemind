using System.Collections.Generic;

public class LocalData
{
    public Dictionary<string, List<TopicData>> LocalTopicData { get; set; }
    public Dictionary<string, List<CollectedPixelArtData>> LocalCollectedPixelArtData { get; set; }

    private string _selectTopicDataID;
    private string _selectPixelArtDataID;

    public LocalData()
    {
        LocalTopicData = new Dictionary<string, List<TopicData>>();
        LocalCollectedPixelArtData = new Dictionary<string, List<CollectedPixelArtData>>();
        _selectTopicDataID = string.Empty;
        _selectPixelArtDataID = string.Empty;
    }
    
    public string GetSelectTopicDataID()
    {
        return _selectTopicDataID;
    }

    public string GetSelectPixelArtDataID()
    {
        return _selectPixelArtDataID;
    }
    
    public void SetSelectTopicDataID(string id)
    {
        if (string.IsNullOrEmpty(id))
            return;
        
        _selectTopicDataID = id;
    }
    
    public void SetSelectPixelArtDataID(string id)
    {
        if (string.IsNullOrEmpty(id))
            return;
        
        _selectPixelArtDataID = id;
    }
}