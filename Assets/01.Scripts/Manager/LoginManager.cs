using System;
using UnityEngine;
using System.Threading.Tasks;

namespace LTH.ColorMatch.Managers
{
    public class LoginManager : MonoBehaviour
    {
        private void Start()
        {
            Debug.Log("GPGS Init");
            // GPGSUtill을 초기화
            GPGSUtill.Init(); 

            // LoginAsync를 호출
            Debug.Log("LoginAsync");
            LoginAsync(); 
        }

        private async void LoginAsync()
        {
            try
            {
                Debug.Log("LoginAsync Start");
                // Login 메소드를 호출하고 반환값인 idToken을 받음
                string idToken = await Login(); 
                
                Debug.Log("!!!!!");
                // Firebase authentication을 시도하고 반환값인 firebaseLoginSuccess를 받음
                bool firebaseLoginSuccess = await FirebaseManager.ins.FireAuth.TryFirebaseLogin(idToken);
                
                Debug.Log($"@@@@@@ : {idToken}");
                
                if (!firebaseLoginSuccess)
                {
                    // Firebase authentication이 실패하면 로그를 출력
                    Debug.LogError("Firebase Login Failed");
                }
            }
            catch (Exception e)
            {
                // Login 메소드 실행 중 예외가 발생하면 로그를 출력
                Debug.LogError($"Google Game Services login failed: {e.Message}"); 
            }
        }

        private Task<string> Login()
        {
            Debug.Log("Login Start");
            // TaskCompletionSource를 생성
            var tcs = new TaskCompletionSource<string>(); 
            
            // GPGSUtill 로그인 메소드에 콜백을 등록
            GPGSUtill.Login((success, token) => 
            {
                Debug.Log($"Login : {success}");
                if (success)
                {
                    // 성공하면 반환값을 TaskCompletionSource에 설정
                    tcs.SetResult(token); 
                }
                else
                {
                    // 실패하면 예외를 TaskCompletionSource에 설정
                    tcs.SetException(new Exception("Google Game Services login failed.")); 
                }
            });
            
            Debug.Log($"Login Result: {tcs.Task}");
            // TaskCompletionSource.Task를 반환
            return tcs.Task; 
        }
    }
}