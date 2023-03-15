using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Firebase.Firestore;
using LTH.ColorMatch.Data;

namespace LTH.ColorMatch.Managers
{
    public class FirebaseManager : MonoBehaviour
    {
        public static FirebaseManager ins;
        private FirebaseFirestore _db;

        private void Awake()
        {
            if (ins == null)
            {
                ins = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(this);
            }
            
            Initialization();
        }

        private void Initialization()
        {
            _db = FirebaseFirestore.DefaultInstance;
        }

        public async Task<bool> CheckDocumentExists(string collection, string document)
        {
            try
            {
                var docRef = _db.Collection(collection).Document(document);
                var docSnapShot = await docRef.GetSnapshotAsync();

                return docSnapShot.Exists;
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                throw;
            }
        }

        public async Task<bool> CheckCollectionExists(string collection)
        {
            try
            {
                var colRef = _db.Collection(collection);
                var colSnapShot = await colRef.GetSnapshotAsync();

                return colSnapShot.Count > 0;
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                throw;
            }
        }

        public async Task<TopicData> GetTopicData(string collection, string document)
        {
            try
            {
                var docRef = _db.Collection(collection).Document(document);
                var docSnapShot = await docRef.GetSnapshotAsync();

                if (!docSnapShot.Exists)
                {
                    Debug.LogError("Document does not Exists");
                    return null;
                }

                var topicData = docSnapShot.ConvertTo<TopicData>();
                return topicData;
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                throw;
            }
        }

        public async Task<List<TopicData>> GetAllTopicData()
        {
            try
            {
                var snapshot = await _db.Collection("GalleryData").GetSnapshotAsync();
                var topicDataList = new List<TopicData>();

                foreach (var document in snapshot.Documents)
                {
                    Debug.Log(document.Id);
                    var topicData = document.ConvertTo<TopicData>();
                    topicDataList.Add(topicData);
                }

                return topicDataList;
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                throw;
            }
        }
        public async Task AddTopicData(string collectionName, string documentName, TopicData topicData)
        {
            try
            {
                var documentReference = _db.Collection(collectionName).Document(documentName);
                
                await documentReference.SetAsync(topicData);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                throw;
            }
        }

        public async Task UpdateTopicData(string collectionName, string documentName, TopicData topicData)
        {
            try
            {
                var documentReference = _db.Collection(collectionName).Document(documentName);
                
                await documentReference.SetAsync(topicData, SetOptions.MergeAll);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                throw;
            }
        }
    }
}
