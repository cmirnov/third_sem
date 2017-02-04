using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadPool
{
    class Program
    {
        static void Main(string[] args)
        {
            ThreadPool threadPool = new ThreadPool();
            threadPool.Start();
            for (int i = 0; i < 42; i++)
            {
                threadPool.Enqueue(Task.MyTask);
            }

            while (threadPool.GetNumOfTask() != 0) { }
            threadPool.Dispose();
        }
    }
}
