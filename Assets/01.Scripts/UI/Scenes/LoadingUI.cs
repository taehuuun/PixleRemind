using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadingUI : BaseSceneUI
{
    [SerializeField] private Image loadingBar;
    [SerializeField] private TMP_Text taskText;
    [SerializeField] private TMP_Text percentText;
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
        loadingBar.fillAmount = 0f;
        taskText.text = "";
        percentText.text = "0%";

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
            loadingBar.fillAmount = LoadingTaskManager.Instance.TaskProgress;
            percentText.text = $"{LoadingTaskManager.Instance.TaskProgress * 100f:#.##} %";
            taskText.text = LoadingTaskManager.Instance.CurrentTask;

            yield return null;
        }

        loadingBar.fillAmount= 1f;
        percentText.text = $"100 %";
        taskText.text = $"{LoadingTaskManager.Instance.NextSceneName} 씬으로 로딩중..";
        
        LoadingTaskManager.Instance.ActivateScene();

        while (!LoadingTaskManager.Instance.IsSceneLoadingCompleted())
        {
            loadingBar.fillAmount = LoadingTaskManager.Instance.TaskProgress;
            percentText.text = $"{LoadingTaskManager.Instance.TaskProgress * 100f:#.##} %";
            
            yield return null;
        }
        
        taskText.text = $"{LoadingTaskManager.Instance.NextSceneName} 씬으로 로딩 완료..";
        
        LoadingTaskManager.Instance.ResetTasks();
        MoveScene(LoadingTaskManager.Instance.NextSceneName);
    }
}