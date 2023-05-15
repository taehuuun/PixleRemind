using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class GPGSUtill
{
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
    }
}
