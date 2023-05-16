using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Firestore;
using LTH.ColorMatch.Data;
using UnityEngine;

namespace LTH.ColorMatch.Handlers
{
    public class FirestoreHandler
    {
        private FirebaseFirestore _firestore;

        public FirestoreHandler()
        {
            _firestore = FirebaseFirestore.DefaultInstance;
        }
        
        public async Task<bool> CheckDocumentExists(string collection, string document)
        {
            try
            {
                var docRef = _firestore.Collection(collection).Document(document);
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
                var colRef = _firestore.Collection(collection);
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
                var docRef = _firestore.Collection(collection).Document(document);
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
                var snapshot = await _firestore.Collection("GalleryData").GetSnapshotAsync();
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
                var documentReference = _firestore.Collection(collectionName).Document(documentName);
                
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
                var documentReference = _firestore.Collection(collectionName).Document(documentName);
                
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