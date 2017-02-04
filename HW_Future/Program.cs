using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Future
{
    class Program
    {
      //  const int NumOfElem = 30;
        static void Main(string[] args)
        {
            for (int NumOfElem = 1; NumOfElem < 100; NumOfElem++) //testing
            {
                int[] arr = new int[NumOfElem];

                for (int i = 0; i < NumOfElem; i++)
                {
                    arr[i] = i + 1;
                }

                IterSum firstSum = new IterSum();
                RecSum secondSum = new RecSum();

                Console.WriteLine("Expected value is {0}", NumOfElem * (NumOfElem + 1) / 2);
                Console.WriteLine("First value is {0}", firstSum.Sum(arr));
                Console.WriteLine("Second value is {0}", secondSum.Sum(arr));
                Console.WriteLine();
            }
        }
    }
}
