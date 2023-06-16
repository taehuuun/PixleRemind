using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class LoadingTaskManager : MonoBehaviour
{
    public static LoadingTaskManager Instance;

    public float TaskProgress { get; private set; }
    public bool AllTaskComplete { get; private set; }
    public string NextSceneName { get; set; }
    public string CurrentTask { get; private set; }

    private readonly List<TaskData> _tasks = new List<TaskData>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        DontDestroyOnLoad(this);
    }

    public void AddTask(Func<Task> task, string name)
    {
        _tasks.Add(new TaskData(task, name));
    }

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

    public void ResetTasks()
    {
        _tasks.Clear();
        TaskProgress = 0f;
        AllTaskComplete = false;
    }
}