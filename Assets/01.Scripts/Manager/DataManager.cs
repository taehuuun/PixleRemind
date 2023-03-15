using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

namespace LTH.ColorMatch.Managers
{
    public class DataManager : MonoBehaviour
    {
        public static readonly string BasePath = Application.persistentDataPath;
        public static readonly string GalleryDataPath = Path.Combine(DataManager.BasePath, "Gallery","Topics");
        
        public static List<string> GetTargetFolderFileNames(string path)
        {
            List<string> targetDirectoryFileNames = new List<string>();
            DirectoryInfo directoryInfo = new DirectoryInfo(path);

            foreach (var fileFullName in directoryInfo.GetFiles())
            {
                var fileName = Path.GetFileNameWithoutExtension(fileFullName.Name);
                targetDirectoryFileNames.Add(fileName);
            }

            return targetDirectoryFileNames;
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
        public static bool LocalDirectoryExists(string path)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            return directoryInfo.Exists;
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
