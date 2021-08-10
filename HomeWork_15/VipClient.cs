using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary;

namespace HomeWork_15
{
    /// <summary>
    /// VIP-клиент
    /// </summary>
    class VipClient : Client
    {
        public VipClient(string name, int age, int departmentId) : base(name, age, departmentId)
        {
            RateOfCredit = 0.10;
            MinRate = 0.05;
        }
    }
}
