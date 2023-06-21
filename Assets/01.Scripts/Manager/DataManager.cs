using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    public UserData userData;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }
    
    /// <summary>
    /// 타겟 폴더의 모든 파일 이름을 반환하는 메서드
    /// </summary>
    /// <param name="path">타겟 폴더 경로</param>
    /// <returns>리스트 형태의 파일이름</returns>
    public static List<string> GetTargetFolderFileNames(string path)
    {
        try
        {
            Debug.Log("DataManager GetTargetFolderFileNames");
            List<string> targetDirectoryFileNames = new List<string>();
            DirectoryInfo directoryInfo = new DirectoryInfo(path);

            foreach (var fileFullName in directoryInfo.GetFiles())
            {
                var fileName = Path.GetFileNameWithoutExtension(fileFullName.Name);
                targetDirectoryFileNames.Add(fileName);
            }

            return targetDirectoryFileNames;
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to get filenames from directory {path}. Exception: {ex}");
            return new List<string>(); // Return an empty list to prevent null reference exceptions
        }
    }
    
    /// <summary>
    /// 로컬에 Josn 데이터를 저장하는 호출용 메서드
    /// </summary>
    /// <param name="savePath">저장 경로</param>
    /// <param name="fileName">저장 파일명</param>
    /// <param name="jsonData">저장할 JsonData</param>
    public static void SaveJsonData(string savePath, string fileName, string jsonData)
    {
        SaveJsonDataToFile(savePath, fileName, jsonData);
    }

    /// <summary>
    /// 로컬에서 데이터를 로드하는 호출용 메서드 
    /// </summary>
    /// <param name="loadPath">로드 할 경로</param>
    /// <param name="fileName">로드 할 파일 이름</param>
    /// <typeparam name="T">로드 할 데이터 타입</typeparam>
    /// <returns>로드한 데이터</returns>
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

        File.WriteAllText(path, jsonData);
        // var bytes = System.Text.Encoding.UTF8.GetBytes(jsonData);
        // var code = System.Convert.ToBase64String(bytes);
        // File.WriteAllText(path, code);
    }

    private static T LoadJsonDataFromFile<T>(string loadPath, string fileName)
    {
        var path = Path.Combine(loadPath, $"{fileName}.json");

        T loadData = default;

        if (FileExists(loadPath, fileName))
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

    private static bool FileExists(string checkPath, string fileName)
    {
        var path = Path.Combine(checkPath, $"{fileName}.json");
        return File.Exists(path);
    }
}