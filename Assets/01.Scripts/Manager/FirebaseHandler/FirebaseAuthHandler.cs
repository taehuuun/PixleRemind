using System;
using System.Threading.Tasks;
using Firebase.Auth;
using LTH.ColorMatch.Managers;
using UnityEditor.PackageManager;
using UnityEngine;

namespace LTH.ColorMatch.Managers.FirebaseHandlers
{
    public class FirebaseAuthHandler : MonoBehaviour
    {
        // 사용자의 Firebase UID 속성을 외부에서 수정할수 없도록 함 
        public string FUID { get; private set; }

        // FirebaseAuth클래스의 인스턴스를 참조하는 필드
        private readonly FirebaseAuth _auth;
        
        /// <summary>
        /// 생성자에서 FirebaseAuth의 기본 인스턴스를 초기화
        /// </summary>
        public FirebaseAuthHandler()
        {
            // FirebaseAuth 인스턴스를 초기화
            _auth = FirebaseAuth.DefaultInstance;
        }
        
        /// <summary>
        /// Firebase를 통해 Auth로그인을 시도하는 메서드
        /// </summary>
        public void TryFirebaseLogin()
        {
            // GoogleAuthProvider를 사용하여 IdToken을 기반으로 로그인 자격 증명을 생성합니다.
            Credential credential = GoogleAuthProvider.GetCredential(GPGSUtill.IdToken, null);
            
            // FirebaseAuth 인스턴스의 SignInWithCredentialAsync 메서드를 사용해 Firebase에 로그인을 시도하고,
            // 그 결과를 비동기 작업으로 받습니다.
            _auth.SignInWithCredentialAsync(credential).ContinueWith(task =>
            {
                // 로그인 작업이 취소되었는지 확인
                if (task.IsCanceled)
                {
                    // 에러 로그를 출력하고 리턴
                    Debug.LogError("Firebase Auth 로그인 취소");
                    return;
                }

                // 로그인 작업 중에 오류가 발생했을 경우
                if (task.IsFaulted)
                {
                    // 에러 로그를 출력하고 리턴
                    Debug.LogError($"Firebase Auth 로그인 중 오류 발생 : {task.Exception}");
                    return;
                }

                // 작업이 성공적으로 완료되었다면 FirebaseUser 인스턴스를 가져와 사용자의 Firebase UID를 저장
                FirebaseUser newUser = task.Result;
                FUID = newUser.UserId;
                
                Debug.Log("Firebase Auth 로그인 성공");
            });
        }
    }
}
