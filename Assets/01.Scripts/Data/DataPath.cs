using System.IO;
using UnityEngine;

public class DataPath : MonoBehaviour
{
    private static readonly string BasePath = Application.persistentDataPath;
    public static readonly string GalleryDataPath = Path.Combine(BasePath, "Gallery", "Topics");
}