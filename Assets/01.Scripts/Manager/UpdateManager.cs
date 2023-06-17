using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

public class UpdateManager : MonoBehaviour
{
    public UpdatePopup updatePopup;

    public event Action OnDownloadCompleted;
    public event Action<bool> OnUpdateCheckCompleted;
    
    private List<TopicData> _topicDataList;

    private async void Start()
    {
        Debug.Log("UpdateManager Start");
        _topicDataList =
            await FirebaseManager.ins.Firestore.GetAllData<TopicData>(FirestoreCollections.GalleryData);

        Debug.Log(_topicDataList.Count);

        _topicDataList.Sort(new TopicDataListComparer());

        if (DataManager.Instance.userData.LocalTopicDataIDs == null)
        {
            DataManager.Instance.userData.LocalTopicDataIDs = new List<string>();
        }
        
        await CheckForUpdated();
    }

    public Task CheckForUpdated()
    {
        Debug.Log("UpdateManager CheckForUpdated");
        List<string> localTopicDataIDs = DataManager.Instance.userData.LocalTopicDataIDs;
        List<string> missingDataIDs = new List<string>();
        List<string> outdatedDataIDs = new List<string>();

        List<TopicData> missingDataList = new List<TopicData>();
        List<TopicData> outdatedDataList = new List<TopicData>();

        if (localTopicDataIDs == null || localTopicDataIDs.Count == 0)
        {
            Debug.Log("로컬 데이터가 없음");
            foreach (var topicData in _topicDataList)
            {
                missingDataIDs.Add(topicData.Title);
                missingDataList.Add(topicData);
            }

            updatePopup.Show(outdatedDataList, missingDataList);
        }
        else
        {
            missingDataIDs = _topicDataList.Select(data => data.ID).Except(localTopicDataIDs).ToList();

            foreach (var id in localTopicDataIDs)
            {
                TopicData localData = DataManager.LoadJsonData<TopicData>(DataPath.GalleryDataPath, id);
                TopicData serverData = _topicDataList.Find(data => data.ID == id);

                if (serverData.LastUpdated > localData.LastUpdated)
                {
                    outdatedDataIDs.Add(id);
                    outdatedDataList.Add(serverData);
                }
            }
        }

        Debug.Log($"Missing Data : {string.Join(", ", missingDataIDs)}");
        Debug.Log($"Outdated Data : {string.Join(", ", outdatedDataIDs)}");

        if (outdatedDataList.Count > 0)
        {
            updatePopup.Show(outdatedDataList, missingDataList);
        }
        
        OnUpdateCheckCompleted?.Invoke(missingDataList.Count >0 || outdatedDataList.Count > 0);
        
        return Task.CompletedTask;
    }

    public async Task DownloadTopicData(string topicID)
    {
        Debug.Log("UpdateManager DownloadTopicData");
        TopicData serverData = _topicDataList.Find(data => data.ID == topicID);

        if (serverData != null)
        {
            string jsonData = JsonConvert.SerializeObject(serverData);
            DataManager.SaveJsonData(DataPath.GalleryDataPath, topicID, jsonData);

            DataManager.Instance.userData.LocalTopicDataIDs.Add(topicID);
            DataManager.Instance.userData.LastUpdated = DateTime.Now;

#if UNITY_ANDROID && !UNITY_EDITOR
                 string FUID = FirebaseManager.ins.FireAuth.FUID;
#else
            string FUID = "Test";
#endif
            await FirebaseManager.ins.Firestore.UpdateData<UserData>(FirestoreCollections.UserData, FUID,
                DataManager.Instance.userData);
            
            OnDownloadCompleted?.Invoke();
        }
        else
        {
            Debug.LogError("Topic ID를 서버에서 찾지 못 했습니다.");
        }
    }
}