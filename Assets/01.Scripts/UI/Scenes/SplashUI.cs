using System.Collections;
using UnityEngine;

public class SplashUI : BodyUI
{
    [SerializeField] private SplashText splashText;
    private void Start()
    {
        StartCoroutine(WaitSplash());
    }

    private IEnumerator WaitSplash()
    {
        yield return new WaitUntil(() => splashText.Complete);
        LoadingTaskManager.Instance.AddTask(GPGSHelper.Init, "구글 플레이 서비스 초기화...");
        LoadingTaskManager.Instance.AddTask(LoginManager.Login, "로그인...");
        LoadingTaskManager.Instance.AddTask(DataSyncManager.SyncUserDataAndLocalData, "유저 / 로컬 데이터 동기화 중...");
        LoadingTaskManager.Instance.NextSceneName = SceneNames.MainScene;
        MoveScene(SceneNames.LoadingScene);
    }
}