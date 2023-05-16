using System;
using System.Threading.Tasks;
using Firebase.Auth;
using Firebase.Extensions;
using UnityEngine;

namespace LTH.ColorMatch.Handlers
{
    public class FirebaseAuthHandler : MonoBehaviour
    {
        private readonly FirebaseAuth _auth;
        
        public FirebaseAuthHandler()
        {
            // FirebaseAuth 인스턴스를 초기화
            _auth = FirebaseAuth.DefaultInstance;
        }
        
        /// <summary>
        /// Firebase를 통해 로그인을 시도 하는 함수
        /// </summary>
        /// <param name="idToken">구글 플레이 게임 서비스로부터 받은 IdToken</param>
        /// <returns>로그인 성공 여부</returns>
        public async Task<bool> TryFirebaseLogin(string idToken)
        {
            try
            {
                // ID 토큰을 사용한 Credential 생성
                Credential credential = GoogleAuthProvider.GetCredential(idToken, null);
                
                // Credential을 사용한 비동기 로그인 시도 후 결과 반환
                await _auth.SignInWithCredentialAsync(credential).ContinueWithOnMainThread(task =>
                {
                    // 취소된 경우 로그 출력후 false 반환
                    if (task.IsCanceled)
                    {
                        Debug.LogError($"Firebase Login Cancled");
                        return false;
                    }
                    
                    // 문제가 발생한 경우 로그 출력후 false 반환
                    if (task.IsFaulted)
                    {
                        Debug.LogError("Firebase login failed");
                        return false;
                    }

                    // 로그인 성공 시 FirebaseUser 객체를 얻고 로그를 출력
                    FirebaseUser user = task.Result;
                    
                    Debug.Log($"Firebase user logged in : {user.DisplayName} {user.UserId}");
                    
                    // 로그인 성공 반환 
                    return true;
                });
            }
            catch (Exception e)
            {
                // 예외 발생 시 로그를 출력하고 false를 반환
                Debug.LogError($"Firebase Login Error : {e.Message}");
                return false;
            }

            return false;
        }
    }
}
