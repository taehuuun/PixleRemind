using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    public static LoadingManager Instance { get; private set; }
    
    public float TaskProgress { get; private set; }
    public bool AllTaskComplete { get; private set; }
    public string NextSceneName { get; set; }
    public string CurrentTask { get; private set; }

    private AsyncOperation _sceneLoadingOperation;
    private readonly List<TaskData> _tasks = new List<TaskData>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        DontDestroyOnLoad(this);
    }

    public void LoadSceneAsync(string sceneName)
    {
        _sceneLoadingOperation = SceneManager.LoadSceneAsync(sceneName);
        _sceneLoadingOperation.allowSceneActivation = false;
    }

    public bool IsSceneLoadingCompleted()
    {
        if (_sceneLoadingOperation != null)
        {
            return _sceneLoadingOperation.isDone;
        }

        return false;
    }

    public void ActivateScene()
    {
        if (_sceneLoadingOperation != null)
        {
            _sceneLoadingOperation.allowSceneActivation = true;
        }
    }
    
    /// <summary>
    /// 진행할 Task를 추가하는 메서드
    /// </summary>
    /// <param name="task">추가할 작업</param>
    /// <param name="name">UI에 표시할 문구</param>
    public void AddTask(Func<Task> task, string name)
    {
        _tasks.Add(new TaskData(task, name));
    }

    /// <summary>
    /// 추가된 Task를 하나씩 진행 시키는 메서드
    /// </summary>
    public async Task RunTasks()
    {
        CurrentTask = "";
        for (int i = 0; i < _tasks.Count; i++)
        {
            CurrentTask = _tasks[i].Name;
            await _tasks[i].Task.Invoke();
            TaskProgress = (float)(i + 1) / _tasks.Count;
            CurrentTask = $"{_tasks[i].Name} 완료!";
            await Task.Delay(1000);
        }

        AllTaskComplete = true;
    }
    
    /// <summary>
    /// 모든 Task를 제거하고 초기 상태로 세팅하는 메서드
    /// </summary>
    public void ResetTasks()
    {
        _tasks.Clear();
        TaskProgress = 0f;
        AllTaskComplete = false;
    }
}