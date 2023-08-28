using System.Collections;
using TMPro;
using UnityEngine;

public class SplashUI : BodyUI
{
    [SerializeField] private TMP_Text[] splashText;
    [SerializeField] private float delay;
    [SerializeField] private float completeDelay;
    [SerializeField] private float scaleDuration;
    [SerializeField] private float moveDuration;
    [SerializeField] private string fullText;
    
    private WaitForSeconds _waitForDelay;
    private WaitForSeconds _waitForMoveDelay;
    private WaitForSeconds _waitForCompleteDelay;
    
    private void Start()
    {
        _waitForDelay = new WaitForSeconds(delay);
        _waitForMoveDelay = new WaitForSeconds(moveDuration + delay);
        _waitForCompleteDelay = new WaitForSeconds(completeDelay);
        StartCoroutine(SplashAfter());
    }

    private IEnumerator Splash()
    {
        yield return UIAnimation.ShowTextOneByOne(splashText, fullText, delay, scaleDuration);

        // Step 2: Move LT up and H down
        yield return _waitForDelay;
        StartCoroutine(UIAnimation.MoveUI(splashText[0].transform, new Vector3(0, 150, 0), moveDuration));
        StartCoroutine(UIAnimation.MoveUI(splashText[1].transform, new Vector3(0, 150, 0), moveDuration));
        StartCoroutine(UIAnimation.MoveUI(splashText[2].transform, new Vector3(0, -150, 0), moveDuration));

        // Step 3: Move LT to right and H to left
        yield return _waitForMoveDelay;
        StartCoroutine(UIAnimation.MoveUI(splashText[0].transform, new Vector3(50, 0, 0), moveDuration));
        StartCoroutine(UIAnimation.MoveUI(splashText[1].transform, new Vector3(120, 0, 0), moveDuration));
        StartCoroutine(UIAnimation.MoveUI(splashText[2].transform, new Vector3(-225, 0, 0), moveDuration));
        
        yield return _waitForDelay;
        
        for (int i = 0; i < fullText.Length; i++)
        {
            StartCoroutine(UIAnimation.ScaleUI(splashText[i].transform, splashText[i].transform.localScale * 1.1f,scaleDuration));
            yield return _waitForDelay;
        }
        
        yield return _waitForCompleteDelay;
        yield return SplashAfter();
    }
    
    private IEnumerator SplashAfter()
    {
        LoadingTaskManager.Instance.AddTask(GPGSHelper.Init, "구글 플레이 서비스 초기화...");
        LoadingTaskManager.Instance.AddTask(LoginManager.Login, "로그인...");
        LoadingTaskManager.Instance.AddTask(DataSyncManager.SyncUserDataAndLocalData, "유저 / 로컬 데이터 동기화 중...");
        LoadingTaskManager.Instance.NextSceneName = SceneNames.MainScene;
        MoveScene(SceneNames.LoadingScene);
        yield return null;
    }
}