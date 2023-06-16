using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Firestore;
using UnityEngine;

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
    /// 특정 컬렉션의 문서를 Data로 변환하여 반환하는 비동기 메서드
    /// </summary>
    /// <param name="collection">컬렉션 이름</param>
    /// <param name="document">문서 이름</param>
    /// <typeparam name="T">데이터 타입</typeparam>
    /// <returns>변환된 Data를 반환</returns>
    public async Task<T> GetData<T>(string collection, string document)
    {
        dynamic data = default(T);

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
                return data;
            }

            // 스냅샷 데이터를 반환 data 타입으로 변환
            data = docSnapShot.ConvertTo<T>();

            // 변환된 data를 반환
            return data;
        }
        catch (Exception e)
        {
            // 오류 발생 시 로그 출력후 예외 발생
            Debug.LogError(e);
            throw;
        }
    }

    /// <summary>
    /// 새로운 Data를 추가하는 비동기 메서드
    /// </summary>
    /// <param name="collection">컬렉션 이름</param>
    /// <param name="document">문서 이름</param>
    /// <param name="data">추가할 TopicData</param>
    /// <typeparam name="T">데이터 타입</typeparam>
    public async Task AddData<T>(string collection, string document, T data)
    {
        try
        {
            // Firestore 퀄렉션과 문서를 참조
            var documentReference = _firestore.Collection(collection).Document(document);

            // 문서에 Data를 설정
            await documentReference.SetAsync(data);
        }
        catch (Exception e)
        {
            // 오류 발생 시 로그 출력 후 예외 발생
            Debug.LogError(e);
            throw;
        }
    }

    public async Task DeleteData(string collection, string document)
    {
        try
        {
            var documentReference = _firestore.Collection(collection).Document(document);
            await documentReference.DeleteAsync();
        }
        catch (Exception e)
        {
            Debug.LogError(e);
            throw;
        }
    }

    /// <summary>
    /// 새로운 Data를 추가하는 비동기 메서드
    /// </summary>
    /// <param name="collection">컬렉션 이름</param>
    /// <param name="data">추가할 데이터</param>
    /// <typeparam name="T">데이터 타입</typeparam>
    /// <returns>DocumentReference</returns>
    public async Task<DocumentReference> AddData<T>(string collection, T data)
    {
        try
        {
            var collectionReference = _firestore.Collection(collection);
            var documentReference = await collectionReference.AddAsync(data);

            return documentReference;
        }
        catch (Exception e)
        {
            Debug.LogError(e);
            throw;
        }
    }

    /// <summary>
    /// 기존 Data를 업데이트하는 비동기 메서드
    /// </summary>
    /// <param name="collection">컬랙션 이름</param>
    /// <param name="document">문서 이름</param>
    /// <param name="data">업데이트 데이터</param>
    /// <typeparam name="T">데이터 타입</typeparam>
    public async Task UpdateData<T>(string collection, string document, T data)
    {
        try
        {
            // Firestore에서 컬렉션과 문서를 참조
            var documentReference = _firestore.Collection(collection).Document(document);

            // 문서에 Data를 업데이트 (이미 존재하는 필드는 유지)
            await documentReference.SetAsync(data, SetOptions.MergeAll);
        }
        catch (Exception e)
        {
            // 오류 발생 시 로그 출력후 예외 발생
            Debug.LogError(e);
            throw;
        }
    }

    /// <summary>
    /// 지정한 Collection내의 모든 데이터들을 리스트로 반환하는 비동기 메서드
    /// </summary>
    /// <param name="collection">컬렉션 이름</param>
    /// <typeparam name="T">데이터 타입</typeparam>
    /// <returns>T타입의 데이터 List</returns>
    public async Task<List<T>> GetAllData<T>(string collection)
    {
        try
        {
            // collection의 스냅샷을 가져옴
            var snapshot = await _firestore.Collection(collection).GetSnapshotAsync();

            // Data를 담을 리스트를 생성
            var dataList = new List<T>();

            // 스냅샷의 각 문서들을 순회함
            foreach (var document in snapshot.Documents)
            {
                Debug.Log(document.Id);

                // 문서를 Data로 변환
                var topicData = document.ConvertTo<T>();

                // 변환된 리스트에 추가
                dataList.Add(topicData);
            }

            // 모두 변환 및 추가된 Data리스트를 반환
            return dataList;
        }
        catch (Exception e)
        {
            // 오류 발생 시 로그 출력 후 예외 발생
            Debug.LogError(e);
            throw;
        }
    }
}