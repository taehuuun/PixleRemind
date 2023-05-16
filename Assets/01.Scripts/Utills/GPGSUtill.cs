using System;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;

public static class GPGSUtill
{
    // 초기화 상태를 나타내는 변수를 추가합니다.
    private static bool _initialized = false;

    /// <summary>
    /// 구글 플레이 서비스에서 사용되는 PlayGamesPlatform 객체를 초기화 시키는 함수
    /// </summary>
    public static void Init()
    {
        // PlayGamesClientConfiguration을 사용하여 구성 객체를 만듭니다.
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
            .RequestEmail()                 // 사용자의 이메일 주소 요청
            .RequestIdToken()               // 사용자 인증에 필요한 토큰 요청
            .RequestServerAuthCode(false)   // 인증 코드를 요청하지 않도록 설정합니다.
            .Build();
        
        // config 인스턴스를 전달하여 PlayGamesPlatform 객체를 초기화
        PlayGamesPlatform.InitializeInstance(config);
        
        // 디버그 로그를 비활성화
        PlayGamesPlatform.DebugLogEnabled = false;
        
        // PlayGamesPlatform을 활성화
        PlayGamesPlatform.Activate();
        
        // 초기화 상태 변경
        _initialized = true;
    }
    
    /// <summary>
    ///  구글 플레이 게임 서비스 로그인 함수
    /// </summary>
    /// <param name="callback">로그인 결과 에따른 콜백</param>
    public static void Login(Action<bool,string> callback)
    {
        // 초기화 유무 체크
        if (!_initialized)
        {
            Debug.LogError("GPGSUtill이 초기화 되지 않았습니다");
            return;
        }
        
        // 사용자 인증을 수행 후 결과를 콜백 함수로 리턴
        Social.localUser.Authenticate((success =>
        {
            // 성공 하였을 경우, ID토큰을 가져옴
            if (success)
            {
                string idToken = ((PlayGamesLocalUser)Social.localUser).GetIdToken();
                Debug.Log($"로그인 성공! ID Token : {idToken}");
                
                // 콜백 함수에 결과 리턴
                callback(true, idToken);
            }
            else
            {
                // 실패 하였을 경우, 실패내역 콜백 전달
                Debug.LogError("로그인 실패");
                callback(false, "");
            }
        }));
    }
}
