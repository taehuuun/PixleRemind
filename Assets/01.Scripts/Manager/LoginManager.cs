using System;
using System.Collections.Generic;
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
}