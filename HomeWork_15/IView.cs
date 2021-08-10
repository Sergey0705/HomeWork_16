using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_15
{
    public interface IView
    {
        string Account { get; }

        Department Department { get; }

        ClientsOfBank Client { get; }

        string Sum { get; }

        string TransferMonth { get; }

        string NumberOfNavigation { get; }
    }
}
