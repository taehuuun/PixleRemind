using System.Threading.Tasks;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;

namespace LTH.PixelRemind.Util
{
    public static class GPGSUtil
    {
        // 로그인(인증)후 IdToken을 저장, 해당 함수 내에서만 값 변경 가능
        public static string IdToken { get; private set; }
        
        // 초기화 상태를 나타내는 필드
        private static bool _initialized = false;

        /// <summary>
        /// 구글 플레이 서비스에서 사용되는 PlayGamesPlatform 객체를 초기화 시키는 메서드
        /// </summary>
        public static Task Init()
        {
            // PlayGamesClientConfiguration을 사용하여 구성 객체를 만듭니다.
            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
                .RequestEmail()                 // 사용자의 이메일 주소 요청
                .RequestIdToken()               // 사용자 인증에 필요한 토큰 요청
                .RequestServerAuthCode(false)   // 인증 코드를 요청하지 않도록 설정합니다.
                .Build();
            
            // config 인스턴스를 전달하여 PlayGamesPlatform 객체를 초기화
            PlayGamesPlatform.InitializeInstance(config);
            
            // 디버그 로그를 활성, 비활성
            PlayGamesPlatform.DebugLogEnabled = false;
            
            // PlayGamesPlatform을 활성화
            PlayGamesPlatform.Activate();

            // 초기화 상태를 true로 변경
            _initialized = true;
            return Task.CompletedTask;
        }
        
        /// <summary>
        /// 이 메서드는 Google Play Games 서비스를 사용하여 로그인하는 비동기 작업을 담당
        /// </summary>
        /// <returns>로그인 성공 유무를 반환</returns>
        public static async Task<bool> LoginAsync()
        {
            // 우선, Google Play Games 서비스 플러그인이 초기화 되었는지 확인
            // 만약 초기화되지 않았다면, 에러 메시지를 출력하고 false를 반환하여 로그인 실패를 리턴.
            if (!_initialized)
            {
                Debug.LogError("GPGSUtill이 초기화 되지 않았습니다");
                return false;
            }
            
            // 사용자 인증의 성공/실패 여부를 담을 TaskCompletionSource를 생성
            var tcs = new TaskCompletionSource<bool>();
            
            // Google Play Games 서비스를 통해 로컬 사용자의 인증을 시도
            Social.localUser.Authenticate((success =>
            {
                    // 인증에 성공 하였을 경우
                    if (success)
                    {
                        // 로컬 사용자의 ID 토큰을 가져와서 IdToken에 저장 
                        // 로그인 성공 메시지와 함께 ID 토큰을 출력 (디버그)
                        IdToken = ((PlayGamesLocalUser)Social.localUser).GetIdToken();
                        Debug.Log($"로그인 성공! ID Token : {IdToken}");
                        
                        // TaskCompletionSource의 결과를 true로 설정
                        tcs.SetResult(true);
                    }
                    else
                    {
                        // 인증에 실패하면, 로그인 실패 메시지를 출력하고,
                        Debug.LogError("로그인 실패");
                        
                        // TaskCompletionSource의 결과를 false로 설정
                        tcs.SetResult(false);
                    }
            }));
            
            // 로그인 성공/실패 여부를 비동기적으로 반환
            // 이 때, 로그인 작업이 완료될 때까지 여기에서 대기
            return await tcs.Task;
        }
    }
}
