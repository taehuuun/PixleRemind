using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

namespace LTH.ColorMatch.Managers
{
    public class DataManager : MonoBehaviour
    {
        public static DataManager ins;
        
        private void Awake()
        {
            if (ins == null)
            {
                ins = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        public static List<string> GetFiles(string topic)
        {
            List<string> files = new List<string>();

            DirectoryInfo directoryInfoInfo = new DirectoryInfo(Application.persistentDataPath + "/" + topic + "/");
            
            Debug.Log(Application.persistentDataPath + "/" + topic + "/");
            
            foreach (var file in directoryInfoInfo.GetFiles())
            {
                files.Add(file.Name.Substring(0,file.Name.Length-5));
            }

            return files;
        }
        public static List<string> GetDirectorys()
        {
            List<string> directorys = new List<string>();

            DirectoryInfo dirInfo = new DirectoryInfo(Application.persistentDataPath);
            
            foreach (var dir in dirInfo.GetDirectories())
            {
                directorys.Add(dir.Name);
            }
            
            return directorys;
        }
        public static void SaveJsonData(string jsonData, string fileName)
        {
            string path = Application.persistentDataPath + "/" + fileName + ".json";
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(jsonData);
            string code = System.Convert.ToBase64String(bytes);
        
            File.WriteAllText(path, code);

            Debug.Log($"Save Path : {path}");
        }
        public static T LoadJsonData<T> (string fileName)
        {
            string path =Application.persistentDataPath + "/" + fileName + ".json";

            T loadObj = default (T);

            if (JsonFileExist(fileName))
            {
                StreamReader reader = new StreamReader(path);
                string code = reader.ReadToEnd();
                byte[] bytes = System.Convert.FromBase64String(code);
                string loadJson = System.Text.Encoding.UTF8.GetString(bytes);
                
                Debug.Log(loadJson);
                loadObj = JsonConvert.DeserializeObject<T>(loadJson);
            }
            else
            {
                Debug.LogError("해당 json파일이 존재 하지 않음");
            }

            return loadObj;
        }
        public static bool JsonFileExist(string fileName)
        {
            string findFile = Application.persistentDataPath + "/" + fileName + ".json";
            return File.Exists(findFile);
        }
    }
}
