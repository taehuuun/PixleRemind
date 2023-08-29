using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Michsky.MUIP;

public class LoadingUI : BodyUI
{
    [SerializeField] private ProgressBar loadingBar;
    // [SerializeField] private Image loadingBar;
    [SerializeField] private TMP_Text taskText;
    [SerializeField] private TMP_Text[] splashTexts;
    [SerializeField] private float scaleDuration;
    [SerializeField] private float scaleDelay;
    [SerializeField] private float repeatDelay;
    [SerializeField] private string fullText;

    private WaitForSeconds _waitForRepeatDelay;
    private WaitForSeconds _waitForScaleDelay;
    
    private void Start()
    {
        _waitForRepeatDelay = new WaitForSeconds(repeatDelay);
        _waitForScaleDelay = new WaitForSeconds(scaleDelay);
        loadingBar.currentPercent = 0f;
        taskText.text = "";

        StartCoroutine(ScaleText());
        StartCoroutine(ShowLoadingProgress());
        LoadingTaskManager.Instance.LoadSceneAsync(LoadingTaskManager.Instance.NextSceneName);
        _ = LoadingTaskManager.Instance.RunTasks();
    }

    private IEnumerator ScaleText()
    {
        while (true)
        {
            for (int i = 0; i < fullText.Length; i++)
            {
                StartCoroutine(UIAnimation.ScaleUI(splashTexts[i].transform, splashTexts[i].transform.localScale * 1.1f,scaleDuration));
                yield return _waitForScaleDelay;
            }

            yield return _waitForRepeatDelay;
        }
    }
    
    private IEnumerator ShowLoadingProgress()
    {
        while (!LoadingTaskManager.Instance.AllTaskComplete)
        {
            loadingBar.currentPercent = LoadingTaskManager.Instance.TaskProgress * 100f;
            loadingBar.textPercent.text = $"{LoadingTaskManager.Instance.TaskProgress * 100f:#.##} %";
            taskText.text = LoadingTaskManager.Instance.CurrentTask;

            yield return null;
        }

        loadingBar.currentPercent = 100f;
        loadingBar.textPercent.text = $"100 %";
        taskText.text = $"{LoadingTaskManager.Instance.NextSceneName} 씬으로 로딩중..";
        
        LoadingTaskManager.Instance.ActivateScene();

        while (!LoadingTaskManager.Instance.IsSceneLoadingCompleted())
        {
            loadingBar.currentPercent = LoadingTaskManager.Instance.TaskProgress * 100f;
            loadingBar.textPercent.text = $"{LoadingTaskManager.Instance.TaskProgress * 100f:#.##} %";
            
            yield return null;
        }
        
        taskText.text = $"{LoadingTaskManager.Instance.NextSceneName} 씬으로 로딩 완료..";
        
        LoadingTaskManager.Instance.ResetTasks();
        MoveScene(LoadingTaskManager.Instance.NextSceneName);
    }
}