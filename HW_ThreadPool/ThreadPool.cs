using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ThreadPool
{
    public class ThreadPool : IDisposable
    {
        const int numOfThreads = 7;
        private Queue<Action> tasks = new Queue<Action>();
        private List<MyThread> threads = new List<MyThread>();

        public int GetNumOfTask()
        {
            return tasks.Count;
        }

        public void Enqueue (Action task)
        {
            lock (tasks)
           {
                tasks.Enqueue(task);
                Monitor.Pulse(tasks);
            }
        }
        
        public void Start()
        {
            for (int i = 0; i < numOfThreads; i++)
            {
                MyThread thread = new MyThread(tasks, i);
                threads.Add(thread);
            }
        }

        public void Dispose()
        {
            lock (tasks)
            {
                tasks.Clear();
                for (int i = 0; i < numOfThreads; i++)
                {
                    threads[i].SetWork();
                }
                Monitor.PulseAll(tasks);
                Console.WriteLine("all threads have been stopped");
            }
        }
    }
}
