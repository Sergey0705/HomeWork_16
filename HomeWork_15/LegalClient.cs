using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary;

namespace HomeWork_15
{
    /// <summary>
    /// Юридическое лицо
    /// </summary>
    class LegalClient : Client
    {
        public LegalClient(string name, int age, int departmentId) : base(name, age, departmentId)
        {
            RateOfCredit = 0.12;
            MinRate = 0.06;
        }
    }
}
