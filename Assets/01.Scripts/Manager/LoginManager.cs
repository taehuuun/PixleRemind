using System;
using System.Threading.Tasks;
using LTH.PixelRemind.Data;
using LTH.PixelRemind.Managers.Data;
using LTH.PixelRemind.Managers.Data.Paths;
using LTH.PixelRemind.Managers.Firebase;
using LTH.PixelRemind.Managers.Firebase.Collections;
using LTH.PixelRemind.Util;
using UnityEngine;

namespace LTH.PixelRemind.Managers.Login
{
    public class LoginManager : MonoBehaviour
    {
        /// <summary>
        /// Login 메서드
        /// </summary>
        public static async Task Login()
        {
            GPGSUtil.Init();
            
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
#else
            await Initialize();
#endif
        }

        private static async Task Initialize()
        {
            Debug.Log("LoginManager Initialize");
#if UNITY_ANDROID && !UNITY_EDITOR
            string FUID = FirebaseManager.ins.FireAuth.FUID;
#else
            string FUID = "Test";
#endif
            bool userDataExists =
                await FirebaseManager.ins.Firestore.CheckDocumentExists(FirestoreCollections.UserData, FUID);

            if (!userDataExists)
            {
                Debug.Log("최초 접속 유저");
                DataManager.Instance.userData = new UserData();
                DataManager.Instance.userData.LastUpdated = DateTime.Now;
                await FirebaseManager.ins.Firestore.AddData(FirestoreCollections.UserData,FUID, DataManager.Instance.userData);
            }
            else
            {
                Debug.Log("기존 유저");
                DataManager.Instance.userData =
                    await FirebaseManager.ins.Firestore.GetData<UserData>(FirestoreCollections.UserData, FUID);
            }

            DataManager.Instance.userData.LocalTopicDataIDs =
                DataManager.GetTargetFolderFileNames(DataPath.GalleryDataPath);
        }
    }
}