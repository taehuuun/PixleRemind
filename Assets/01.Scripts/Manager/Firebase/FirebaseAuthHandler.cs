using System;
using System.Threading.Tasks;
using Firebase.Auth;
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
        public async Task TryFirebaseLogin()
        {
            // GoogleAuthProvider를 사용하여 IdToken을 기반으로 로그인 자격 증명을 생성합니다.
            Credential credential = GoogleAuthProvider.GetCredential(GPGSUtill.IdToken, null);

            try
            {
                // 생성된 인증 정보를 이용해 Firebase에 비동기 로그인을 시도합니다. 
                // 로그인 작업이 완료될 때까지 기다리기 위해 'await' 키워드를 사용합니다.
                var newUser = await _auth.SignInWithCredentialAsync(credential);
                
                // FirebaseUser 인스턴스에서 사용자의 Firebase UID를 가져와 저장합니다.
                FUID = newUser.UserId;

                Debug.Log("Firebase Auth 로그인 성공");
                Debug.Log($"IdToken : {GPGSUtill.IdToken}");
                Debug.Log($"FUID : {FUID}");
            }
            catch (Exception e)
            {
                Debug.LogError($"Firebase Auth 로그인 중 오류 발생 : {e}");
            }
        }
    }
}
