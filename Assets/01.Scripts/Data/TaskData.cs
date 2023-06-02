using System;
using System.Threading.Tasks;
using UnityEngine;

namespace LTH.PixelRemind.Data
{
    public class TaskData : MonoBehaviour
    {
        public Func<Task> Task { get; private set; }
        public string Name { get; private set; }

        public TaskData(Func<Task> task, string name)
        {
            Task = task;
            Name = name;
        }
    }
}

