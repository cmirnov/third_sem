using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ProducerConsumer
{
    public class Flag
    {
        int isLocked = 0;

        public void Lock()
        {
            while (0 != Interlocked.CompareExchange(ref isLocked, 1, 0))
            { }
        }

        public void UnLock()
        {
            Interlocked.Exchange(ref isLocked, 0);
        }
    }
}
