using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using Contracts;


namespace Client
{
    static class Program
    {
        [STAThread]
        static void Main()
        {

            int bufferSize = 15000000;
            NetTcpBinding binding = new NetTcpBinding(SecurityMode.None);
            binding.MaxBufferPoolSize = bufferSize;
            binding.MaxBufferSize = bufferSize;
            binding.MaxReceivedMessageSize = bufferSize;
            ChannelFactory<IService> cf = new ChannelFactory<IService>(binding, "net.tcp://127.0.0.1:11000/");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form(cf.CreateChannel()));

        }
    }
}
