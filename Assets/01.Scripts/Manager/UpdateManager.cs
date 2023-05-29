using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LTH.PixelRemind.Data;
using LTH.PixelRemind.Managers.Data;
using LTH.PixelRemind.Managers.Data.Paths;
using LTH.PixelRemind.Managers.Firebase;
using LTH.PixelRemind.Managers.Firebase.Collections;
using Newtonsoft.Json;
using UnityEngine;

namespace LTH.PixelRemind.Managers
{
    public class UpdateManager : MonoBehaviour
    {
        private List<TopicData> _topicDataList;

        private async void Start()
        {
            _topicDataList =
                await FirebaseManager.ins.Firestore.GetAllData<TopicData>(FirestoreCollections.GalleryData);
            
            await CheckForUpdated();
        }

        private async Task CheckForUpdated()
        {
            List<string> localTopicDataIDs = DataManager.Instance.userData.LocalTopicDataIDs;
            
            if (localTopicDataIDs == null || localTopicDataIDs.Count == 0)
            {
                List<string> tmpList = new List<string>();
                foreach (var topicData in _topicDataList)
                {
                    tmpList.Add(topicData.Title);
                }
                Debug.Log($"Missing Data : {string.Join(", ", tmpList)}");
            }
            else
            {
                List<string> missingDataIDs = new List<string>();
                
                missingDataIDs = _topicDataList.Select(data => data.ID).Except(localTopicDataIDs).ToList();
                
                List<string> outdatedDataIDs = new List<string>();

                foreach (var id in localTopicDataIDs)
                {
                    TopicData localData = DataManager.LoadJsonData<TopicData>(DataPath.GalleryDataPath, id);
                    TopicData serverData = _topicDataList.Find(data => data.ID == id);

                    if (serverData.LastUpdated > localData.LastUpdated)
                    {
                        outdatedDataIDs.Add(id);
                    }
                }
                
                Debug.Log($"Missing Data : {string.Join(", ",missingDataIDs)}");
                Debug.Log($"Outdated Data : {string.Join(", ",outdatedDataIDs)}");
            }
        }

        private async Task DownloadTopicData(string topicID)
        {
            TopicData serverData = _topicDataList.Find(data => data.ID == topicID);
            
            string jsonData = JsonConvert.SerializeObject(serverData);
            DataManager.SaveJsonData(DataPath.GalleryDataPath,topicID,jsonData);
            
            DataManager.Instance.userData.LocalTopicDataIDs.Add(topicID);
            DataManager.Instance.userData.LastUpdated = DateTime.Now;
            
#if UNITY_ANDROID && !UNITY_EDITOR
             string FUID = FirebaseManager.ins.FireAuth.FUID;
#else
            string FUID = "Test";
#endif
            await FirebaseManager.ins.Firestore.UpdateData<UserData>(FirestoreCollections.UserData, FUID, DataManager.Instance.userData);
        }
    }
}