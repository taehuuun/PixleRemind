using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadingUI : BodyUI
{
    [SerializeField] private Image _loadingBar;
    [SerializeField] private TMP_Text _taskText;

    private void Start()
    {
        _loadingBar.fillAmount = 0f;
        _taskText.text = "";

        StartCoroutine(ShowLoadingProgress());
        _ = LoadingTaskManager.Instance.RunTasks();
    }

    private IEnumerator ShowLoadingProgress()
    {
        while (!LoadingTaskManager.Instance.AllTaskComplete)
        {
            _loadingBar.fillAmount = LoadingTaskManager.Instance.TaskProgress;
            _taskText.text = LoadingTaskManager.Instance.CurrentTask;

            yield return null;
        }

        _loadingBar.fillAmount = 1f;
        _taskText.text = $"{LoadingTaskManager.Instance.NextSceneName} 씬으로 이동중..";
        MoveScene(LoadingTaskManager.Instance.NextSceneName);
    }
}