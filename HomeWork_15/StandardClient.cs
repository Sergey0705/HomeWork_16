using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary;

namespace HomeWork_15
{
    /// <summary>
    /// Обычный клиент
    /// </summary>
    public class StandardClient : Client
    {
        public StandardClient(string name, int age, int departmentId) : base(name, age, departmentId)
        {
            RateOfCredit = 0.20;
            MinRate = 0.10;
        }
    }
}
