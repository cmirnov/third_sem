using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using System.ServiceModel;
using System.ServiceModel.Diagnostics;
using System.Web;
using System.ServiceModel.Channels;
using System.ServiceModel.Web;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            int bufferSize = 15000000;
            WebServiceHost host = new WebServiceHost(typeof(Service), new Uri("net.tcp://localhost:11000/"));
            NetTcpBinding binding = new NetTcpBinding(SecurityMode.None);
            binding.MaxBufferPoolSize = bufferSize;
            binding.MaxBufferSize = bufferSize;
            binding.MaxReceivedMessageSize = bufferSize;
            host.AddServiceEndpoint(typeof(IService), binding, "net.tcp://localhost:11000/");
            host.Open();
            Console.WriteLine("Сервис открыт");
            Console.ReadKey();
            host.Close();
        }
    }
}
