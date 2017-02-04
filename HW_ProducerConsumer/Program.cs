using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ProducerConsumer
{
    class Program
    {

        static void Main(string[] args)
        {
            List<Producer> currentProducer = new List<Producer>();
            List<Consumer> currentConsumer = new List<Consumer>();
            Flag isLocked = new Flag();
            

            const int numOfProd = 3;
            const int numOfCons = 3;


            List<int> sharedList = new List<int>();

            for (int i = 0; i < numOfProd; i++)
            {
                Producer prod = new Producer(i + 1, sharedList, isLocked);
                currentProducer.Add(prod);
            }

            for (int i = 0; i < numOfCons; i++)
            {
                Consumer cons = new Consumer(i + 1, sharedList, isLocked);
                currentConsumer.Add(cons);
            }
            Console.ReadKey();

            Console.WriteLine("Wait little bit");

            

            for (int i = 0; i < numOfCons; i++)
            {
                currentConsumer[0].Stop();
                currentConsumer.RemoveAt(0);
            }
            for (int i = 0; i < numOfProd; i++)
            {
                currentProducer[0].Stop();
                currentProducer.RemoveAt(0);
            }

            Console.WriteLine("that's all");

            return;
        }     
    }
}
