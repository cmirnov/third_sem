using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ThreadPool
{
    class MyThread
    {
        private Queue<Action> tasks = new Queue<Action>();
        private Thread curThread;
        private bool work = true;
        private int name;

        public MyThread(Queue<Action> tasks, int name)
        {
            this.name = name;
            this.tasks = tasks;
            Thread curThread = new Thread(Run);
            this.curThread = curThread;
            this.curThread.Start();
        }

        public void SetWork()
        {
            work = false;
        }

        public void Run()
        {
            while (work)
            {
                Action task = new Action(() => { });
                lock (tasks)
                {
                    if (tasks.Count() > 0)
                    {
                        task = tasks.Dequeue();

                        Console.WriteLine("{0} has new work", name);
                        Monitor.Pulse(tasks);
                    }
                    else
                    {
                        Monitor.Wait(tasks);
                        if (!work)
                        {
                            return;
                        }
                    }
                }
                task();
            }
        }

        public void Stop()
        {
            curThread.Join();
        }
    }
}
