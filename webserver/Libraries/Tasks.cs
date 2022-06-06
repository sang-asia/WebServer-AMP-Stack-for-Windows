using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace WebServer.Libraries
{
    class Tasks
    {
        private readonly List<KeyValuePair<CancellationTokenSource, Task>> _tasks = new List<KeyValuePair<CancellationTokenSource, Task>> { };

        /// <summary>
        /// Create async tasks
        /// </summary>
        public KeyValuePair<CancellationTokenSource, Task> CreateTask(Action f)
        {
            CancellationTokenSource token = new CancellationTokenSource();
            Task task = Task.Factory.StartNew(f, token.Token);
            KeyValuePair<CancellationTokenSource, Task> kvp = new KeyValuePair<CancellationTokenSource, Task>(token, task);

            task.ContinueWith((Task t) => {
                this._tasks.Remove(kvp);
            });

            this._tasks.Add(kvp);

            return kvp;
        }

        /// <summary>
        /// Cancel all Tasks
        /// </summary>
        public int CancelTasks()
        {
            int counter = 0;
            
            foreach (KeyValuePair<CancellationTokenSource, Task> kvp in this._tasks)
            {
                kvp.Key.Cancel();
                counter++;
            }

            return counter;
        }
    }
}
