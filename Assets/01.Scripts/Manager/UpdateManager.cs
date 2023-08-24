using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class UpdateManager : MonoBehaviour
{
    public UpdatePopup updatePopup;

    // 다운로드가 완료 되었을 때 이벤트
    public event Action OnDownloadCompleted;
    
    // 다운로드가 실패 했을 때 이벤트
    public event Action<Exception> OnDownloadFailed;
    
    // 업데이트 체크가 완료 되었을 때 이벤트
    public event Action<bool> OnUpdateCheckCompleted;
    
    private List<TopicData> _topicDataList;

    private async void Start()
    {
        // Firestore에 있는 모든 토픽 데이터를 로드
        _topicDataList =
            await FirebaseManager.ins.Firestore.GetAllData<TopicData>(FirestoreCollections.GalleryData);

#if UNITY_ANDROID && !UNITY_EDITOR
        string FUID = FirebaseManager.ins.FireAuth.FUID;
#else
        string FUID = "Test";
#endif
        
        // 로드된 토픽 데이터 리스트를 정렬
        _topicDataList.Sort(new TopicDataListComparer());

        await CheckForUpdated();
    }

    /// <summary>
    /// 업데이트 체크 메서드
    /// </summary>
    /// <returns></returns>
    public Task CheckForUpdated()
    {
        // 로컬에 저장되어 있는 토픽 데이터 ID 리스트
        var localTopicDataIDs = DataManager.LocalData.GetTopicDataKeys();
        
        // 다운로드 하지 않은 토픽 데이터 리스트
        List<TopicData> missingDataList = new List<TopicData>();
        
        // 추가된 토픽 데이터 리스트
        List<TopicData> outdatedDataList = new List<TopicData>();

        // 토컬 ID 리스트가 비었거나, 추가된 ID가 없는 경우 
        if (localTopicDataIDs == null || localTopicDataIDs.Count == 0)
        {
            // Firestore에서 로드한 데이터를 missingDataList에 추가
            foreach (var topicData in _topicDataList)
            {
                missingDataList.Add(topicData);
            }
        }
        else
        {
            // 다운로드 하지 않은 토픽 데이터 ID 리스트
            List<string> missingDataIDs = _topicDataList.Select(data => data.ID).Except(localTopicDataIDs).ToList();
            
            // 미 다운로드 ID에 해당하는 Firestore 데이터를 missingDataList에 추가
            foreach (var id in missingDataIDs)
            {
                TopicData serverData = _topicDataList.Find(data => data.ID == id);

                if (serverData != null)
                {
                    missingDataList.Add(serverData);
                }
            }
            
            // Firestore 데이터와 비교하여 로컬에 없는 데이터를 outdatedDataList 추가
            foreach (var id in localTopicDataIDs)
            {
                TopicData localData = DataManager.LocalData.GetTopicData(id);
                TopicData serverData = _topicDataList.Find(data => data.ID == id);

                if (serverData.LastUpdated > localData.LastUpdated)
                {
                    outdatedDataList.Add(serverData);
                }
            }
        }
        
        // 각 DataList에 데이터들이 존재 할 경우 UpdatePopup을 화면에 띄움
        bool updatedPopupShow = missingDataList.Count > 0 || outdatedDataList.Count > 0;
        
        if (updatedPopupShow)
        {
            updatePopup.Show(outdatedDataList, missingDataList);
        }
        else
        {
            PopupManager.Instance.ShowPopup("업데이트 체크 완료", "모든 데이터가 추가되어 있습니다");
        }

        // 업데이트 체크 완료 이벤트 실행
        OnUpdateCheckCompleted?.Invoke(updatedPopupShow);
        
        return Task.CompletedTask;
    }

    /// <summary>
    /// ID와 일치하는 토픽 데이터를 다운로드 하는 메서드
    /// </summary>
    /// <param name="topicID">다운로드 할 Topic ID</param>
    public async Task DownloadTopicData(string topicID)
    {
        TopicData serverData = _topicDataList.Find(data => data.ID == topicID);
        
        try
        {
            if (serverData != null)
            {
                DataManager.LocalData.AddLocalTopicData(topicID, serverData);
                DataManager.LocalData.AddCollectedPixelArtList(topicID, new List<CollectedPixelArtData>());
                DataManager.SaveLocalData();

                DownloadTopicData newDownloadTopicData = new DownloadTopicData(serverData.ID, serverData.Title, serverData.Description, serverData.TotalCount);
                DataManager.UserData.DownloadTopicData.Add(topicID,newDownloadTopicData);
                DataManager.UserData.LastUpdated = DateTime.Now;

#if UNITY_ANDROID && !UNITY_EDITOR
                 string FUID = FirebaseManager.ins.FireAuth.FUID;
#else
                string FUID = "Test";
#endif
                await FirebaseManager.ins.Firestore.UpdateData<UserData>(FirestoreCollections.UserData, FUID,
                    DataManager.UserData);
            
                OnDownloadCompleted?.Invoke();
            }
            else
            {
                OnDownloadFailed?.Invoke(new Exception("Topic ID를 서버에서 찾지 못 했습니다."));
            }
        }
        catch (Exception e)
        {
            OnDownloadFailed?.Invoke(e);
        }
    }
}