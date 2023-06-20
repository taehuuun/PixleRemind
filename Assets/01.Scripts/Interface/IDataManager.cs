using System.Collections.Generic;

public interface IDataManager
{
    List<string> GetPixelArts(string topic);
    List<string> GetTopics();
    void SavePixelArtData(TopicData data);
    T LoadJsonData<T>(string fileName);
    bool LocalDataExists();
}