using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ProducerConsumer
{
    public class Producer
    {
        int name;
        Thread myThread;
        List<int> sharedList;
        const int wait = 1000;
        bool isEnd = false;

        public Producer(int name, List<int> sharedList, Flag isLocked)
        {
            this.name = name;
            this.sharedList = sharedList;
            myThread = new Thread(() => this.Run(isLocked));
            myThread.Start();
        }

        public void Stop()
        {
            this.isEnd = true;
            myThread.Join();
        }

        public void Run(Flag isLocked)
        {
            while (!this.isEnd)
            {
                isLocked.Lock();
                if (!this.isEnd)
                {
                    Random rand = new Random();
                    int val = rand.Next(100);
                    sharedList.Add(val);
                    Console.WriteLine("Producer # {0} add number {1} to list", name, val);
                }
                isLocked.UnLock();
                Thread.Sleep(wait);
            }
        }
    }

}
