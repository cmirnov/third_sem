using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ForUniversity
{
    class Teacher
    {
        private IExamSystem _system;
        private Thread _thread;
        private int _numAdd = 90;
        private int _numRem = 10;
        private int _numCont = 900;
        private int _numOfStudents = 1000;
        private int _numOfExams = 100;
        private Random _rand;

        public Teacher(IExamSystem curSystem)
        {
            _rand = new Random();
            _system = curSystem;
            _thread = new Thread(() => Run());
            _thread.Start();
        }

        public void Run()
        {
            for (int i = 0; i < _numAdd; i++)
            {
                int[] temp = GetNewVal();
                _system.Add(temp[0], temp[1]);
            }

            for (int i = 0; i < _numRem; i++)
            {
                int[] temp = GetNewVal();
                _system.Remove(temp[0], temp[1]);
            }


            for (int i = 0; i < _numCont; i++)
            {
                int[] temp = GetNewVal();
                _system.Contains(temp[0], temp[1]);
            }
            
        }

        public int[] GetNewVal()
        {
            int[] temp = new int[2];
            temp[0] = _rand.Next(_numOfStudents);
            temp[1] = _rand.Next(_numOfExams);
            return temp;
        }
        public void Stop()
        {
            _thread.Join();
        }
    }
}
