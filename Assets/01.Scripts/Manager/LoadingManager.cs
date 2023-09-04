using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
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

    public void StartNextSceneLoading(string nextScene)
    {
        NextSceneName = nextScene;
        SceneManager.LoadScene(SceneNames.LoadingScene);
    }
    public void LoadSceneAsync(string sceneName)
    {
        _sceneLoadingOperation = SceneManager.LoadSceneAsync(sceneName);
        _sceneLoadingOperation.allowSceneActivation = false;
    }

    public bool IsSceneLoadingCompleted() => _sceneLoadingOperation?.isDone ?? false;

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

    public async Task ExecuteTask(TaskData taskData, int index, int total)
    {
        CurrentTask = taskData.Name;
        await taskData.Task.Invoke();
        TaskProgress = (float)(index + 1) / total;
        CurrentTask = $"{taskData.Name} 완료";
        await Task.Delay(1000);
    }
    
    /// <summary>
    /// 추가된 Task를 하나씩 진행 시키는 메서드
    /// </summary>
    public async Task RunTasks()
    {
        CurrentTask = "";

        int totalTasks = _tasks.Count;
        
        if (totalTasks > 0)
        {
            for (int i = 0; i < _tasks.Count; i++)
            {
                await ExecuteTask(_tasks[i], i, totalTasks);
            }
        }
        else
        {
            float randomWaitTime = UnityEngine.Random.Range(0.6f, 1.0f);
            await Task.Delay(TimeSpan.FromSeconds(randomWaitTime));
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