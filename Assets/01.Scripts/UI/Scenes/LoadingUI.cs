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
    
    protected override void Initialize()
    {
        _waitForRepeatDelay = new WaitForSeconds(repeatDelay);
        _waitForScaleDelay = new WaitForSeconds(scaleDelay);
        loadingBar.fillAmount = 0f;
        taskText.text = "";
        percentText.text = "0%";

        StartCoroutine(ScaleText());
        StartCoroutine(ShowLoadingProgress());
        LoadingManager.Instance.LoadSceneAsync(LoadingManager.Instance.NextSceneName);
        _ = LoadingManager.Instance.RunTasks();
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
        while (!LoadingManager.Instance.AllTaskComplete)
        {
            loadingBar.fillAmount = LoadingManager.Instance.TaskProgress;
            percentText.text = $"{LoadingManager.Instance.TaskProgress * 100f:#.##} %";
            taskText.text = LoadingManager.Instance.CurrentTask;

            yield return null;
        }

        loadingBar.fillAmount= 1f;
        percentText.text = $"100 %";
        taskText.text = $"{LoadingManager.Instance.NextSceneName} 씬으로 로딩중..";
        
        while (!LoadingManager.Instance.IsSceneLoadingCompleted())
        {
            loadingBar.fillAmount = LoadingManager.Instance.TaskProgress;
            percentText.text = $"{LoadingManager.Instance.TaskProgress * 100f:#.##} %";
            
            yield return null;
        }
        taskText.text = $"{LoadingManager.Instance.NextSceneName} 씬으로 로딩 완료..";
        
        LoadingManager.Instance.ActivateScene();
        
        LoadingManager.Instance.ResetTasks();
    }
}