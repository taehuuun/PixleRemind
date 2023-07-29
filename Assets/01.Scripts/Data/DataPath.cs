using System.IO;
using UnityEngine;

public class DataPath : MonoBehaviour
{
    // 로컬 저장 베이스 경로
    private static readonly string BasePath = Application.persistentDataPath;
    
    // 로컬 토픽 데이터 저장 경로
    public static readonly string GalleryDataPath = Path.Combine(BasePath, "Gallery", "Topics");
    public static readonly string CollectDataPath = Path.Combine(BasePath, "CollectData");
}