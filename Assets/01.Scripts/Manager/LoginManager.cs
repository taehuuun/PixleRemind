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
        Debug.Log("LoginManager Login");
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

    public static async Task LoadUserData()
    {
        try
        {
            Debug.Log("LoginManager LoadUserData");
#if UNITY_ANDROID && !UNITY_EDITOR
        string FUID = FirebaseManager.ins.FireAuth.FUID;
#else
            string FUID = "Test";
#endif
            bool userDataExists =
                await FirebaseManager.ins.Firestore.CheckDocumentExists(FirestoreCollections.UserData, FUID);
            Debug.Log("LoginManager LoadUserData 1");
            if (DataManager.Instance.userData == null)
            {
                DataManager.Instance.userData = new UserData();
            }

            Debug.Log("LoginManager LoadUserData 2");

            if (!userDataExists)
            {
                Debug.Log("LoginManager LoadUserData 3");
                Debug.Log("최초 접속 유저");
                DataManager.Instance.userData.LastUpdated = DateTime.Now;
                await FirebaseManager.ins.Firestore.AddData(FirestoreCollections.UserData, FUID,
                    DataManager.Instance.userData);
            }
            else
            {
                Debug.Log("LoginManager LoadUserData 3");
                Debug.Log("기존 유저");
                DataManager.Instance.userData =
                    await FirebaseManager.ins.Firestore.GetData<UserData>(FirestoreCollections.UserData, FUID);
            }

            Debug.Log("LoginManager LoadUserData 4");

            DataManager.Instance.userData.LocalTopicDataIDs =
                DataManager.GetTargetFolderFileNames(DataPath.GalleryDataPath);
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to get topic data ids. Exception: {ex}");
        }
    }
}