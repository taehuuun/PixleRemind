using System.Collections.Generic;
using System.IO;
using Google.MiniJSON;
using UnityEngine;
using Newtonsoft.Json;
using UnityEditor.U2D.Sprites;

namespace LTH.ColorMatch.Managers
{
    public class DataManager : MonoBehaviour
    {
        public static readonly string BasePath = Application.persistentDataPath;
        public static readonly string GalleryDataPath = Path.Combine(BasePath, "Gallery");
        
        public static List<string> GetPixelArts(string topic)
        {
            return GetPixelArtNames(topic);
        }
        public static List<string> GetTopics()
        {
            return GetTopicNames();
        }
        public static void SaveJsonData(string savePath, string fileName, string jsonData)
        {
            SaveJsonDataToFile(savePath, fileName, jsonData);
        }
        public static T LoadJsonData<T>(string loadPath, string fileName)
        {
            // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
            return LoadJsonDataFromFile<T>(loadPath, fileName);
        }
        public static bool LocalDataExists()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(GalleryDataPath);
            return directoryInfo.Exists;
        }

        private static List<string> GetPixelArtNames(string topic)
        {
            var fileNames = new List<string>();

            var directoryInfo = new DirectoryInfo(Path.Combine(GalleryDataPath, topic));
            
            Debug.Log(directoryInfo.Exists);
            
            foreach (var file in directoryInfo.GetFiles())
            {
                Debug.Log(file.Name);
                fileNames.Add(Path.GetFileNameWithoutExtension(file.Name));
            }

            return fileNames;
        }
        private static List<string> GetTopicNames()
        {
            var directoryNames = new List<string>();
            
            var directoryInfo = new DirectoryInfo(Path.Combine(GalleryDataPath,"Topics"));
            
            foreach (var fileFullName in directoryInfo.GetFiles())
            {
                var file = Path.GetFileNameWithoutExtension(fileFullName.Name);
                Debug.Log(file);
                directoryNames.Add(file);
            }

            return directoryNames;
        }
        private static void SaveJsonDataToFile(string savePath, string fileName, string jsonData)
        {
            var path = Path.Combine(savePath, $"{fileName}.json");

            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }
            
            File.WriteAllText(path,jsonData);
            // var bytes = System.Text.Encoding.UTF8.GetBytes(jsonData);
            // var code = System.Convert.ToBase64String(bytes);
            // File.WriteAllText(path, code);
        }
        private static T LoadJsonDataFromFile<T>(string loadPath, string fileName)
        {
            var path = Path.Combine(loadPath, $"{fileName}.json");

            T loadData = default;

            if (FileExists(loadPath,fileName))
            {
                using (var reader = new StreamReader(path))
                {
                    // var code = reader.ReadToEnd();
                    // var bytes = System.Convert.FromBase64String(code);
                    // var loadJson = System.Text.Encoding.UTF8.GetString(bytes);
                    // loadData = JsonConvert.DeserializeObject<T>(loadJson);

                    var loadJson = reader.ReadToEnd();
                    loadData = JsonConvert.DeserializeObject<T>(loadJson);
                }
            }
            else
            {
                Debug.LogError($"해당 json 파일이 존재하지 않음: {path}");
            }

            return loadData;
        }

        private static bool FileExists(string checkPath,string fileName)
        {
            var path = Path.Combine(checkPath, $"{fileName}.json");
            return File.Exists(path);
        }
    }
}
