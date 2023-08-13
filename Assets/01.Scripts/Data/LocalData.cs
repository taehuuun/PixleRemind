using System;
using System.Collections.Generic;

public class LocalData
{
    public Dictionary<string, List<TopicData>> LocalTopicData { get; set; }
    public Dictionary<string, List<CollectedPixelArtData>> LocalCollectedPixelArtData { get; set; }
    private string _lastSelectID;
    
    private string[] SplitLastSelectID()
    {
        if (string.IsNullOrEmpty(_lastSelectID) || _lastSelectID.Length <= 1)
            return Array.Empty<string>();

        return _lastSelectID.Split('/');
    }

    public string GetLastSelectTopicID()
    {
        var splitSelectID = SplitLastSelectID();
        return splitSelectID.Length > 0 ? splitSelectID[0] : string.Empty;
    }

    public string GetLastSelectPixelArtID()
    {
        var splitSelectID = SplitLastSelectID();
        return splitSelectID.Length > 1 ? splitSelectID[1] : string.Empty;
    }
}
