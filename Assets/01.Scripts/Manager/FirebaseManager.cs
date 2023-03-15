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
        public List<TopicData> testTopic;
        FirebaseFirestore db;

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
            db = FirebaseFirestore.DefaultInstance;
        }

        public async Task<bool> CheckDocumentExists(string collection, string document)
        {
            var docRef = db.Collection(collection).Document(document);
            var docSnapShot = await docRef.GetSnapshotAsync();

            return docSnapShot.Exists;
        }

        public async Task<bool> CheckCollectionExists(string collection)
        {
            var colRef = db.Collection(collection);
            var colSnapShot = await colRef.GetSnapshotAsync();

            return colSnapShot.Count > 0;
        }

        public async Task<TopicData> GetTopicData(string collection, string document)
        {
            var docRef = db.Collection(collection).Document(document);
            var docSnapShot = await docRef.GetSnapshotAsync();

            if (!docSnapShot.Exists)
            {
                Debug.LogError("Document does not Exists");
                return null;
            }

            var topicData = docSnapShot.ConvertTo<TopicData>();
            return topicData;
        }

        public async Task<PixelArtData> GetPixelArtData(string collection, string document)
        {
            var docRef = db.Collection(collection).Document(document);
            var docSnapShot = await docRef.GetSnapshotAsync();

            if (!docSnapShot.Exists)
            {
                Debug.LogError("Document does not exist!");
                return null;
            }

            var pixelArtData = docSnapShot.ConvertTo<PixelArtData>();
            return pixelArtData;
        }
        
        public async Task<List<TopicData>> GetAllTopicData()
        {
            var snapshot = await db.Collection("GalleryData").GetSnapshotAsync();
            var topicDataList = new List<TopicData>();

            foreach (var document in snapshot.Documents)
            {
                Debug.Log(document.Id);
                var topicData = document.ConvertTo<TopicData>();
                topicDataList.Add(topicData);
            }

            return topicDataList;
        }
        public async Task AddTopicData(string collectionName, string documentName, TopicData topicData)
        {
            var documentReference = db.Collection(collectionName).Document(documentName);
            
            await documentReference.SetAsync(topicData);
            
            Debug.Log($"{topicData.ToDictionary().Count}");
        }

        public async Task UpdateTopicData(string collectionName, string documentName, TopicData topicData)
        {
            var documentReference = db.Collection(collectionName).Document(documentName);
            
            await documentReference.SetAsync(topicData, SetOptions.MergeAll);
        }

        // public async Task AddPixelArtData(string collectionName, string documentName, PixelArtData pixelArtData)
        // {
        //     var documentReference = db.Collection(collectionName).Document(documentName);
        //     await documentReference.SetAsync(pixelArtData);
        // }
        //
        // public async Task UpdatePixelArtData(string collectionName, string documentName, PixelArtData pixelArtData)
        // {
        //     var documentReference = db.Collection(collectionName).Document(documentName);
        //     await documentReference.UpdateAsync(pixelArtData.ToDictionary());
        // }
    }
}
