using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ProducerConsumer
{
    public class Consumer
    {
        int name;
        Thread myThread;
        List<int> sharedList;
        int wait = 1000;
        bool isEnd = false;

        public Consumer(int name, List<int> sharedList, Flag isLocked)
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
                    if (sharedList.Count == 0)
                    {
                        Console.WriteLine("Consumer # {0} can't get number because list is empty", name);
                    }
                    else
                    {
                        int val = sharedList.Last();
                        Console.WriteLine("Consumer # {0} get {1} from list", name, val);
                        sharedList.Remove(val);
                    }
                }
                isLocked.UnLock();
                Thread.Sleep(wait);
            }
        }
    }
}
