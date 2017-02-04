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
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "IService1" в коде и файле конфигурации.
    [ServiceContract]
    public interface IService1
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
