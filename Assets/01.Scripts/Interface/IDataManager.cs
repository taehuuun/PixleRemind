
using System.Collections;
using System.Collections.Generic;
using LTH.ColorMatch.Data;
using UnityEditor;
using UnityEngine;

namespace LTH.ColorMatch.Interfaces
{
    public interface IDataManager
    {
        List<string> GetPixelArts(string topic);
        List<string> GetTopics();
        void SavePixelArtData(TopicData data);
        T LoadJsonData<T>(string fileName);
        bool LocalDataExists();
    }
}
