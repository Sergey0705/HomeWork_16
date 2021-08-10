using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary;

namespace HomeWork_15
{
    class NullClient : Client
    {
        public NullClient() 
        {
            Name = "Not determined";
            Age = default;
        }
    }
}
