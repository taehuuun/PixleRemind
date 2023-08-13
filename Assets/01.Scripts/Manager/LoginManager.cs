using System;
using System.Threading.Tasks;
using UnityEngine;

public class LoginManager : MonoBehaviour
{
    /// <summary>
    /// Login 메서드
    /// </summary>
    public static async Task Login()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
            // GPGSUtill을 통해 로그인을 시도
            if (await GPGSUtill.LoginAsync())
            {
                // 로그인이 성공하면, Firebase Auth 로그인을 시도
                if (await FirebaseManager.ins.FireAuth.TryFirebaseLogin())
                {
                    await Initialize();
                    // GPGSUtill의 IdToken과 FUID를 로그로 출력
                    Debug.Log($"IdToken : {GPGSUtill.IdToken}");
                    Debug.Log($"FUID : {FirebaseManager.ins.FireAuth.FUID}");
                }
                else
                {
                    Debug.LogError("Firebase 로그인 실패");
                }
            }
            else
            {
                Debug.LogError("GPGS 로그인 실패");
            }
#endif
    }
    
    /// <summary>
    /// Firestore를 통해 UserData를 로드하는 메서드
    /// </summary>
    public static async Task LoadUserData()
    {
        try
        {
#if UNITY_ANDROID && !UNITY_EDITOR
        string FUID = FirebaseManager.ins.FireAuth.FUID;
#else
            string FUID = "Test";
#endif
            bool userDataExists =
                await FirebaseManager.ins.Firestore.CheckDocumentExists(FirestoreCollections.UserData, FUID);

            if (DataManager.userData == null)
            {
                DataManager.userData = new UserData();
            }

            if (!userDataExists)
            {
                DataManager.userData.LastUpdated = DateTime.Now;
                await FirebaseManager.ins.Firestore.AddData(FirestoreCollections.UserData, FUID,
                    DataManager.userData);
            }
            else
            {
                DataManager.userData = await FirebaseManager.ins.Firestore.GetData<UserData>(FirestoreCollections.UserData, FUID);
            }

            DataManager.userData.LocalTopicDataIDs =
                DataManager.GetTargetFolderFileNames(DataPath.LocalTopicData);
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to get topic data ids. Exception: {ex}");
        }
    }
}