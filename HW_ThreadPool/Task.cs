using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ThreadPool
{
    class Task
    {
        public static void MyTask()
        {
            Console.WriteLine("executing");
            Thread.Sleep(300);
        }
    }
}
