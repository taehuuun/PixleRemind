using System.Threading.Tasks;
using LTH.PixelRemind.Managers.Firebase;
using UnityEngine;

namespace LTH.PixelRemind.Managers.Login
{
    public class LoginManager : MonoBehaviour
    {
        private async void Start()
        {
            Debug.Log("GPGS Init");
            
            // GPGSUtill을 초기화
            GPGSUtill.Init(); 

            // LoginAsync를 호출
            Debug.Log("LoginAsync");
            
            await Login(); 
        }
        
        /// <summary>
        /// Login 메서드
        /// </summary>
        private async Task Login()
        {
            // GPGSUtill을 통해 로그인을 시도
            if (await GPGSUtill.LoginAsync())
            {
                // 로그인이 성공하면, Firebase Auth 로그인을 시도
                await FirebaseManager.ins.FireAuth.TryFirebaseLogin();
                
                // GPGSUtill의 IdToken과 FUID를 로그로 출력
                Debug.Log($"IdToken : {GPGSUtill.IdToken}");
                Debug.Log($"FUID : {FirebaseManager.ins.FireAuth.FUID}");
            }
        }
    }
}