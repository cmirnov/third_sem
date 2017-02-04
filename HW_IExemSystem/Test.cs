using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ForUniversity
{
    class Test
    {
        private int numOfTreads = 100;

        public void StatTest(IExamSystem system)
        {
            var timer = System.Diagnostics.Stopwatch.StartNew();
            List<Teacher> teachers = new List<Teacher>();
            for (int i = 0; i < numOfTreads; i++)
            {
                Teacher temp = new Teacher(system);
                teachers.Add(temp);
            }    
            for (int i = 0; i < numOfTreads; i++)
            {
                teachers[i].Stop();
            }
            Console.WriteLine(timer.ElapsedMilliseconds);
        }
    }
}
