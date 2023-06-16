using LTH.PixelRemind.Managers;
using LTH.PixelRemind.Managers.Login;
using LTH.PixelRemind.Util;

namespace LTH.PixelRemind.UI
{
    public class LoginUI : BodyUI
    {
        private void Start()
        {
            LoadingTaskManager.Instance.AddTask(GPGSUtil.Init,"구글 플레이 서비스 초기화...");
            LoadingTaskManager.Instance.AddTask(LoginManager.Login, "로그인...");
            LoadingTaskManager.Instance.AddTask(LoginManager.LoadUserData, "유저 데이터 가져오는 중...");
            LoadingTaskManager.Instance.NextSceneName = "MainScene";
            MoveScene("LoadingScene");
        }
    }
}
