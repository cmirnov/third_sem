using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Text;
using System.Threading;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using Contracts;

namespace FirstTest
{
    static class Program
    {
        static private int picH = 100;
        static private int picW = 100;
        static private List<Point> listOfPoints = new List<Point>();
        static private List<long> Time = new List<long>();

        [STAThread]
        static void Main()
        {
            GetData();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FirstTest(listOfPoints));
        }

        static public void GetData()
        {
            for (int i = 1; true; i++)
            {
                Time.Add(0);
                try
                {
                    List<Thread> clients = new List<Thread>();
                    for (int j = 0; j < i; j++)
                    {
                        clients.Add(new Thread(() => Client(j)));
                        clients[j].Start();
                    }

                    bool check = true;
                    long result = 0;
                    for (int j = 0; j < i; j++)
                    {
                        while (clients[j].IsAlive) { }
                        
                        result += Time[j];
                        if (Time[j] == -1)
                        {
                            check = false;
                            break;
                        }
                    }
                    if (!check)
                    {
                        break;
                    }
                    listOfPoints.Add(new Point(i, (int)result));
                }
                catch
                {
                    return;
                }
            }
        }

        static private void Client(int name)
        {
            int bufferSize = 15000000;
            NetTcpBinding binding = new NetTcpBinding(SecurityMode.None);
            binding.MaxBufferPoolSize = bufferSize;
            binding.MaxBufferSize = bufferSize;
            binding.MaxReceivedMessageSize = bufferSize;
            ChannelFactory<IService> cf = new ChannelFactory<IService>(binding, "net.tcp://127.0.0.1:11000/");
            try
            {
                IService service = cf.CreateChannel();
                var time = System.Diagnostics.Stopwatch.StartNew();
                Bitmap image = new Bitmap(picH, picW);
                image = service.ApplyFilter(image, "blue");
                long resTime = time.Elapsed.Milliseconds;
                Time[name] = resTime;
                return;
            }
            catch
            {
                Time[name] = -1;
                return;
            }
        }
    }
}
