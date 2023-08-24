using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public static class DataSyncManager
{
    public static async Task SyncUserDataAndLocalData()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        string fuid = FirebaseManager.ins.FireAuth.FUID;
#else
        string fuid = "Test";
#endif
        
        Debug.Log("사용자 데이터와 로컬 데이터 동기화 시작");
        DataManager.UserData =
            await FirebaseManager.ins.Firestore.GetData<UserData>(FirestoreCollections.UserData, fuid);
        DataManager.LocalData = DataManager.LoadLocalData();

        if (DataManager.UserData != null && DataManager.LocalData != null)
        {
            Debug.Log("UserData와 LocalData 모두 존재함");
            await SyncTopicDataKeys();
            SyncCollectedPixelArtData();
        }
        else if (DataManager.UserData != null && DataManager.LocalData == null)
        {
            Debug.Log("UserData만 존재함");
            await SyncLocalDataWithUserData();
        }
        else if (DataManager.UserData == null && DataManager.LocalData != null)
        {
            Debug.Log("LocalData만 존재함");
            ResetAllData();
        }
        else
        {
            Debug.Log("UserData와 LocalData 모두 없음");
            await CreateAndSaveNewData(fuid);
        }
    }

    private static async Task SyncTopicDataKeys()
    {
        Debug.Log("TopicData 키 동기화 시작");
        var userKeys = DataManager.UserData.GetDownloadTopicDataKeys();
        var localKeys = DataManager.LocalData.GetTopicDataKeys();

        // UserData에는 존재하지만 LocalData에 존재하지 않는 경우
        foreach (var key in userKeys.Except(localKeys))
        {
            Debug.Log($"UserData에 존재하나 LocalData에 없는 키 발견: {key}");
            TopicData downloadTopicData =
                await FirebaseManager.ins.Firestore.GetData<TopicData>(FirestoreCollections.GalleryData, key);
            DataManager.LocalData.SetLocalTopicData(key,downloadTopicData);
        }

        // LocalData에는 존재하지만 UserData에 존재하지 않는 경우
        foreach (var key in localKeys.Except(userKeys))
        {
            Debug.Log($"LocalData에 존재하나 UserData에 없는 키 발견: {key}");
            DataManager.LocalData.RemoveTopicData(key);
        }
    }

    private static void SyncCollectedPixelArtData()
    {
        Debug.Log("CollectedPixelArtData 동기화 시작");

        var userDownloadTopicDataKey = DataManager.UserData.GetDownloadTopicDataKeys();
        
        Debug.Log($"{userDownloadTopicDataKey.Count}");
        
        foreach (var key in userDownloadTopicDataKey)
        {
            if (!DataManager.LocalData.ContainTopicDataKey(key))
            {
                Debug.Log($"{key}가 존재하지 않음");
                continue;
            }
            
            List<CollectedPixelArtData> userCollectedDataList = DataManager.UserData.GetCollectedPixelArtDataList(key);
            List<CollectedPixelArtData> localCollectedDataList = DataManager.LocalData.GetCollectedPixelArtList(key);
            
            // UserData에는 존재하지만 LocalData에 존재하지 않는 경우
            foreach (var data in userCollectedDataList.Except(localCollectedDataList))
            {
                Debug.Log($"UserData에 존재하나 LocalData에 없는 데이터 발견: {data}");
                localCollectedDataList.Add(data);
            }

            // LocalData에는 존재하지만 UserData에 존재하지 않는 경우
            foreach (var data in localCollectedDataList.Except(userCollectedDataList))
            {
                Debug.Log($"LocalData에 존재하나 UserData에 없는 데이터 발견: {data}");
                localCollectedDataList.Remove(data);
            }
        }
    }

    private static async Task SyncLocalDataWithUserData()
    {
        Debug.Log("LocalData를 UserData와 동기화 시작");
        DataManager.LocalData = new LocalData();

        var keys = DataManager.UserData.GetDownloadTopicDataKeys();
        
        // TopicData 다운로드 필요
        foreach (var key in keys)
        {
            Debug.Log($"TopicData 다운로드: {key}");
            TopicData downloadTopicData =
                await FirebaseManager.ins.Firestore.GetData<TopicData>(FirestoreCollections.GalleryData, key);
            DataManager.LocalData.SetLocalTopicData(key, downloadTopicData);
        }

        // CollectedData 동기화
        foreach (var key in keys)
        {
            List<CollectedPixelArtData> collectedPixelArtDataList = DataManager.UserData.GetCollectedPixelArtDataList(key);
            DataManager.LocalData.AddCollectedPixelArtList(key, collectedPixelArtDataList);
        }

        DataManager.SaveLocalData();
    }

    private static void ResetAllData()
    {
        Debug.Log("모든 데이터 리셋");
        DataManager.LocalData = new LocalData();
        DataManager.SaveLocalData();
    }

    private static async Task CreateAndSaveNewData(string fuid)
    {
        Debug.Log("새로운 UserData와 LocalData 생성 및 저장");
        DataManager.UserData = new UserData();
        DataManager.LocalData = new LocalData();

        await FirebaseManager.ins.Firestore.AddData(FirestoreCollections.UserData, fuid, DataManager.UserData);
        DataManager.SaveLocalData();
    }
}
