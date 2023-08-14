using System.IO;
using UnityEngine;

public static class DataPath
{
    // 로컬 저장 베이스 경로
    private static readonly string BasePath = Application.persistentDataPath;
    
    // 로컬 토픽 데이터 저장 경로
    public static readonly string LocalTopicData = Path.Combine(BasePath, "Topics");
    public static readonly string LocalData = Path.Combine(BasePath,"LocalData");
}