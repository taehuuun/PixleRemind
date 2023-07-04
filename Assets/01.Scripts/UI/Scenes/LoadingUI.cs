using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadingUI : BodyUI
{
    [SerializeField] private Image loadingBar;
    [SerializeField] private TMP_Text taskText;

    private void Start()
    {
        loadingBar.fillAmount = 0f;
        taskText.text = "";

        StartCoroutine(ShowLoadingProgress());
        LoadingTaskManager.Instance.LoadSceneAsync(LoadingTaskManager.Instance.NextSceneName);
        _ = LoadingTaskManager.Instance.RunTasks();
    }
    
    private IEnumerator ShowLoadingProgress()
    {
        while (!LoadingTaskManager.Instance.AllTaskComplete)
        {
            loadingBar.fillAmount = LoadingTaskManager.Instance.TaskProgress;
            taskText.text = LoadingTaskManager.Instance.CurrentTask;

            yield return null;
        }

        loadingBar.fillAmount = 1f;
        taskText.text = $"{LoadingTaskManager.Instance.NextSceneName} 씬으로 로딩중..";
        
        LoadingTaskManager.Instance.ActivateScene();

        while (!LoadingTaskManager.Instance.IsSceneLoadingCompleted())
        {
            loadingBar.fillAmount = LoadingTaskManager.Instance.TaskProgress;
            yield return null;
        }
        
        taskText.text = $"{LoadingTaskManager.Instance.NextSceneName} 씬으로 로딩 완료..";
        
        LoadingTaskManager.Instance.ResetTasks();
        MoveScene(LoadingTaskManager.Instance.NextSceneName);
    }
}