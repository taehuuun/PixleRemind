using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LTH.PixelRemind.Data;
using LTH.PixelRemind.Managers.Data;
using LTH.PixelRemind.Managers.Data.Paths;
using LTH.PixelRemind.Managers.Firebase;
using LTH.PixelRemind.Managers.Firebase.Collections;
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

            List<string> missingDataIDS = _topicDataList.Select(data => data.ID).Except(localTopicDataIDs).ToList();

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
            
            Debug.Log($"Missing Data : {string.Join(", ",missingDataIDS)}");
            Debug.Log($"Outdated Data : {string.Join(", ",outdatedDataIDs)}");
        }
    }
}