using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForUniversity
{
    class Program
    {
        static void Main(string[] args)
        {
            Test test = new Test();
            IExamSystem firstSystem = new LockingTable();
            test.StatTest(firstSystem);
            IExamSystem secondSystem = new LockingPartOfTable();
            test.StatTest(secondSystem);
        }
    }
}
