using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Threading;
using Fibers;

namespace ProcessManager
{
    public static class ProcessManager
    {
        public static Queue<uint> queueOfActiveFibers = new Queue<uint>();
        public static List<uint> allFibers = new List<uint>();
        public static List<int> Priority = new List<int>();
        public static Dictionary<uint, int> allFibersWithPriority = new Dictionary<uint, int>();
        public static Dictionary<int, uint> supportDict = new Dictionary<int, uint>();
        private static uint currentFiber;
        public static int NumOfIter = 0;
        public static int Rand = 1000;


        public static void DeleteAllFibers()
        {
            foreach (uint fiber in allFibers)
            {
                 Fiber.Delete(fiber);
            }
        }

        //whitout priority
    /*
       public static void Switch(bool fiberFinished)
        {
            if(queueOfActiveFibers.Count == 0)
            {
                DeleteAllFibers();
            }
            queueOfActiveFibers.Dequeue();
            if(fiberFinished)
            {
                if(queueOfActiveFibers.Count == 0)
                {
                    Console.WriteLine("This is the end");
                    Fiber.Switch(Fiber.PrimaryId);
                }
                else
                {
                    currentFiber = queueOfActiveFibers.Peek();
                    Fiber.Switch(currentFiber);
                }
            }
            else
            {
                queueOfActiveFibers.Enqueue(currentFiber);
                currentFiber = queueOfActiveFibers.Peek();
                Fiber.Switch(currentFiber);
            }
        }
        */


        //we sort our fibers by priority and after that we just use FIFO's Switch function

        public static int GetMaxIdx()
        {
            int max = Priority[0];
            int curIdx = 0;
            for (int i = 0; i < Priority.Count; i++)
            {
                if (Priority[i] > max)
                {
                    max = Priority[i];
                    curIdx = i;
                }
            }
            return curIdx;
        }
        
        public static int GetMinIdx()
        {
            int min = Priority[0];
            int curIdx = 0;
            for (int i = 0; i < Priority.Count; i++)
            {
                if (Priority[i] < min)
                {
                    min = Priority[i];
                    curIdx = i;
                }
            }
            return curIdx;
        }

        public static void Switch(bool fiberFinished)
        {
            if(allFibers.Count == 0)
            {
                DeleteAllFibers();
            }
            if(fiberFinished)
            {
                int idx = allFibers.IndexOf(currentFiber);
                allFibers.Remove(currentFiber);
                Priority.Remove(Priority[idx]);
                if(allFibers.Count == 0)
                {
                    Console.WriteLine("This is the end");
                    Fiber.Switch(Fiber.PrimaryId);
                }
                else
                {
                    int curIdx = 0;
                    if (NumOfIter == Rand)
                    {
                        curIdx = GetMinIdx();
                        NumOfIter = 0;
                    }
                    else
                    {
                        curIdx = GetMaxIdx();
                        NumOfIter++;
                    }
                    currentFiber = allFibers[curIdx];
                    Fiber.Switch(currentFiber);
                }
            }
            else
            {
                int curIdx = 0;
                if (NumOfIter == Rand)
                {
                    curIdx = GetMinIdx();
                    NumOfIter = 0;
                }
                else
                {
                    curIdx = GetMaxIdx();
                    NumOfIter++;
                }
                currentFiber = allFibers[curIdx];
                Fiber.Switch(currentFiber);
            }           
        }
        

        public static void Main()
        {
            int numOfFibers = 5;
            for (int i = 0; i < numOfFibers; i++)
            {
                Process process = new Process();
                Fiber fiber = new Fiber(new Action(process.Run));
                queueOfActiveFibers.Enqueue(fiber.Id);
                allFibers.Add(fiber.Id);
                Priority.Add(process.Priority);
                allFibersWithPriority.Add(fiber.Id, process.Priority);
                Console.WriteLine("{0}-{1}", fiber.Id, process.Priority);
            }
            //currentFiber = queueOfActiveFibers.Peek(); //the first elem for switch without priority

            currentFiber = allFibers[0];
            Switch(false);
            Console.ReadLine();
        }
    }

    public class Process
    {
        private static readonly Random Rng = new Random();

        private const int LongPauseBoundary = 10000;

        private const int ShortPauseBoundary = 100;

        private const int WorkBoundary = 1000;

        private const int IntervalsAmountBoundary = 10;

        private const int PriorityLevelsNumber = 10;

        private readonly List<int> _workIntervals = new List<int>();
        private readonly List<int> _pauseIntervals = new List<int>();

        public Process()
        {
            int amount = Rng.Next(IntervalsAmountBoundary);

            for (int i = 0; i < amount; i++)
            {
                _workIntervals.Add(Rng.Next(WorkBoundary));
                _pauseIntervals.Add(Rng.Next(
                        Rng.NextDouble() > 0.9
                            ? LongPauseBoundary
                            : ShortPauseBoundary));
            }

            Priority = Rng.Next(PriorityLevelsNumber);
        }

        public void Run()
        {
            for (int i = 0; i < _workIntervals.Count; i++)
            {
                Thread.Sleep(_workIntervals[i]); // work emulation
                DateTime pauseBeginTime = DateTime.Now;
                do
                {
                    ProcessManager.Switch(false);
                } while ((DateTime.Now - pauseBeginTime).TotalMilliseconds < _pauseIntervals[i]); // I/O emulation
            }
            ProcessManager.Switch(true);
        }

        public int Priority
        {
            get; private set;
        }

        public int TotalDuration
        {
            get
            {
                return ActiveDuration + _pauseIntervals.Sum();
            }
        }

        public int ActiveDuration
        {
            get
            {
                return _workIntervals.Sum();
            }
        }
    }
}
