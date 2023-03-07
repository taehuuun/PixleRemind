using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

namespace LTH.ColorMatch.Managers
{
    public class DataManager : MonoBehaviour
    {
        private static readonly string BasePath = Application.persistentDataPath;
        
        /// <summary>
        /// 주어진 Topic(디렉토리)에서 모든 파일 이름을 가져오는 함수
        /// </summary>
        /// <param name="topic">Topic(디렉토리)</param>
        /// <returns>파일 이름이 담긴 리스트</returns>
        public static List<string> GetPixelArts(string topic)
        {
            return GetPixelArtsNames(topic);
        }
        /// <summary>
        /// BasePath의 모들 Topic을 가져오는 함수
        /// </summary>
        /// <returns>Topic이 담긴 리스트</returns>
        public static List<string> GetTopics()
        {
            return GetTopicNames();
        }
        /// <summary>
        /// JSON 데이터를 파일로 저장하는 함수
        /// </summary>
        /// <param name="jsonData">저장할 JSON 데이터</param>
        /// <param name="fileName">저장할 파일 이름</param>
        public static void SaveJsonData(string topic,string pixelArt,string jsonData)
        {
            SaveJsonDataToFile(topic, pixelArt, jsonData);
        }
        /// <summary>
        /// JSON 데이터를 로드하는 함수
        /// </summary>
        /// <param name="fileName">로드할 파일 이름</param>
        /// <typeparam name="T">로드할 데이터 타입</typeparam>
        /// <returns name="T">T타입 오브젝트 리턴</returns>
        public static T LoadJsonData<T>(string fileName)
        {
            return LoadJsonDataFromFile<T>(fileName);
        }

        /// <summary>
        /// 주어진 Topic(디렉토리)에서 모든 파일 이름을 가져오는 함수
        /// </summary>
        /// <param name="topic">Topic(디렉토리)</param>
        /// <returns>파일 이름이 담긴 리스트</returns>
        private static List<string> GetPixelArtsNames(string topic)
        {
            List<string> fileNames = new List<string>();

            DirectoryInfo directoryInfo = new DirectoryInfo(Path.Combine(BasePath, topic));
            
            foreach (var file in directoryInfo.GetFiles())
            {
                fileNames.Add(Path.GetFileNameWithoutExtension(file.Name));
            }

            return fileNames;
        }
        /// <summary>
        /// BasePath의 모든 Topic 이름을 가져오는 함수
        /// </summary>
        /// <returns>Topic 이름이 담긴 리스트</returns>
        private static List<string> GetTopicNames()
        {
            List<string> directoryNames = new List<string>();

            DirectoryInfo directoryInfo = new DirectoryInfo(BasePath);
            
            foreach (var dir in directoryInfo.GetDirectories())
            {
                directoryNames.Add(dir.Name);
            }

            return directoryNames;
        }

        /// <summary>
        /// JSON 데이터를 파일로 저장하는 함수
        /// </summary>
        /// <param name="topic">저장할 Topic</param>
        /// <param name="pixelArt">저장할 PixelArt</param>
        /// <param name="jsonData">저장할 JSON 데이터</param>
        private static void SaveJsonDataToFile(string topic,string pixelArt, string jsonData)
        {
            string path = Path.Combine(BasePath, topic, $"{pixelArt}.json");
            Debug.Log(path);
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(jsonData);
            string code = System.Convert.ToBase64String(bytes);

            File.WriteAllText(path, code);
        }
        /// <summary>
        /// JSON 데이터를 로드하는 함수
        /// </summary>
        /// <typeparam name="T">로드할 데이터 타입</typeparam>
        /// <param name="fileName">로드할 파일 이름</param>
        /// <returns>T 타입 오브젝트 리턴</returns>
        private static T LoadJsonDataFromFile<T>(string fileName)
        {
            string path = Path.Combine(BasePath, $"{fileName}.json");

            T loadData = default(T);

            if (FileExists(fileName))
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    string code = reader.ReadToEnd();
                    byte[] bytes = System.Convert.FromBase64String(code);
                    string loadJson = System.Text.Encoding.UTF8.GetString(bytes);
                    loadData = JsonConvert.DeserializeObject<T>(loadJson);
                }
            }
            else
            {
                Debug.LogError($"해당 json 파일이 존재하지 않음: {path}");
            }

            return loadData;
        }
        /// <summary>
        /// 파일이 존재하는지 확인합니다.
        /// </summary>
        /// <param name="fileName">파일 이름</param>
        /// <returns>파일의 존재 여부</returns>
        private static bool FileExists(string fileName)
        {
            string path = Path.Combine(BasePath,$"{fileName}.json");
            return File.Exists(path);
        }
    }
}
