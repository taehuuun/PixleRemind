using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LTH.PixelRemind.Data;
using UnityEngine;

namespace LTH.PixelRemind.Managers
{
    public class LoadingTaskManager : MonoBehaviour
    {
        private static LoadingTaskManager _instance;
        public static LoadingTaskManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = nameof(LoadingTaskManager);
                    _instance = obj.AddComponent<LoadingTaskManager>();
                }

                return _instance;
            }
        }
        
        public float TaskProgress { get; private set; }
        public bool AllTaskComplete { get; private set; }
        public string CurrentTask { get; private set; }

        private readonly List<TaskData> _tasks = new List<TaskData>();
        
        public void AddTask(Func<Task> task, string name)
        {
            _tasks.Add(new TaskData(task,name));
        }

        public async Task RunTasks()
        {
            for (int i = 0; i < _tasks.Count; i++)
            {
                CurrentTask = _tasks[i].Name;
                await _tasks[i].Task.Invoke();
                TaskProgress = (float)(i + 1) / _tasks.Count;
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
}

