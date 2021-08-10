using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary;

namespace HomeWork_15
{
    static class ClientFactory
    {
        static Random r;

        static ClientFactory() { r = new Random(); }
        public static Client GetClient(string name,
                                       int age,
                                       int departmentId)
        {
            if (name.StartsWith("Обычный клиент_"))
            {
                return new StandardClient("Обычный клиент_", r.Next(18, 80), 1);
            }
            else if (name.StartsWith("VIP-клиент_"))
            {
                return new VipClient("VIP-клиент_", r.Next(18, 80), 2);
            }
            else if (name.StartsWith("Юридичесткое лицо_"))
            {
                return new LegalClient("Юридичесткое лицо_", r.Next(18, 80), 3);
            }
            else
            {
                return new NullClient();
            }
        }
    }
}
