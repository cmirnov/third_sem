using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Drawing;

namespace Contracts
{
    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        List<string> GetListOfFilters();

        [OperationContract]
        Bitmap ApplyFilter(Bitmap image, string nameOfFilter);

        [OperationContract]
        int GetProgress();

        [OperationContract]
        void Stop();

    }
}
