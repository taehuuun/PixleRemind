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
            
            // Firestore의 FUID에 해당하는 UserData와 LocalData가 존재하는지 체크
            // 둘 다 존재 할경우)
            //      두 데이터를 비교하여 UserData에 존재 하지만, LocalData에 존재하지 않는 데이터를 찾아 
            //      LocalData를 UserData와 일치 시킴
            // UserData만 존재 할 경우)
            //      LocalData의 내용을 UserData와 일치 시킴
            // LocalData만 존재 하는 경우
            //      UserData가 존재하지 않는 경우 해당 FUID의 데이터가 없음 (플레이 한 적이 없음) 하지만 LocalData가 존재 할 때 
            //      다른 플레이어의 데이터를 경로에 배치 시킨걸로 간주 => 모든 데이터 리셋
            // 둘다 존재 하지 않는 경우
            //      UserData와 LocalData를 새로 생성 후 UserData를 Firestore에 업로드, LocalData는 로컬 경로에 저장
            
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to get topic data ids. Exception: {ex}");
        }
    }
}