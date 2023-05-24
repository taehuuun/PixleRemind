using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Firestore;
using LTH.ColorMatch.Data;
using UnityEngine;

namespace LTH.ColorMatch.Managers.FirebaseHandlers
{
    /// <summary>
    /// Firebase의 Firestore의 기능을 담당하는 핸들러
    /// </summary>
    public class FirestoreHandler
    {
        // Firestore Firestore 인스턴스를 참조하는 필드
        private readonly FirebaseFirestore _firestore;
        
        /// <summary>
        /// 생성자에서는 Firestore의 기본 인스턴스를 초기화 함
        /// </summary>
        public FirestoreHandler()
        {
            _firestore = FirebaseFirestore.DefaultInstance;
        }
        
        /// <summary>
        /// 해당 컬렉션의 문서가 존재하는지 확인하는 비동기 메서드
        /// </summary>
        /// <param name="collection">컬렉션의 이름</param>
        /// <param name="document">문서의 이름</param>
        /// <returns>해당 컬렉션의 문서 존재 여부</returns>
        public async Task<bool> CheckDocumentExists(string collection, string document)
        {
            try
            {
                // Firestore에서 컬렉션과 문서를 참조함
                var docRef = _firestore.Collection(collection).Document(document);
                
                // 해당 문서의 스냅샷을 가져옴
                var docSnapShot = await docRef.GetSnapshotAsync();
                
                // 문서가 존재하는지 확인후 존재 여부를 리턴
                return docSnapShot.Exists;
            }
            catch (Exception e)
            {
                // 오류 발생 시 로그 출력 후 예외 발생
                Debug.LogError(e);
                throw;
            }
        }
        /// <summary>
        /// 컬렉션의 존재 여부를 확인하는 비동기 메서드
        /// </summary>
        /// <param name="collection">컬렉션 이름</param>
        /// <returns>컬렉션의 존재 여부</returns>
        public async Task<bool> CheckCollectionExists(string collection)
        {
            try
            {
                // Firestore에서 컬렉션을 참조
                var colRef = _firestore.Collection(collection);
                
                // 해당 컬렉션의 스냅샷을 가져옴
                var colSnapShot = await colRef.GetSnapshotAsync();
                
                // 컬렉션 내에 문서가 하나라도 있는지 확인 및 반환
                return colSnapShot.Count > 0;
            }
            catch (Exception e)
            {
                // 오류 발생 시 로그 출력후 예외 발생
                Debug.LogError(e);
                throw;
            }
        }
        /// <summary>
        /// 특정 컬렉션의 문서를 TopicData로 변환하여 반환하는 비동기 메서드
        /// </summary>
        /// <param name="collection">컬렉션 이름</param>
        /// <param name="document">문서 이름</param>
        /// <returns>변환된 TopicData를 반환</returns>
        public async Task<TopicData> GetTopicData(string collection, string document)
        {
            try
            {
                // Firestore에서 컬렉션과 문서를 참조
                var docRef = _firestore.Collection(collection).Document(document);
                
                // 해당 문서의 스냅샷을 가져옴
                var docSnapShot = await docRef.GetSnapshotAsync();
                
                // 해당 문서가 존재 하지 않다면, 로그 출력후 null 반환
                if (!docSnapShot.Exists)
                {
                    Debug.LogError("Document does not Exists");
                    return null;
                }
                
                // 스냅샷 데이터를 TopicData로 변환
                var topicData = docSnapShot.ConvertTo<TopicData>();
                
                // 변환된 TopicData를 반환
                return topicData;
            }
            catch (Exception e)
            {
                // 오류 발생 시 로그 출력후 예외 발생
                Debug.LogError(e);
                throw;
            }
        }
        /// <summary>
        /// GalleryData 컬렉션의 모든 문서를 TopicData 리스트로 반환하는 비동기 메서드
        /// </summary>
        /// <returns>GalleryData내에 모든 TopicData List</returns>
        public async Task<List<TopicData>> GetAllTopicData()
        {
            try
            {
                // GalleryData의 스냅샷을 가져옴
                var snapshot = await _firestore.Collection("GalleryData").GetSnapshotAsync();
                
                // TopicData를 담을 리스트를 생성
                var topicDataList = new List<TopicData>();
                
                // 스냅샷의 각 문서들을 순회함
                foreach (var document in snapshot.Documents)
                {
                    Debug.Log(document.Id);
                    
                    // 문서를 TopicData로 변환
                    var topicData = document.ConvertTo<TopicData>();
                    
                    // 변환된 리스트에 추가
                    topicDataList.Add(topicData);
                }
                
                // 모두 변환 및 추가된 TopicData리스트를 반환
                return topicDataList;
            }
            catch (Exception e)
            {
                // 오류 발생 시 로그 출력 후 예외 발생
                Debug.LogError(e);
                throw;
            }
        }
        /// <summary>
        /// 새로운 TopicData를 추가하는 비동기 메서드
        /// </summary>
        /// <param name="collectionName">컬렉션 이름</param>
        /// <param name="documentName">문서 이름</param>
        /// <param name="topicData">추가할 TopicData</param>
        public async Task AddTopicData(string collectionName, string documentName, TopicData topicData)
        {
            try
            {
                // Firestore 퀄렉션과 문서를 참조
                var documentReference = _firestore.Collection(collectionName).Document(documentName);
                
                // 문서에 TopicData를 설정
                await documentReference.SetAsync(topicData);
            }
            catch (Exception e)
            {
                // 오류 발생 시 로그 출력 후 예외 발생
                Debug.LogError(e);
                throw;
            }
        }
        /// <summary>
        /// 새로운 UserData를 추가하는 비동기 메서드
        /// </summary>
        /// <param name="fuid">유저의 FUID</param>
        /// <param name="userData">새로 추가할 UserData</param>
        public async Task AddUserData(string fuid, UserData userData)
        {
            try
            {
                var docRef = _firestore.Collection("UserData").Document(fuid);
                await docRef.SetAsync(userData);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                // Console.WriteLine(e);
                throw;
            }
        }
        /// <summary>
        /// 기존 TopicData를 업데이트하는 비동기 메서드
        /// </summary>
        /// <param name="collectionName">컬렉션 이름</param>
        /// <param name="documentName">문서 이름</param>
        /// <param name="topicData">업데이트할 TopicData</param>
        public async Task UpdateTopicData(string collectionName, string documentName, TopicData topicData)
        {
            try
            {
                // Firestore에서 컬렉션과 문서를 참조
                var documentReference = _firestore.Collection(collectionName).Document(documentName);
                
                // 문서에 TopicData를 업데이트 (이미 존재하는 필드는 유지)
                await documentReference.SetAsync(topicData, SetOptions.MergeAll);
            }
            catch (Exception e)
            {
                // 오류 발생 시 로그 출력후 예외 발생
                Debug.LogError(e);
                throw;
            }
        }
    }
}