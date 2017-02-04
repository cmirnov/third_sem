using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using Contracts;
using System.Drawing;

namespace Testing
{
    class Program
    {
        private static int num;
        private static bool correct;


        [STAThread]
        static void Main()
        {
            correct = true;
            int bufferSize = 15000000;
            NetTcpBinding binding = new NetTcpBinding(SecurityMode.None);
            binding.MaxBufferPoolSize = bufferSize;
            binding.MaxBufferSize = bufferSize;
            binding.MaxReceivedMessageSize = bufferSize;
            ChannelFactory<IService> cf = new ChannelFactory<IService>(binding, "net.tcp://127.0.0.1:11000/");
            
            List<Thread> maxNum = new List<Thread>();

            for (int i = 0; correct; i++)
            {
                num = i + 1;
                try
                {
                    maxNum.Add(new Thread(() => { Run(cf, i); }));
                    maxNum[i].Start();
                }
                catch
                {

                }
                Thread.Sleep(300);
            }

            for (int i = 0; i < num; i++)
            {
                maxNum[i].Join();
            }

            Console.WriteLine("The maximum numbers of clients is {0}", num);

        }

        public static void Run(ChannelFactory<IService> cf, int name)
        {
            IService service = cf.CreateChannel();

            try
            {
                Bitmap image = new Bitmap(100, 100);
                while (correct)
                {
                    image = service.ApplyFilter(image, "red");
                }
            }
            catch
            {
                correct = false;
                return;
            }
        }
    }
}
