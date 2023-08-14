using System.Linq;
using System.Threading.Tasks;

public static class DataSyncManager
{
    public static async Task SyncUserDataAndLocalData(string fuid)
    {
        DataManager.UserData =
            await FirebaseManager.ins.Firestore.GetData<UserData>(FirestoreCollections.UserData, fuid);
        DataManager.LocalData = DataManager.LoadJsonData<LocalData>(DataPath.LocalData, "LocalData");

        if (DataManager.UserData != null && DataManager.LocalData != null)
        {
            await SyncTopicDataKeys();
            SyncCollectedPixelArtData();
        }
        else if (DataManager.UserData != null && DataManager.LocalData == null)
        {
            await SyncLocalDataWithUserData();
        }
        else if (DataManager.UserData == null && DataManager.LocalData != null)
        {
            ResetAllData();
        }
        else
        {
            await CreateAndSaveNewData(fuid);
        }
    }

    private static async Task SyncTopicDataKeys()
    {
        var userKeys = DataManager.UserData.DownloadTopicData.Keys.ToList();
        var localKeys = DataManager.LocalData.LocalTopicData.Keys.ToList();

        // UserData에는 존재하지만 LocalData에 존재하지 않는 경우
        foreach (var key in userKeys.Except(localKeys))
        {
            // 해당 Key의 TopicData 다운로드
            // 다운로드 방식은 프로젝트의 구조에 따라 달라질 수 있으므로 적절히 구현해야 함
            // 예: DataManager.LocalData.LocalTopicData[key] = DownloadTopicData(key);
            TopicData downloadTopicData =
                await FirebaseManager.ins.Firestore.GetData<TopicData>(FirestoreCollections.GalleryData, key);
            DataManager.LocalData.LocalTopicData.Add(key,downloadTopicData);
        }

        // LocalData에는 존재하지만 UserData에 존재하지 않는 경우
        foreach (var key in localKeys.Except(userKeys))
        {
            // LocalData에서 Key 제거
            DataManager.LocalData.LocalTopicData.Remove(key);
        }
    }

    private static void SyncCollectedPixelArtData()
    {
        foreach (var topicData in DataManager.UserData.DownloadTopicData)
        {
            var key = topicData.Key;
            var userCollectedDataList = topicData.Value.CollectedPixelArtDataList;
            var localCollectedDataList = DataManager.LocalData.LocalCollectedPixelArtData[key];

            // UserData에는 존재하지만 LocalData에 존재하지 않는 경우
            foreach (var data  in userCollectedDataList.Except(localCollectedDataList))
            {
                localCollectedDataList.Add(data);
            }

            // LocalData에는 존재하지만 UserData에 존재하지 않는 경우
            foreach (var data in localCollectedDataList.Except(userCollectedDataList))
            {
                localCollectedDataList.Remove(data);
            }
        }
    }

    private static async Task SyncLocalDataWithUserData()
    {
        // TopicData 다운로드 필요
        foreach (var key in DataManager.UserData.DownloadTopicData.Keys)
        {
            TopicData downloadTopicData =
                await FirebaseManager.ins.Firestore.GetData<TopicData>(FirestoreCollections.GalleryData, key);
            DataManager.LocalData.LocalTopicData.Add(key,downloadTopicData);
        }
        
        // CollectedData 동기화
        var userDataDownloadTopicDataKeys = DataManager.UserData.DownloadTopicData.Keys;
        foreach (var key in userDataDownloadTopicDataKeys)
        {
            DataManager.LocalData.LocalCollectedPixelArtData.Add(key,DataManager.UserData.DownloadTopicData[key].CollectedPixelArtDataList);
        }
    }

    private static void ResetAllData()
    {
        DataManager.LocalData = new LocalData();
        DataManager.SaveJsonData(DataPath.LocalData, "LocalData",DataManager.LocalData);
    }

    private static async Task CreateAndSaveNewData(string fuid)
    {
        DataManager.UserData = new UserData();
        DataManager.LocalData = new LocalData();

        await FirebaseManager.ins.Firestore.AddData(FirestoreCollections.UserData,fuid, DataManager.UserData);
        DataManager.SaveJsonData(DataPath.LocalData, "LocalData",DataManager.LocalData);
    }
}