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

namespace TestSecond
{
    static class Program
    {
        static private int picH = 1;
        static private int picW = 1;
        static private int numClients = 3;
        static private List<Point> listOfPointsMid = new List<Point>();
        static private List<Point> listOfPointsAver = new List<Point>();
        static private List<long> Time = new List<long>();

        [STAThread]
        static void Main()
        {
            GetData();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new SecondTest(listOfPointsMid, listOfPointsAver));
        }

        static private void GetData()
        {
            for (int i = 0; i < numClients; i++)
            {
                Time.Add(0);
            }
            for (int i = 1; i < 10; i++)
            {
                picH += 100;
                picW += 100;
                
                try
                {
                    List<Thread> clients = new List<Thread>();
                    for (int j = 0; j < 3; j++)
                    {
                        clients.Add(new Thread(() => Client(j)));
                        clients[j].Start();
                    }

                    bool check = true;
                    long result = 0;
                    for (int j = 0; j < numClients; j++)
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
                    listOfPointsAver.Add(new Point(picW * picH, (int)(result / numClients)));
                    Time.Sort();
                    listOfPointsMid.Add(new Point(picW * picH, (int)Time[numClients / 2]));
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
